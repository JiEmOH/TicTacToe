namespace TicTacToe.DTO
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string BoardState { get; set; }
        public char CurrentPlayerSymbol { get; set; }
        public bool IsCompleted { get; set; }
    }
}
