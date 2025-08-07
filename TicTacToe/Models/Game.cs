
namespace TicTacToe.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string BoardState { get; set; } = "_________"; // 9 символов (3x3)
        public char CurrentPlayerSymbol { get; set; } = 'X'; // X ходит первым
        public bool IsCompleted { get; set; }
    }
}
