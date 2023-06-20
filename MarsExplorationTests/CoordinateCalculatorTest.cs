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
    
    private static readonly object[] TestCorners =
    {
        new object[] {new Coordinate(0,0), 2, new List<Coordinate>() { new Coordinate(1, 0), new Coordinate(0, 1) }},
        new object[] {new Coordinate(1,0), 2, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(1, 1) }},
        new object[] {new Coordinate(0,1), 2, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(1, 1) }},
        new object[] {new Coordinate(1,1), 2, new List<Coordinate>() { new Coordinate(0, 1), new Coordinate(1, 0) }},
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

    [TestCaseSource(nameof(TestCorners))]
    public void Test_GetAdjacentCoordinates1_Corner(Coordinate coordinate, int dimension, List<Coordinate> neighbors)
    {
        Assert.That(_coordinateCalculator.GetAdjacentCoordinates(coordinate, dimension).Intersect(neighbors).Count() == 2);
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