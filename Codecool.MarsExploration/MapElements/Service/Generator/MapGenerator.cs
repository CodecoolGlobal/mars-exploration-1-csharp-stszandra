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
    private readonly ICoordinateCalculator _coordinateCalculator;
    private static Random random = new Random();

    public MapGenerator(IDimensionCalculator dimensionCalculator, IMapElementsGenerator mapElementsGenerator, IMapElementPlacer mapElementPlacer, ICoordinateCalculator coordinateCalculator)
    {
        _dimensionCalculator = dimensionCalculator;
        _mapElementsGenerator = mapElementsGenerator;
        _mapElementPlacer = mapElementPlacer;
        _coordinateCalculator = coordinateCalculator;
    }

    private List<Coordinate> GetPreferredLocationCoordinates(string?[,] map, string symbol)
    {
        List<Coordinate> preferredCoordinates = new List<Coordinate>();
        
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == symbol)
                {
                    preferredCoordinates.Add(new Coordinate(x,y));
                }
            }
        }

        return preferredCoordinates;
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
            if (elementToPlace.PreferredLocationSymbol == null)
            {
              for (int y = 0; y <= mapDimension - elementDimension + 1; y++)
              { 
                  for (int x = 0; x <= mapDimension - elementDimension + 1; x++) 
                  { 
                      Coordinate topLeftCornerOfElement = new Coordinate(x, y); 
                      if (_mapElementPlacer.CanPlaceElement(elementToPlace, mapRepresentation, topLeftCornerOfElement)) 
                      { 
                          _mapElementPlacer.PlaceElement(elementToPlace, mapRepresentation, topLeftCornerOfElement); 
                          //allElementsToPlace.Remove(elementToPlace);
                          //isElementPlaced = true;
                          //elementIndexToRemove = allElementsToPlace.IndexOf(elementToPlace);
                          break;
                      }
                                  
                  } 
                  if (isElementPlaced) 
                  { 
                      break;
                  }
              }  
            }
            else if (elementToPlace.Name == "water")
            {
                List<Coordinate> adjacentLocations = (List<Coordinate>)_coordinateCalculator.GetAdjacentCoordinates(GetPreferredLocationCoordinates(mapRepresentation, "&"), mapDimension);
                foreach (Coordinate adjacentCoordinate in adjacentLocations)
                {
                    if (mapRepresentation[adjacentCoordinate.Y, adjacentCoordinate.X] != null)
                    {
                        adjacentLocations.Remove(adjacentCoordinate);
                    }
                }
                if (adjacentLocations.Count > 0)
                {
                    int indexOfCoordinate = random.Next(0, adjacentLocations.Count);
                    mapRepresentation[adjacentLocations[indexOfCoordinate].Y, adjacentLocations[indexOfCoordinate].X] =
                        "*";
                    
                }
                else
                {
                    Coordinate randomCoordinate = new Coordinate(random.Next(0, mapDimension),random.Next(0, mapDimension));
                    while (mapRepresentation[randomCoordinate.Y, randomCoordinate.X] != null)
                    {
                        randomCoordinate = new Coordinate(random.Next(0, mapDimension),random.Next(0, mapDimension));
                    }

                    mapRepresentation[randomCoordinate.Y, randomCoordinate.X] = "*";
                }

                isElementPlaced = true;
            } else if (elementToPlace.Name == "mineral")
            {
                List<Coordinate> adjacentLocations = (List<Coordinate>)_coordinateCalculator.GetAdjacentCoordinates(GetPreferredLocationCoordinates(mapRepresentation, "#"), mapDimension);
                foreach (Coordinate adjacentCoordinate in adjacentLocations)
                {
                    if (mapRepresentation[adjacentCoordinate.Y, adjacentCoordinate.X] != null)
                    {
                        adjacentLocations.Remove(adjacentCoordinate);
                    }
                }
                if (adjacentLocations.Count > 0)
                {
                    int indexOfCoordinate = random.Next(0, adjacentLocations.Count);
                    mapRepresentation[adjacentLocations[indexOfCoordinate].Y, adjacentLocations[indexOfCoordinate].X] =
                        "%";
                }
                else
                {
                    Coordinate randomCoordinate = new Coordinate(random.Next(0, mapDimension),random.Next(0, mapDimension));
                    while (mapRepresentation[randomCoordinate.Y, randomCoordinate.X] != null)
                    {
                        randomCoordinate = new Coordinate(random.Next(0, mapDimension),random.Next(0, mapDimension));
                    }

                    mapRepresentation[randomCoordinate.Y, randomCoordinate.X] = "%"; 
                }

                isElementPlaced = true;
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