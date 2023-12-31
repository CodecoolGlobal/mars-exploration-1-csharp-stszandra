﻿using System.Runtime.InteropServices;
using Codecool.MarsExploration.Configuration.Model;

namespace Codecool.MarsExploration.Configuration.Service;

public class MapConfigurationValidator : IMapConfigurationValidator
{
    private int TotalNumberOfElements { get; set; }

    public MapConfigurationValidator()
    {
        TotalNumberOfElements = 0;
    }
    
    public bool Validate(MapConfiguration mapConfig)
    {
        return IsDimensionGrowthOk(mapConfig) && IsPreferredPlacementOk(mapConfig) && DoSymbolAndNameMatch(mapConfig) &&
               SingleOrMultiDimensionalOk(mapConfig) && TotalNumberOfElementsOk(mapConfig);
    }

    public bool IsDimensionGrowthOk(MapConfiguration mapConfig)
    {
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            if (config.Name == "mountain" && config.DimensionGrowth != 3
                || config.Name == "pit" && config.DimensionGrowth != 10
                || config.Name == "mineral" && config.DimensionGrowth != 0
                || config.Name == "water" && config.DimensionGrowth != 0)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsPreferredPlacementOk(MapConfiguration mapConfig)
    {
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            if (config.Name == "mountain" && config.PreferredLocationSymbol != null
                || config.Name == "pit" && config.PreferredLocationSymbol != null
                || config.Name == "mineral" && config.PreferredLocationSymbol != "#"
                || config.Name == "water" && config.PreferredLocationSymbol != "&")
            {
                return false;
            }
        }

        return true;
       
    }

    public bool DoSymbolAndNameMatch(MapConfiguration mapConfig)
    {
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            if (config.Name == "mountain" && config.Symbol != "#"
                || config.Name == "pit" && config.Symbol != "&"
                || config.Name == "mineral" && config.Symbol != "%"
                || config.Name == "water" && config.Symbol != "*")
            {
                return false;
            }
        }

        return true;

    }

    public bool SingleOrMultiDimensionalOk(MapConfiguration mapConfig)
    {
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            foreach (ElementToSize elementToSize in config.ElementsToDimensions)
            {
                if (config.Name == "mountain" && elementToSize.Size <= 1
                    || config.Name == "pit" && elementToSize.Size <= 1
                    || config.Name == "mineral" && (elementToSize.Size < 1 || elementToSize.Size > 1)
                    || config.Name == "water" && (elementToSize.Size < 1 || elementToSize.Size > 1))
                {
                    return  false;
                }
            }
        }
        return true;
    }

    public bool TotalNumberOfElementsOk(MapConfiguration mapConfig)
    {
        foreach (MapElementConfiguration config in mapConfig.MapElementConfigurations)
        {
            foreach (ElementToSize elementToSize in config.ElementsToDimensions)
            {
                TotalNumberOfElements += elementToSize.ElementCount * elementToSize.Size;
            }
        }

        double actualNumberOfSpaces = mapConfig.MapSize - TotalNumberOfElements;
        double requiredNumberOfSpaces = TotalNumberOfElements / mapConfig.ElementToSpaceRatio;
        if (requiredNumberOfSpaces > actualNumberOfSpaces)
        {
            TotalNumberOfElements = 0;
            return false;
        }
        TotalNumberOfElements = 0;
        return true;
    }
}