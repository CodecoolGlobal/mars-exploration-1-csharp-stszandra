using System.Runtime.InteropServices;
using Codecool.MarsExploration.Configuration.Model;

namespace Codecool.MarsExploration.Configuration.Service;

public class MapConfigurationValidator : IMapConfigurationValidator
{
    private int TotalNumberOfElements { get; set; } = 0;
    
    public bool Validate(MapConfiguration mapConfig)
    {
        bool isMapConfigValid = true;
        int totalNumberOfElements = 0;
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            //Is dimension growth OK?
            if (config.Name == "mountain" && config.DimensionGrowth != 3
                || config.Name == "pit" && config.DimensionGrowth != 10
                || config.Name == "mineral" && config.DimensionGrowth != 1
                || config.Name == "water" && config.DimensionGrowth != 1)
            {
                isMapConfigValid = false;
            }
            //Preferred placement OK?
            if (config.Name == "mountain" && config.PreferredLocationSymbol != null
                || config.Name == "pit" && config.PreferredLocationSymbol != null
                || config.Name == "mineral" && config.PreferredLocationSymbol != "#"
                || config.Name == "water" && config.PreferredLocationSymbol != "&")
            {
                isMapConfigValid = false;
            }
            //Do Symbol and Name match?
            if (config.Name == "mountain" && config.Symbol != "#"
                || config.Name == "pit" && config.Symbol != "&"
                || config.Name == "mineral" && config.Symbol != "%"
                || config.Name == "water" && config.Symbol != "*")
            {
                isMapConfigValid = false;
            }
            //Single or Multidimensional correctly chosen?
            foreach (ElementToSize elementToSize in config.ElementsToDimensions)
            {
                if (config.Name == "mountain" && elementToSize.Size <= 1
                    || config.Name == "pit" && elementToSize.Size <= 1
                    || config.Name == "mineral" && (elementToSize.Size < 1 || elementToSize.Size > 1)
                    || config.Name == "water" && (elementToSize.Size < 1 || elementToSize.Size > 1))
                {
                    isMapConfigValid = false;
                }
            }
            //Total number of elements not greater than the defined ElementToSpaceRatio?    
            foreach (ElementToSize elementToSize in config.ElementsToDimensions)
            {
                TotalNumberOfElements += elementToSize.ElementCount * elementToSize.Size;
            }
        }
        //mapConfig.MapSize = elements + spaces
        //elements = spaces * ratio
        //spaces = elements / ratio
        //mapConfig.MapSize = elements + elements / ratio
        //mapConfig.MapSize = elements (1 + 1/ratio)
        //mapConfig.MapSize = elements (ratio/ratio + 1/ratio)
        //mapConfig.MapSize = elements ((ratio + 1) / ratio)
        //elements = mapConfig.MapSize / ((ratio + 1) / ratio)
        if (TotalNumberOfElements >
            mapConfig.MapSize / ((mapConfig.ElementToSpaceRatio + 1) / mapConfig.ElementToSpaceRatio))
        {
            isMapConfigValid = false;
        }
        return isMapConfigValid;
    }
    
}