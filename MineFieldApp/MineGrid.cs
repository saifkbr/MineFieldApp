using System;
using System.Drawing;
using static System.Console;

namespace MineFieldApp
{
    public interface IMineGrid
    {
        void ClearScreen();
        void PrintGrid(Point[] minesHit, Point currentPosition);
        void PrintInformation(int remainingLives, int totalNumberOfMoves, Point currentPosition);
    }

    public class MineGrid : IMineGrid
    {
        private readonly int rowToPrint;
        private readonly int columnToPrint;

        public MineGrid(int numberOfRows, int numberOfColumns)
        {
            this.rowToPrint = numberOfRows;
            this.columnToPrint = numberOfColumns;
        }

        public void ClearScreen()
        {
            Clear();
        }

        public void PrintGrid(Point[] minesHit, Point currentPosition)
        {
            WriteLine();

            for (int i = 0; i < this.rowToPrint + 1; i++)
            {
                for (int j = 0; j < this.columnToPrint + 1; j++)
                {
                    // Column header
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            PrintCell(string.Empty);
                        }
                        else
                        {
                            var columnHeader = Convert.ToChar(65 + (j - 1));
                            PrintCell(columnHeader.ToString());
                        }
                    }
                    else
                    {
                        // Row header
                        if (j == 0)
                        {
                            PrintCell(i.ToString());
                        }
                        else
                        {
                            // Row content
                            if (CheckForMine(i, j, minesHit))
                            {
                                BackgroundColor = ConsoleColor.Red;
                                PrintCell("X");
                                BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                if (IsCurrentCell(i, j, currentPosition))
                                {
                                    BackgroundColor = ConsoleColor.Yellow;
                                    PrintCell("-");
                                    BackgroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    PrintCell("-");
                                }
                            }
                        }
                    }
                }

                WriteLine();

                for (int j = 0; j < this.columnToPrint + 1; j++)
                {
                    Write("-----");
                }

                WriteLine();
            }
        }

        public void PrintInformation(int remainingLives, int totalNumberOfMoves, Point currentPosition)
        {
            WriteLine("\nPress arrow key to move Up, Down, Right and Left.\n");
            WriteLine($"Lives left: {remainingLives}");
            WriteLine($"Numbers of move: {totalNumberOfMoves}");
            WriteLine($"Position: {Convert.ToChar(65 + currentPosition.X)}{currentPosition.Y + 1}\n");
        }

        private bool CheckForMine(int row, int column, Point[] minesHit)
        {
            if (minesHit.Length > 0)
            {
                for (int i = 0; i < minesHit.Length; i++)
                {
                    if (minesHit[i].X >= 0 && minesHit[i].Y >= 0 &&
                        IsCurrentCell(row, column, minesHit[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsCurrentCell(int row, int column, Point position)
        {
            if (column == position.X + 1 && row == position.Y + 1)
            {
                return true;
            }
            return false;
        }

        private void PrintCell(string text)
        {
            Write($"{text.PadLeft(3),2} |");
        }
    }
}
