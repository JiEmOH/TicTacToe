using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TicTacToe.DTO;

namespace TicTacToe.Services
{
    public class GameLogicService
    {
        private const int BoardSize = 3;
        private readonly ILogger<GameLogicService>? _logger;

        public GameLogicService(ILogger<GameLogicService>? logger = null)
        {
            _logger = logger;
        }

        public string InitializeBoardState()
        {
            return new string('_', BoardSize * BoardSize);
        }

        public string NormalizeBoardState(string? state)
        {
            if (string.IsNullOrEmpty(state))
                return InitializeBoardState();

            state = state.Trim();

            if (state.Length == BoardSize * BoardSize)
                return state;

            if (state.Length > BoardSize * BoardSize)
                return state.Substring(0, BoardSize * BoardSize);

            return state.PadRight(BoardSize * BoardSize, '_');
        }

        public string[][] ConvertBoardToMatrix(string boardState)
        {
            var normalized = NormalizeBoardState(boardState);
            var board = new string[BoardSize][];
            for (int i = 0; i < BoardSize; i++)
            {
                board[i] = new string[BoardSize];
                for (int j = 0; j < BoardSize; j++)
                {
                    int idx = i * BoardSize + j;
                    char c = idx < normalized.Length ? normalized[idx] : '_';
                    board[i][j] = c == '_' ? string.Empty : c.ToString();
                }
            }
            return board;
        }

        public bool TryApplyMove(in string currentBoardState, int row, int col, string symbol, out string newBoardState)
        {
            newBoardState = NormalizeBoardState(currentBoardState);

            if (string.IsNullOrWhiteSpace(symbol))
                return false;
            char s = char.ToUpperInvariant(symbol[0]);
            if (s != 'X' && s != 'O')
                return false;

            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize)
                return false;

            int idx = row * BoardSize + col;
            if (idx >= newBoardState.Length)
                return false;

            if (newBoardState[idx] != '_')
                return false;

            var arr = newBoardState.ToCharArray();
            arr[idx] = s;
            newBoardState = new string(arr);
            return true;
        }

        public bool CheckWin(string boardState, string symbol)
        {
            var normalized = NormalizeBoardState(boardState);
            char s = char.ToUpperInvariant(symbol[0]);
            var lines = new List<int[]>();

            for (int i = 0; i < BoardSize; i++)
                lines.Add(Enumerable.Range(i * BoardSize, BoardSize).ToArray());

            for (int i = 0; i < BoardSize; i++)
                lines.Add(Enumerable.Range(0, BoardSize).Select(x => x * BoardSize + i).ToArray());

            lines.Add(Enumerable.Range(0, BoardSize).Select(i => i * BoardSize + i).ToArray());
            lines.Add(Enumerable.Range(0, BoardSize).Select(i => i * BoardSize + (BoardSize - 1 - i)).ToArray());

            foreach (var line in lines)
            {
                if (line.All(idx => idx < normalized.Length && normalized[idx] == s))
                    return true;
            }

            return false;
        }

        public bool CheckDraw(string boardState)
        {
            var normalized = NormalizeBoardState(boardState);
            return normalized.All(c => c == 'X' || c == 'O');
        }

        public string ToggleSymbol(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) return "X";
            return char.ToUpperInvariant(symbol[0]) == 'X' ? "O" : "X";
        }
    }
}



