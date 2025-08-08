using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;
using TicTacToe.DTO;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly GameLogicService _logic;

    public GameController(GameLogicService logic)
    {
        _logic = logic;
    }

    [HttpGet("board")]
    public IActionResult GetBoard()
    {
        return Ok(new GameStateResponse
        {
            Board = _logic.GetBoard(),
            IsCompleted = false
        });
    }

    [HttpPost("move")]
    public IActionResult MakeMove([FromBody] MoveRequest move)
    {
        var result = _logic.MakeMove(move.Row, move.Col, move.Symbol);
        return Ok(result);
    }
}



