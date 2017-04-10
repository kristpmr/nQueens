using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens.Classes
{
    public class Cell
    {
        public int row { get; set; }
        public int col { get; set; }
        public bool HasPiece { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Cell(int x, int y)
        {
            this.row = x;
            this.col = y;
        }

        public override string ToString()
        {
            return $"{row}, {col}";
        }
    }
}
