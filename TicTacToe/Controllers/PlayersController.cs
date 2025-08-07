using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers
{
    public class PlayersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
