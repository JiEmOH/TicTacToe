namespace TicTacToe.DTO
{
    public class GameStateResponse
    {
        public string[][] Board { get; set; }
        public bool IsCompleted { get; set; }
    }
}

