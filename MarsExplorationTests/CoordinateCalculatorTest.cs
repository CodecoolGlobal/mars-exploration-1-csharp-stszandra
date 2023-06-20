using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;

namespace MarsExplorationTests;

public class CoordinateCalculatorTest
{
    private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();


    private static readonly object[] DimensionsForRandomCoordinates =
    {
        new object[] {4},
        new object[] {20},
        new object[] {1},
        new object[] {30}
    };
    
    private static readonly object[] Corners =
    {
        new object[] {new Coordinate(0,0), 2, new List<Coordinate>() { new Coordinate(1, 0), new Coordinate(0, 1) }},
        new object[] {new Coordinate(1,0), 2, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(1, 1) }},
        new object[] {new Coordinate(0,1), 2, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(1, 1) }},
        new object[] {new Coordinate(1,1), 2, new List<Coordinate>() { new Coordinate(0, 1), new Coordinate(1, 0) }},
    };
    
    private static readonly object[] Edges =
    {
        new object[] {new Coordinate(1,0), 3, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(2, 0), new Coordinate(1, 1) }},
        new object[] {new Coordinate(0,1), 3, new List<Coordinate>() { new Coordinate(0, 0), new Coordinate(0, 2), new Coordinate(1, 1) }},
    };
    
    private static readonly object[] Middle =
    {
        new object[] {new Coordinate(1,1), 3, new List<Coordinate>() { new Coordinate(1, 0), new Coordinate(1, 2), new Coordinate(0, 1), new Coordinate(2, 1) }},
    };
    
    private static readonly object[] NeighborsToMultipleCoordinates =
    {
        new object[] {new List<Coordinate>(){ new Coordinate(0, 0), new Coordinate(1, 1), new Coordinate(2, 2) }, 5, new List<Coordinate>() { new Coordinate(1, 0), new Coordinate(0, 1), new Coordinate(2, 1), new Coordinate(1, 2), new Coordinate(3, 2), new Coordinate(2, 3) }},
    };

    [TestCaseSource(nameof(DimensionsForRandomCoordinates))]
    public void Test_GetRandomCoordinate(int dimension)
    {
        Coordinate generatedCoordinate = _coordinateCalculator.GetRandomCoordinate(dimension);
        
        Assert.That(generatedCoordinate.X >= 0 && generatedCoordinate.X < dimension && generatedCoordinate.Y >= 0 && generatedCoordinate.Y < dimension);
    }

    [TestCaseSource(nameof(Corners))]
    public void Test_GetAdjacentCoordinates1_Corner(Coordinate coordinate, int dimension, List<Coordinate> neighbors)
    {
        Assert.That(_coordinateCalculator.GetAdjacentCoordinates(coordinate, dimension).Intersect(neighbors).Count() == 2);
    }
    
    [TestCaseSource(nameof(Edges))]
    public void Test_GetAdjacentCoordinates1_Edges(Coordinate coordinate, int dimension, List<Coordinate> neighbors)
    {
        Assert.That(_coordinateCalculator.GetAdjacentCoordinates(coordinate, dimension).Intersect(neighbors).Count() == 3);
    }
    
    [TestCaseSource(nameof(Middle))]
    public void Test_GetAdjacentCoordinates1_Middle(Coordinate coordinate, int dimension, List<Coordinate> neighbors)
    {
        Assert.That(_coordinateCalculator.GetAdjacentCoordinates(coordinate, dimension).Intersect(neighbors).Count() == 4);
    }

    [TestCaseSource(nameof(NeighborsToMultipleCoordinates))]
    public void Test_GetAdjacentCoordinates2(List<Coordinate> coordinates, int dimension, List<Coordinate> neighbors)
    {
        var calculatedNeighbors = _coordinateCalculator.GetAdjacentCoordinates(coordinates, dimension);
        Assert.That(calculatedNeighbors.Except(neighbors).Count().Equals(neighbors.Except(calculatedNeighbors).Count()));
    }
}