using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.MapElements.Model;

namespace Codecool.MarsExploration.MapElements.Service.Placer;

public class MapElementPlacer : IMapElementPlacer
{
    public bool CanPlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        int dimension = element.Dimension;
        for (int y = coordinate.Y; y < coordinate.Y + dimension - 1; y++)
        {
            for (int x = coordinate.X; x < coordinate.X + dimension - 1; x++)
            {
                if (map[y, x] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        
    }
}