using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service;

namespace MarsExplorationTests;

public class MapConfigurationValidatorTest
{
    private readonly IMapConfigurationValidator _mapConfigurationValidator = new MapConfigurationValidator();
    private static MapConfiguration? _correctMapConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(2, 20),
                new ElementToSize(1, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(1, 5),
                new ElementToSize(1, 10),
            }, 10),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(8, 1)
            }, 0, "&")});
    
    
    
    private static MapConfiguration? _badDimensionGrowthMapConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(2, 20),
                new ElementToSize(1, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(1, 5),
                new ElementToSize(1, 10),
            }, 0),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(8, 1)
            }, 0, "&")});
    
    private static MapConfiguration[] _badDimensionGrowth =
    {
        _correctMapConfig,
        _badDimensionGrowthMapConfig,
    };
    
    [TestCaseSource(nameof(_badDimensionGrowth))]
    public void Test_IsDimensionGrowthOk(MapConfiguration config)
    {
        Assert.That(_mapConfigurationValidator.IsDimensionGrowthOk(config));
    }
    
    private static MapConfiguration? _badPreferredPlaceConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(2, 20),
                new ElementToSize(1, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(1, 5),
                new ElementToSize(1, 10),
            }, 10),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "&"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(8, 1)
            }, 0, "&")});
    
    private static MapConfiguration[] _badPreferredPlace =
    {
        _correctMapConfig,
        _badPreferredPlaceConfig,
    };
    
    [TestCaseSource(nameof(_badPreferredPlace))]
    public void Test_IsPreferredPlacementOk(MapConfiguration config)
    {
        Assert.That(_mapConfigurationValidator.IsPreferredPlacementOk(config));
    }
    
    private static MapConfiguration? _badSymbolAndNameConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(2, 20),
                new ElementToSize(1, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(1, 5),
                new ElementToSize(1, 10),
            }, 10),
            new MapElementConfiguration("!", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(8, 1)
            }, 0, "&")});

    
    private static MapConfiguration[] _badSymbolAndName =
    {
        _correctMapConfig,
        _badSymbolAndNameConfig,
    };
    
    [TestCaseSource(nameof(_badSymbolAndName))]
    public void Test_DoSymbolAndNameMatch(MapConfiguration config)
    {
        Assert.That(_mapConfigurationValidator.DoSymbolAndNameMatch(config));
    }
    
    private static MapConfiguration? _badSingleOrMultiDimensionConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(2, 20),
                new ElementToSize(1, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(1, 5),
                new ElementToSize(1, 10),
            }, 10),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(10, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(8, 20)
            }, 0, "&")});
    
    private static MapConfiguration[] _badSingleOrMultiDimension =
    {
        _correctMapConfig,
        _badSingleOrMultiDimensionConfig,
    };
    
    [TestCaseSource(nameof(_badSingleOrMultiDimension))]
    public void Test_SingleOrMultiDimensionalOk(MapConfiguration config)
    {
        Assert.That(_mapConfigurationValidator.SingleOrMultiDimensionalOk(config));
    }
    
    private static MapConfiguration? _badNumberOfElementsConfig = new MapConfiguration(1000, 0.5, 
        new List<MapElementConfiguration>() { 
            new MapElementConfiguration("#", "mountain", new[]
            {
                new ElementToSize(100, 20),
                new ElementToSize(200, 30),
            }, 3),
            new MapElementConfiguration("&", "pit", new[]
            {
                new ElementToSize(200, 5),
                new ElementToSize(250, 10),
            }, 10),
            new MapElementConfiguration("%", "mineral", new[]
            {
                new ElementToSize(100, 1)
            }, 0, "#"),
            new MapElementConfiguration("*", "water", new[]
            {
                new ElementToSize(800, 1)
            }, 0, "&")});
    
    private static MapConfiguration[] _badNumberOfElements =
    {
        _correctMapConfig,
        _badNumberOfElementsConfig,
    };

    [TestCaseSource(nameof(_badNumberOfElements))]
    public void Test_TotalNumberOfElementsOk(MapConfiguration config)
    {
        Assert.That(_mapConfigurationValidator.TotalNumberOfElementsOk(config));
    }
    
}