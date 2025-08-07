namespace TicTacToe.DTO
{
    public class GameStateResponse
    {
        public string[][] Board { get; set; } = Array.Empty<string[]>();
        public bool IsCompleted { get; set; }
    }
}


