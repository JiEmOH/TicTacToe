namespace TicTacToe.DTO
{
    public class GameStateResponse
    {
        public string[][] Board { get; set; } = Array.Empty<string[]>();
        public bool IsCompleted { get; set; }
        public bool IsDraw { get; set; }
        public string? WinnerSymbol { get; set; }
        public string CurrentTurnSymbol { get; set; } = "X";
    }

    public class MoveRequest
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string Symbol { get; set; } = "X";
    }

    public class CreateGameResponse
    {
        public Guid GameId { get; set; }
        public string AssignedSymbol { get; set; } = "X";
    }

    public class JoinGameResponse
    {
        public Guid GameId { get; set; }
        public string AssignedSymbol { get; set; } = "O";
    }
}

