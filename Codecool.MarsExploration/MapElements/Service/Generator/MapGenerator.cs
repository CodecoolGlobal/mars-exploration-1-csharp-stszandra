using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Placer;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    private readonly IDimensionCalculator _dimensionCalculator;
    private readonly IMapElementsGenerator _mapElementsGenerator;
    private readonly IMapElementPlacer _mapElementPlacer;

    public MapGenerator(IDimensionCalculator dimensionCalculator, IMapElementsGenerator mapElementsGenerator, IMapElementPlacer mapElementPlacer)
    {
        _dimensionCalculator = dimensionCalculator;
        _mapElementsGenerator = mapElementsGenerator;
        _mapElementPlacer = mapElementPlacer;
    }
    
    public Map Generate(MapConfiguration mapConfig)
    {
        int mapDimension = _dimensionCalculator.CalculateDimension(mapConfig.MapSize, 0);
        string?[,] mapRepresentation = new string?[mapDimension, mapDimension];
        
        List<MapElement> allElementsToPlace = (List<MapElement>)_mapElementsGenerator.CreateAll(mapConfig);
        bool isElementPlaced = false;
        //int elementIndexToRemove = -1;
        
        while (allElementsToPlace.Count > 0)
        {
            MapElement elementToPlace = allElementsToPlace.OrderBy(element => element.Dimension).Last();
            int elementDimension = elementToPlace.Dimension;
            for (int y = 0; y <= mapDimension - elementDimension + 1; y++)
            {
                for (int x = 0; x <= mapDimension - elementDimension + 1; x++)
                {
                    Coordinate topLeftCornerOfElement = new Coordinate(x, y);
                    if (_mapElementPlacer.CanPlaceElement(elementToPlace, mapRepresentation, topLeftCornerOfElement))
                    {
                        _mapElementPlacer.PlaceElement(elementToPlace, mapRepresentation, topLeftCornerOfElement);
                        //allElementsToPlace.Remove(elementToPlace);
                        isElementPlaced = true;
                        //elementIndexToRemove = allElementsToPlace.IndexOf(elementToPlace);
                        break;
                    }
                    
                }
                if (isElementPlaced)
                {
                    break;
                }
            }
            if (isElementPlaced)
            {
                //allElementsToPlace.RemoveAt(elementIndexToRemove);
                allElementsToPlace.Remove(elementToPlace);
                isElementPlaced = false;
                //elementIndexToRemove = -1;
            }
        }
        Map map = new Map(mapRepresentation);
        return map;
    }
}