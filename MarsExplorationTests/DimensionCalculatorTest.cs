using Codecool.MarsExploration.Calculators.Service;

namespace MarsExplorationTests;

[TestFixture]
public class DimensionCalculatorTest
{
    private readonly IDimensionCalculator _dimensionCalculator = new DimensionCalculator();


    private static readonly object[] TestCases =
    {
        new object[] {4, 2, 4},
        new object[] {20, 3, 8},
        new object[] {1, 0, 1},
        new object[] {30, 4, 10}
    };
    [TestCaseSource(nameof(TestCases))]
    public void Test_CalculateDimension_(int size, int dimensionGrowth, int expectedResult)
    {
        Assert.That(_dimensionCalculator.CalculateDimension(size, dimensionGrowth), Is.EqualTo(expectedResult));
    }
    
}