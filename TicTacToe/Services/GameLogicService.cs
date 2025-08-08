using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TicTacToe.DTO;

namespace TicTacToe.Services
{
    public class GameLogicService
    {
        private const int size = 3;
        private string boardState;
        private bool isCompleted = false;
        private readonly ILogger<GameLogicService>? _logger;

        // Внедрим ILogger опционально для отладки
        public GameLogicService(ILogger<GameLogicService>? logger = null)
        {
            _logger = logger;
            boardState = new string('_', size * size);
            boardState = NormalizeBoardState(boardState);
            _logger?.LogInformation("Init boardState: '{State}' len={Len}", boardState, boardState.Length);
        }

        // Гарантируем ровно size*size символов; удаляем пробелы, обрезаем или дополняем '_'
        private string NormalizeBoardState(string? state)
        {
            if (string.IsNullOrEmpty(state))
                return new string('_', size * size);

            // Убираем пробелы, которые могли появиться из-за fixed-length колонок
            state = state.Trim();

            if (state.Length == size * size)
                return state;

            if (state.Length > size * size)
                return state.Substring(0, size * size);

            return state.PadRight(size * size, '_');
        }

        public string[][] GetBoard()
        {
            boardState = NormalizeBoardState(boardState);

            var board = new string[size][];
            for (int i = 0; i < size; i++)
            {
                board[i] = new string[size];
                for (int j = 0; j < size; j++)
                {
                    int idx = i * size + j;
                    // Защита индекса — если что-то не так, считаем клетку пустой
                    char c = idx < boardState.Length ? boardState[idx] : '_';
                    board[i][j] = c == '_' ? "" : c.ToString();
                }
            }
            return board;
        }

        public GameStateResponse MakeMove(int row, int col, string symbol)
        {
            boardState = NormalizeBoardState(boardState);

            if (isCompleted)
                return new GameStateResponse { Board = GetBoard(), IsCompleted = true };

            if (row < 0 || row >= size || col < 0 || col >= size)
                return new GameStateResponse { Board = GetBoard(), IsCompleted = isCompleted };

            int idx = row * size + col;

            if (idx >= boardState.Length) // дополнительная защита
            {
                boardState = NormalizeBoardState(boardState);
                if (idx >= boardState.Length)
                    return new GameStateResponse { Board = GetBoard(), IsCompleted = isCompleted };
            }

            if (boardState[idx] != '_')
                return new GameStateResponse { Board = GetBoard(), IsCompleted = isCompleted };

            var arr = boardState.ToCharArray();
            arr[idx] = symbol.Length > 0 ? symbol[0] : '_';
            boardState = new string(arr);
            boardState = NormalizeBoardState(boardState);

            isCompleted = CheckWin(symbol);

            _logger?.LogInformation("Move: {Sym} @ {R},{C} => boardState='{State}' len={Len}", symbol, row, col, boardState, boardState.Length);

            return new GameStateResponse
            {
                Board = GetBoard(),
                IsCompleted = isCompleted
            };
        }

        private bool CheckWin(string symbol)
        {
            char s = symbol[0];
            var lines = new List<int[]>();

            for (int i = 0; i < size; i++)
                lines.Add(Enumerable.Range(i * size, size).ToArray());

            for (int i = 0; i < size; i++)
                lines.Add(Enumerable.Range(0, size).Select(x => x * size + i).ToArray());

            lines.Add(Enumerable.Range(0, size).Select(i => i * size + i).ToArray());
            lines.Add(Enumerable.Range(0, size).Select(i => i * size + (size - 1 - i)).ToArray());

            foreach (var line in lines)
            {
                if (line.All(idx => idx < boardState.Length && boardState[idx] == s))
                    return true;
            }

            return false;
        }
    }
}



