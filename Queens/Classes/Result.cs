using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queens.Classes
{
    public class Result
    {
        public int PieceCount { get; set; }
        public Board ExampleBoard { get; set; }
        public int MoveCount { get; set; }

        public int startRow { get; set; }

        public int startCol { get; set; }
    }
}
