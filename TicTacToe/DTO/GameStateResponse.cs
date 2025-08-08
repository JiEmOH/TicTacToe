namespace TicTacToe.DTO
{
    public class GameStateResponse
    {
        public string[][] Board { get; set; } = Array.Empty<string[]>();
        public bool IsCompleted { get; set; }
    }

    public class MoveRequest
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Symbol { get; set; } = "X";
    }
}

