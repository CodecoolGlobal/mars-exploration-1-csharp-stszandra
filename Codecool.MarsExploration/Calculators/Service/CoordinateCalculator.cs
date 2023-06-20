using Codecool.MarsExploration.Calculators.Model;

namespace Codecool.MarsExploration.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    public Coordinate GetRandomCoordinate(int dimension)
    {
        Random random = new Random();
        int randomXCoordinate = random.Next(0, dimension + 1);
        int randomYCoordinate = random.Next(0, dimension + 1);
        Coordinate coordinate = new Coordinate(randomXCoordinate, randomYCoordinate);
        return coordinate;
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension)
    {
        
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        
    }
}