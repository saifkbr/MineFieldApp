using System;
using System.Drawing;
using System.Linq;
using static System.Console;

namespace MineFieldApp
{
    public interface IPlayMineGame
    {
        void Play();
    }

    public class PlayMineGame : IPlayMineGame
    {
        private readonly IMineGrid _mineGrid;
        private readonly IMoves _moves;
        private readonly IMineGenerator _mineGenerator;
        private readonly Point[] _mineExploded;

        public PlayMineGame(IMineGrid mineGrid, IMoves moves, IMineGenerator mineGenerator, Point[] mineExploded)
        {
            _mineGrid = mineGrid;
            _moves = moves;
            _mineGenerator = mineGenerator;
            _mineExploded = mineExploded;
        }

        public void Play()
        {
            var currentPosition = new Point(0, 0);
            int numberOfMovesTaken = 0;
            var mineAddress = _mineGenerator.GenerateMinePoints();

            do
            {
                if (mineAddress.Any(x => x == currentPosition))
                {
                    var index = Array.IndexOf(_mineExploded, new Point(-1, -1));
                    if (index > -1)
                    {
                        _mineExploded[index] = currentPosition;
                    }
                }

                _mineGrid.ClearScreen();

                _mineGrid.PrintInformation(_mineExploded.Count(x => x.X == -1), numberOfMovesTaken++, currentPosition);

                _mineGrid.PrintGrid(_mineExploded, currentPosition);

                currentPosition = _moves.CaptureMoves(currentPosition, ReadKey());
            }

            while (_mineExploded.Any(x => x.X == -1));
        }
    }
}
