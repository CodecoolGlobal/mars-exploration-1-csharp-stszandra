using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Builder;

public class MapElementBuilder : IMapElementBuilder
{
    private readonly IDimensionCalculator _dimensionCalculator;
    private readonly ICoordinateCalculator _coordinateCalculator;

    public MapElementBuilder(IDimensionCalculator dimensionCalculator, ICoordinateCalculator coordinateCalculator)
    {
        _dimensionCalculator = dimensionCalculator;
        _coordinateCalculator = coordinateCalculator;
    }

    public MapElement Build(int size, string symbol, string name, int dimensionGrowth, string? preferredLocationSymbol = null)
    {
        int dimension = _dimensionCalculator.CalculateDimension(size, dimensionGrowth);
        string?[,] representation = new string?[dimension, dimension];
        for (int i = 1; i <= size; i++)
        {
            Coordinate coordinate = _coordinateCalculator.GetRandomCoordinate(dimension);
            while (representation[coordinate.X, coordinate.Y] != null)
            {
                coordinate = _coordinateCalculator.GetRandomCoordinate(dimension);
            }
        
            representation[coordinate.X, coordinate.Y] = symbol;
        }

        MapElement mapElement = new MapElement(representation, name, dimension, preferredLocationSymbol);
        return mapElement;
    }
}