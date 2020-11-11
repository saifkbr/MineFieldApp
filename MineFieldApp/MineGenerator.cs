using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MineFieldApp
{
    public interface IMineGenerator
    {
        Point[] GenerateMinePoints();
    }

    public class MineGenerator : IMineGenerator
    {
        private readonly int rows;
        private readonly int columns;
        private readonly int lifeline;

        public MineGenerator(int rows, int columns, int lifeline)
        {
            this.rows = rows;
            this.columns = columns;
            this.lifeline = lifeline;
        }

        public Point[] GenerateMinePoints()
        {
            List<Point> points = new List<Point>();

            var retryCounter = 0;
            Random random = new Random();

            // Check if life and cells ratio is enjoyable
            var limit = ((rows * columns) / 4) >= lifeline ? lifeline : 0;

            while (points.Count <= limit && limit > 0 && retryCounter < 100)
            {
                retryCounter++;
                var point = new Point { X = random.Next(0, columns), Y = random.Next(0, rows) };

                if (!points.Any(x => x.X == point.X || x.Y == point.Y) && (point.X != 0 && point.Y != 0))
                {
                    points.Add(point);
                }

            };

            return points.ToArray();
        }
    }
}
