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
        var board = _logic.GetInitialBoard(); // возвращает string[][]
        return Ok(new GameStateResponse
        {
            Board = board,
            IsCompleted = false
        });
    }

    [HttpGet("simpleboard")]
    public IActionResult GetSimpleBoard()
    {
        var board = new string[][]
        {
        new string[] { "", "", "" },
        new string[] { "", "", "" },
        new string[] { "", "", "" }
        };
        return Ok(board);
    }

}



