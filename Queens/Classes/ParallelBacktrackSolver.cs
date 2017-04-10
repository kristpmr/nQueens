using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens.Classes
{
    public static class ParallelBacktrackSolver
    {
        static Result Result { get; set; }
        static int TotalMovesCount { get; set; }

        public static async Task<Result> Start(int Size, int StartingRow, int StartingCol)
        {
            Board start = new Board(Size);
            start.PlacePiece(StartingRow, StartingCol);
            PlayNext(start, 0);

            return Result;
        }

        public static async void PlayNext(Board board, int generation)
        {
            TotalMovesCount++;

            var nextMoves = board.AvailableCells.ToList();

            if (nextMoves.Count() == 0)
            {
                ProcessFinalBoardState(board);
            }
            else
            {
                foreach (var nextMove in nextMoves)
                {
                    var newBoard = new Board(board);
                    newBoard.PlacePiece(nextMove.row, nextMove.col);

                    if(!newBoard.AvailableCells.Any())
                    {
                        ProcessFinalBoardState(newBoard);
                        break;
                    }

                    PlayNext(newBoard, generation + 1);
                }
            }
        }

        public static void ProcessFinalBoardState(Board board)
        {
            int count = board.Cells.Where(c => c.HasPiece).Count();

            if (Result.PieceCount > count)
                return;

            Result.PieceCount = count;
            Result.MoveCount = TotalMovesCount;
            Result.ExampleBoard = board;
            //Result.startCol = StartingCol;
            //Result.startRow = StartingRow;

            ////Quit searching this starting position when we find a win
            //if (count == Size)
            //    abort = true;
        }

    }
}
