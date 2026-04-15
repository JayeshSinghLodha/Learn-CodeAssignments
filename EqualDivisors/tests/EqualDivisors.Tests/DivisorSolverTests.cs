using EqualDivisors;

namespace EqualDivisors.Tests;

public class DivisorCounterTests
{
    [Fact]
    public void CountDivisors_Of1_Returns1()
    {
        Assert.Equal(1, DivisorSolver.CountDivisors(1));
    }

    [Fact]
    public void CountDivisors_OfPrime_Returns2()
    {
        Assert.Equal(2, DivisorSolver.CountDivisors(7));
    }

    [Fact]
    public void CountDivisors_Of4_Returns3()
    {
        Assert.Equal(3, DivisorSolver.CountDivisors(4));
    }

    [Fact]
    public void CountDivisors_Of12_Returns6()
    {
        Assert.Equal(6, DivisorSolver.CountDivisors(12));
    }

    [Theory]
    [InlineData(2,  2)]
    [InlineData(3,  2)]
    [InlineData(6,  4)]
    [InlineData(14, 4)]
    [InlineData(15, 4)]
    [InlineData(36, 9)]
    public void CountDivisors_KnownValues_ReturnsExpected(int n, int expected)
    {
        Assert.Equal(expected, DivisorSolver.CountDivisors(n));
    }
}

public class CountValidPairsTests
{
    [Fact]
    public void CountValidPairs_WhenKIs1_Returns0()
    {
        Assert.Equal(0, DivisorSolver.CountValidPairs(1));
    }

    [Fact]
    public void CountValidPairs_WhenKIs2_Returns0()
    {
        Assert.Equal(0, DivisorSolver.CountValidPairs(2));
    }

    [Fact]
    public void CountValidPairs_WhenKIs3_Returns1()
    {
        Assert.Equal(1, DivisorSolver.CountValidPairs(3));
    }

    [Fact]
    public void CountValidPairs_WhenKIs4_Returns1()
    {
        Assert.Equal(1, DivisorSolver.CountValidPairs(4));
    }

    [Fact]
    public void CountValidPairs_WhenKIs15_Returns2()
    {
        Assert.Equal(2, DivisorSolver.CountValidPairs(15));
    }

    [Fact]
    public void CountValidPairs_WhenKIs100_Returns15()
    {
        Assert.Equal(15, DivisorSolver.CountValidPairs(100));
    }

    [Theory]
    [InlineData(3,   1)]
    [InlineData(15,  2)]
    [InlineData(22,  3)]
    [InlineData(27,  4)]
    [InlineData(100, 15)]
    public void CountValidPairs_MultipleKValues_ReturnsExpected(int k, int expected)
    {
        Assert.Equal(expected, DivisorSolver.CountValidPairs(k));
    }

    [Fact]
    public void Solve_MultipleTestCases_ReturnsCorrectResults()
    {
        int[] inputs   = [15, 3, 100, 2];
        int[] expected = [2,  1,  15, 0];

        int[] results = inputs.Select(DivisorSolver.CountValidPairs).ToArray();

        Assert.Equal(expected, results);
    }
}
