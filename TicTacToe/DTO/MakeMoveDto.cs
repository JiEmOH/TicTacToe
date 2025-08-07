namespace TicTacToe.DTO
{
    public class MakeMoveDto
    {
        public Guid GameId { get; set; }
        public int Position { get; set; } // 0-8 (ячейка доски)
    }
}