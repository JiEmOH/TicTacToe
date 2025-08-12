using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Services;
using TicTacToe.DTO;
using TicTacToe.Data;
using TicTacToe.Models;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly GameLogicService _logic;
    private readonly ApplicationContext _db;

    public GameController(GameLogicService logic, ApplicationContext db)
    {
        _logic = logic;
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame()
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Player1Id = Guid.NewGuid(),
            Player2Id = null,
            BoardState = _logic.InitializeBoardState(),
            IsCompleted = false,
            CurrentTurnSymbol = "X",
            WinnerSymbol = null
        };
        _db.Games.Add(game);
        await _db.SaveChangesAsync();

        return Ok(new CreateGameResponse { GameId = game.Id, AssignedSymbol = "X" });
    }

    [HttpPost("{gameId:guid}/join")]
    public async Task<IActionResult> JoinGame(Guid gameId)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null) return NotFound();
        if (game.Player2Id.HasValue) return Conflict("Game already has two players");

        game.Player2Id = Guid.NewGuid();
        await _db.SaveChangesAsync();

        return Ok(new JoinGameResponse { GameId = game.Id, AssignedSymbol = "O" });
    }

    [HttpGet("{gameId:guid}")]
    public async Task<IActionResult> GetState(Guid gameId)
    {
        var game = await _db.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null) return NotFound();

        var response = new GameStateResponse
        {
            Board = _logic.ConvertBoardToMatrix(game.BoardState),
            IsCompleted = game.IsCompleted,
            IsDraw = game.IsCompleted && string.IsNullOrEmpty(game.WinnerSymbol),
            WinnerSymbol = game.WinnerSymbol,
            CurrentTurnSymbol = game.CurrentTurnSymbol
        };
        return Ok(response);
    }

    [HttpPost("{gameId:guid}/move")]
    public async Task<IActionResult> MakeMove(Guid gameId, [FromBody] MoveRequest move)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null) return NotFound();
        if (game.IsCompleted) return BadRequest("Game is already completed");

        // enforce turn
        if (!string.Equals(move.Symbol, game.CurrentTurnSymbol, StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest($"It is {game.CurrentTurnSymbol}'s turn");
        }

        if (!_logic.TryApplyMove(game.BoardState, move.Row, move.Col, move.Symbol, out var newBoard))
        {
            return BadRequest("Invalid move");
        }

        game.BoardState = newBoard;

        if (_logic.CheckWin(game.BoardState, move.Symbol))
        {
            game.IsCompleted = true;
            game.WinnerSymbol = move.Symbol.ToUpperInvariant();
        }
        else if (_logic.CheckDraw(game.BoardState))
        {
            game.IsCompleted = true;
            game.WinnerSymbol = null; // draw
        }
        else
        {
            game.CurrentTurnSymbol = _logic.ToggleSymbol(game.CurrentTurnSymbol);
        }

        await _db.SaveChangesAsync();

        var response = new GameStateResponse
        {
            Board = _logic.ConvertBoardToMatrix(game.BoardState),
            IsCompleted = game.IsCompleted,
            IsDraw = game.IsCompleted && string.IsNullOrEmpty(game.WinnerSymbol),
            WinnerSymbol = game.WinnerSymbol,
            CurrentTurnSymbol = game.CurrentTurnSymbol
        };
        return Ok(response);
    }
}



