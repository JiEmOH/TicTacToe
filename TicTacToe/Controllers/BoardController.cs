using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBoard()
    {
        // 3x3 доска, пока пустая
        var board = new string[3, 3]
        {
            { "", "", "" },
            { "", "", "" },
            { "", "", "" }
        };
        return Ok(board);
    }
}

