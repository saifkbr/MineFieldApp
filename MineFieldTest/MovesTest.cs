using FluentAssertions;
using MineFieldApp;
using System;
using System.Drawing;
using Xunit;

namespace MineFieldTest
{
    public class MovesTest
    {
        private const int rows = 5;
        private const int columns = 5;

        // Test data for valid moves
        public static TheoryData<(int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition)> validMoves
            = new TheoryData<(int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition)>
        {
            (rows,columns,new Point{X=2,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.RightArrow,false,false,false),new Point{X=3,Y=2} ),
            (rows,columns,new Point{X=2,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.DownArrow,false,false,false),new Point{X=2,Y=3} ),
            (rows,columns,new Point{X=2,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.LeftArrow,false,false,false),new Point{X=1,Y=2} ),
            (rows,columns,new Point{X=2,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.UpArrow,false,false,false),new Point{X=2,Y=1} )
        };

        // Test data to test edge case scenario
        public static TheoryData<(int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition)> edgeCase
            = new TheoryData<(int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition)>
        {
            (rows,columns,new Point{X=5,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.RightArrow,false,false,false),new Point{X=5,Y=2} ),
            (rows,columns,new Point{X=2,Y=5}, new ConsoleKeyInfo('0',ConsoleKey.DownArrow,false,false,false),new Point{X=2,Y=5} ),
            (rows,columns,new Point{X=0,Y=2}, new ConsoleKeyInfo('0',ConsoleKey.LeftArrow,false,false,false),new Point{X=0,Y=2} ),
            (rows,columns,new Point{X=2,Y=0}, new ConsoleKeyInfo('0',ConsoleKey.UpArrow,false,false,false),new Point{X=2,Y=0} )
        };

        [Theory]
        [MemberData(nameof(validMoves))]
        public void CaptureMoves_Moves_Within_Range_Change_XorY_Value_By_1((int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition) input)
        {
            var moves = new Moves(input.row, input.column);

            var actual = moves.CaptureMoves(input.currentPosition, input.consoleKeyInfo);

            actual.Should().Be(input.expectedPosition);
        }

        [Theory]
        [MemberData(nameof(edgeCase))]
        public void CaptureMoves_Moves_Outside_Range_XorY_Remain_Same((int row, int column, Point currentPosition, ConsoleKeyInfo consoleKeyInfo, Point expectedPosition) input)
        {
            var moves = new Moves(input.row, input.column);

            var actual = moves.CaptureMoves(input.currentPosition, input.consoleKeyInfo);

            actual.Should().Be(input.expectedPosition);
        }
    }
}
