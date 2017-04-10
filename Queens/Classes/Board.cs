using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens.Classes
{
    public class Board
    {
        public List<Cell> Cells { get; set; }

        public IEnumerable<Cell> AvailableCells
        {
            get
            {
                return Cells.Where(c => c.IsAvailable);
            }
        }
        public int Size { get; private set; }

        public Board(int size)
        {
            this.Size = size;

            Cells = new List<Cell>(size * size);//new Cell[size * size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Cells.Add(new Cell(row, col));
                }
            }
        }

        //Copy constructor
        public Board(Board b)
        {
            Cells = new List<Cell>();
            foreach(var c in b.Cells)
            {
                var cell = new Cell(c.row, c.col);
                cell.HasPiece = c.HasPiece;
                cell.IsAvailable = c.IsAvailable;
                Cells.Add(cell);
            }

            Size = b.Size;
        }

        public void PlacePiece(int row, int col)
        {
            var Cell = this.Cells[row * Size + col];

            if (!Cell.IsAvailable || Cell.HasPiece)
                throw new Exception("Invalid Placement");

            Cell.HasPiece = true;
            Cell.IsAvailable = false;

            CullAvailableCells(row, col);
        }

        private void CullAvailableCells(int row, int col)
        {
            var diagonalCells = GetDiagonalCells(row, col);

            foreach(var cell in Cells.Where(c => c.IsAvailable))
            {
                //Check for same row or column, or diagonal
                if (cell.row == row || cell.col == col || diagonalCells.Where(d => d.row == cell.row && d.col == cell.col).Any())
                    cell.IsAvailable = false;
            }
        }

        private List<Cell> GetDiagonalCells(int row, int col)
        {
            var ret = new List<Cell>();
            int nextRow, nextCol;

            //Down, right (+, +)
            nextRow = row + 1;
            nextCol = col + 1;
            while(IsInBounds(nextRow, nextCol))
            {
                ret.Add(Cells[IndexFromCoordinates(nextRow++, nextCol++)]);
            }

            //Down, left (+, -)
            nextRow = row + 1;
            nextCol = col - 1;
            while (IsInBounds(nextRow, nextCol))
            {
                ret.Add(Cells[IndexFromCoordinates(nextRow++, nextCol--)]);
            }

            //Up, right (-, +)
            nextRow = row - 1;
            nextCol = col + 1;
            while (IsInBounds(nextRow, nextCol))
            {
                ret.Add(Cells[IndexFromCoordinates(nextRow--, nextCol++)]);
            }

            //Up, left (-, -)
            nextRow = row - 1;
            nextCol = col - 1;
            while (IsInBounds(nextRow, nextCol))
            {
                ret.Add(Cells[IndexFromCoordinates(nextRow--, nextCol--)]);
            }


            return ret;
        }

        public int IndexFromCoordinates(int row, int col)
        {
            return row * Size + col;
        }

        public bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < Size; row++)
            {
                sb.Append("| ");

                for (int col = 0; col < Size; col++)
                {
                    if(Cells[IndexFromCoordinates(row, col)].HasPiece)
                    {
                        sb.Append("Q ");
                    }
                    else
                    {
                        sb.Append("+ ");
                    }
                }

                sb.Append("|\n");
            }

            return sb.ToString();
        }

    }
}
