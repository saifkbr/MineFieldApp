using System;
using System.Drawing;
using static System.Console;

namespace MineFieldApp
{
    public interface IMoves
    {
        Point CaptureMoves(Point currentPosition, ConsoleKeyInfo consoleKeyInfo);
    }

    public class Moves : IMoves
    {
        private readonly int rightMoveLimit;
        private readonly int downMoveLimit;

        public Moves(int rightMoveLimit, int downMoveLimit)
        {
            this.rightMoveLimit = rightMoveLimit;
            this.downMoveLimit = downMoveLimit;
        }

        public Point CaptureMoves(Point currentPosition, ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentPosition.Y > 0)
                    {
                        currentPosition.Y--;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    if (currentPosition.Y < this.downMoveLimit - 1)
                    {
                        currentPosition.Y++;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    if (currentPosition.X < this.rightMoveLimit - 1)
                    {
                        currentPosition.X++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentPosition.X > 0)
                    {
                        currentPosition.X--;
                    }
                    break;
            }

            return currentPosition;
        }
    }
}
