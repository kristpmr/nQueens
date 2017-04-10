using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens.Classes
{
    public class BacktrackSolver
    {
        public List<Board> TopBoards { get; set; }
        public int StartingRow { get; set; }
        public int StartingCol { get; set; }
        public int TotalMovesCount { get; set; }

        private bool abort = false;

        public Result Result { get; set; }

        public int Size { get; set; }

        public BacktrackSolver(int size, int startRow, int startCol)
        {
            StartingRow = startRow;
            StartingCol = startCol;
            Size = size;            

            TopBoards = new List<Board>();
            Result = new Result();
        }

        public async Task<Result> Start()
        {
            Board start = new Board(Size);
            start.PlacePiece(StartingRow, StartingCol);
            PlayNext(start, 0);

            return Result;
        }

        public async void PlayNext(Board board, int generation)
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
                    if (abort)
                        break;

                    var newBoard = new Board(board);
                    newBoard.PlacePiece(nextMove.row, nextMove.col);
                    PlayNext(newBoard, generation + 1);
                }
            }            
        }

        public void ProcessFinalBoardState(Board board)
        {
            int count = board.Cells.Where(c => c.HasPiece).Count();

            if (Result.PieceCount > count)
                return;

            Result.PieceCount = count;
            Result.MoveCount = TotalMovesCount;
            Result.ExampleBoard = board;
            Result.startCol = StartingCol;
            Result.startRow = StartingRow;

            //Quit searching this starting position when we find a win
            if (count == Size)
                abort = true;
        }

    }
}
