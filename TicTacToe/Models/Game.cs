namespace TicTacToe.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Player1Id { get; set; }
        public Guid? Player2Id { get; set; }
        public string BoardState { get; set; } = "_________"; // 9 символов (3x3)
        public bool IsCompleted { get; set; }

    }
}
