using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;

namespace MarsExplorationTests;

public class CoordinateCalculatorTest
{
    private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();


    private static readonly object[] TestCases1 =
    {
        new object[] {4},
        new object[] {20},
        new object[] {1},
        new object[] {30}
    };
    
    private static readonly object[] TestCases2 =
    {
        new object[] {new Coordinate(0,0), 2, new List<Coordinate>{new Coordinate(1, 0), new Coordinate(0, 1)}},
        new object[] {new Coordinate(0,2), 3, new List<Coordinate>{new Coordinate(0, 1), new Coordinate(1, 2)}},
    };
    
    private static readonly object[] TestCases3 =
    {
        new object[] {4},
        new object[] {20},
        new object[] {1},
        new object[] {30}
    };
    
    private static readonly object[] TestCases4 =
    {
        new object[] {4},
        new object[] {20},
        new object[] {1},
        new object[] {30}
    };

    [TestCaseSource(nameof(TestCases1))]
    public void Test_GetRandomCoordinate(int dimension)
    {
        Coordinate generatedCoordinate = _coordinateCalculator.GetRandomCoordinate(dimension);
        
        Assert.That(generatedCoordinate.X >= 0 && generatedCoordinate.X < dimension && generatedCoordinate.Y >= 0 && generatedCoordinate.Y < dimension);
    }

    [TestCaseSource(nameof(TestCases2))]
    public void Test_GetAdjacentCoordinates1_Corner(Coordinate coordinate, int dimension, IEnumerable<Coordinate> expectedResult)
    {
        Assert.That(expectedResult.Equals((IEnumerable<Coordinate>)_coordinateCalculator.GetAdjacentCoordinates(coordinate, dimension)));
    }
    
    // [TestCaseSource(nameof(TestCases3))]
    // public void Test_GetAdjacentCoordinates1_Middle((Coordinate coordinate, int dimension, IEnumerable<Coordinate> expectedResult)
    // {
    //     
    // }
    //
    // [TestCaseSource(nameof(TestCases4))]
    // public void Test_GetAdjacentCoordinates1_Side((Coordinate coordinate, int dimension, IEnumerable<Coordinate> expectedResult)
    // {
    //     
    // }
}