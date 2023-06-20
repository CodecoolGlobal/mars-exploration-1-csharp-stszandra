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
        Coordinate rightNeighbour = null;
        if (coordinate.X != dimension - 1)
        {
            rightNeighbour = coordinate with { X = coordinate.X + 1 };
        }
        Coordinate leftNeighbour = null;
        if (coordinate.X != 0)
        {
            rightNeighbour = coordinate with {X = coordinate.X - 1};
        }
        Coordinate topNeighbour = null;
        if (coordinate.X != 0)
        {
            topNeighbour = coordinate with {Y = coordinate.Y - 1};
        }
        Coordinate bottomNeighbour = null;
        if (coordinate.X != 0)
        {
            bottomNeighbour = coordinate with {Y = coordinate.Y + 1};
        }

        //IEnumerable<Coordinate> adjacentCoordinates = new List<Coordinate>();
        List<Coordinate> adjacentCoordinates = new List<Coordinate>();
        if (rightNeighbour != null)
        {
            adjacentCoordinates.Add(rightNeighbour); 
        }
        if (leftNeighbour != null)
        {
            adjacentCoordinates.Add(leftNeighbour); 
        }
        if (topNeighbour != null)
        {
            adjacentCoordinates.Add(topNeighbour); 
        }
        if (bottomNeighbour != null)
        {
            adjacentCoordinates.Add(bottomNeighbour); 
        }

        return adjacentCoordinates;
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        
    }
}