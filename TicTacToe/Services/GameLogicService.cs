namespace TicTacToe.Services
{
    public class GameLogicService
    {
        public string[][] GetInitialBoard()
        {
            return new string[][]
            {
                new string[] { "", "", "" },
                new string[] { "", "", "" },
                new string[] { "", "", "" }
            };
        }
    }
}



