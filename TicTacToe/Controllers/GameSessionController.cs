using Microsoft.AspNetCore.Mvc;
using TicTacToe.DTO;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameSessionController : ControllerBase
    {
        private readonly GameLogicService _logic;

        public GameSessionController(GameLogicService logic)
        {
            _logic = logic;
        }

        [HttpGet("board")]
        public IActionResult GetBoard()
        {
            var board = _logic.GetInitialBoard();

            return Ok(new GameStateResponse
            {
                Board = board,
                IsCompleted = false
            });
        }
    }
}


