using Queens.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Queens
{
    public partial class Form1 : Form
    {
        static readonly object locker = new object();
        static RichTextBox box; 

        public Form1()
        {
            InitializeComponent();
            box = richTextBox1;
            richTextBox1.Font = new Font(FontFamily.GenericMonospace, richTextBox1.Font.Size);
        }

        private void BtnFire_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            var size = boardSize.Value;
            var upperHalfSize = Math.Ceiling(size / 2);

            for (int row = 0; row < upperHalfSize; row++)
            {
                for (int col = 0; col < upperHalfSize; col++)
                {
                    BacktrackSolver s = new BacktrackSolver((int)size, row, col);

                    s.Start();
                    
                    richTextBox1.Text += $"\nStart: ({row}, {col})\n"
                            + s.Result.ExampleBoard.ToString() +
                            $"\nPieces Placed: {s.Result.PieceCount}\n" +
                            $"Moves examined: {s.Result.MoveCount}\n" +
                            "\n-------------------------------------------------\n";

                }
            }
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            var size = boardSize.Value;
            var upperHalfSize = Math.Ceiling(size / 2);

            List<Task> solveTasks = new List<Task>();

            for (int row = 0; row < upperHalfSize; row++)
            {
                for (int col = 0; col < upperHalfSize; col++)
                {
                    BacktrackSolver s = new BacktrackSolver((int)size, row, col);
                    var task = s.Start();

                    solveTasks.Add(task);


                }
            }
        }

        private async void SetText(Result r)
        {
            lock (locker)
            {
                box.Text += $"\nStart: ({r.startRow}, {r.startCol})\n"
                            + r.ExampleBoard.ToString() +
                            $"\nPieces Placed: {r.PieceCount}\n" +
                            $"Moves examined: {r.MoveCount}\n" +
                            "\n-------------------------------------------------\n";
            }
        }

        void ExecuteSolver(int size, int startRow, int startCol)
        {
            BacktrackSolver s = new BacktrackSolver((int)size, startRow, startCol);
            s.Start();

            lock (locker)
            {
                box.Text += $"\nStart: ({startRow}, {startCol})\n"
                            + s.Result.ExampleBoard.ToString() +
                            $"\nPieces Placed: {s.Result.PieceCount}\n" +
                            $"Moves examined: {s.Result.MoveCount}\n" +
                            "\n-------------------------------------------------\n";
            }
        }
    }
}
