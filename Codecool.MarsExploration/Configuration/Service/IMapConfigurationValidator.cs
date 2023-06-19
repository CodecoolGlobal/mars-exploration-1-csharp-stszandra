using Codecool.MarsExploration.Configuration.Model;

namespace Codecool.MarsExploration.Configuration.Service;

public interface IMapConfigurationValidator
{
    bool Validate(MapConfiguration mapConfig);
    public bool IsDimensionGrowthOk(MapConfiguration mapConfig);

    public bool IsPreferredPlacementOk(MapConfiguration mapConfig);

    public bool DoSymbolAndNameMatch(MapConfiguration mapConfig);

    public bool SingleOrMultiDimensionalOk(MapConfiguration mapConfig);

    public bool TotalNumberOfElementsOk(MapConfiguration mapConfig);
}
