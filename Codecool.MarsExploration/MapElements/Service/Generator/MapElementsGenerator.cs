using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;

namespace Codecool.MarsExploration.MapElements.Service.Generator;

public class MapElementsGenerator : IMapElementsGenerator
{
    private readonly IMapElementBuilder _mapElementBuilder;

    public MapElementsGenerator(IMapElementBuilder mapElementBuilder)
    {
        _mapElementBuilder = mapElementBuilder;
    }    
        
    public IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig)
    {
        List<MapElement> mapElements = new List<MapElement>();
        var mapElementConfigs = mapConfig.MapElementConfigurations;
        foreach (MapElementConfiguration mapElementConfig in mapElementConfigs)
        {
            var elementConfigs = mapElementConfig.ElementsToDimensions;
            foreach (ElementToSize config in elementConfigs)
            {
                int size = config.Size;
                for (int i = 1; i <= size; i++)
                {
                    mapElements.Add(_mapElementBuilder.Build(size, mapElementConfig.Symbol, mapElementConfig.Name,
                        mapElementConfig.DimensionGrowth, mapElementConfig.PreferredLocationSymbol));
                }
            }
        }
        return mapElements;
    }
}