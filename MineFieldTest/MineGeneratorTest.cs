using FluentAssertions;
using MineFieldApp;
using System.Linq;
using Xunit;

namespace MineFieldTest
{
    public class MineGeneratorTest
    {
        [Theory]
        [InlineData(5, 5, 3)]
        [InlineData(4, 4, 3)]
        [InlineData(10, 10, 8)]
        public void GenerateMinePoints_Valid_Row_Column_Lifeline_Should_Return_Array_With_Elements(int row, int column, int lifeline)
        {
            var points = new MineGenerator(row, column, lifeline).GenerateMinePoints();

            points.Length.Should().BeGreaterOrEqualTo(lifeline);
            points.GroupBy(p => p.X).Where(g => g.Count() > 1).Select(s => s.Key).Count().Should().Be(0);
            points.GroupBy(p => p.Y).Where(g => g.Count() > 1).Select(s => s.Key).Count().Should().Be(0);
        }

        [Theory]
        [InlineData(-1, -1, -1, 0)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 1, 1, 0)]
        public void GenerateMinePoints_Invalid_Row_Column_Lifeline_Should_Return_Empty_Array(int row, int column, int lifeline, int expectedOutput)
        {
            var points = new MineGenerator(row, column, lifeline).GenerateMinePoints();

            points.Length.Should().Be(expectedOutput);
        }
    }
}
