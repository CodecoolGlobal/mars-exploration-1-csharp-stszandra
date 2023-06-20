using Codecool.MarsExploration.Calculators.Model;

namespace Codecool.MarsExploration.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    public Coordinate GetRandomCoordinate(int dimension)
    {
        Random random = new Random();
        int randomXCoordinate = random.Next(0, dimension);
        int randomYCoordinate = random.Next(0, dimension);
        Coordinate coordinate = new Coordinate(randomXCoordinate, randomYCoordinate);
        return coordinate;
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension)
    {
        List<Coordinate> adjacentCoordinates = new List<Coordinate>();
        if (coordinate.X != dimension - 1)
        {
            adjacentCoordinates.Add(coordinate with { X = coordinate.X + 1 }); 
        }
        if (coordinate.X != 0)
        {
            adjacentCoordinates.Add(coordinate with {X = coordinate.X - 1});
        }
        if (coordinate.Y != 0)
        {
            adjacentCoordinates.Add(coordinate with {Y = coordinate.Y - 1});
        }
        if (coordinate.Y != dimension - 1)
        {
            adjacentCoordinates.Add(coordinate with {Y = coordinate.Y + 1});
        }

        return adjacentCoordinates;
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        List<Coordinate> adjacentCoordinates = new List<Coordinate>();

        foreach (Coordinate coordinate in coordinates)
        {
            //Adds the neighbours to the list if they aren't already in it.
            adjacentCoordinates.AddRange(GetAdjacentCoordinates(coordinate, dimension).Except(adjacentCoordinates));
        }

        return adjacentCoordinates;
    }
}