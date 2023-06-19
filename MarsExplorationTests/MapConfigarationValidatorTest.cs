using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service;

namespace MarsExplorationTests;

[TestFixture]
public class MapConfigurationValidatorTest
{
    private readonly IMapConfigurationValidator _mapConfigurationValidator = new MapConfigurationValidator();
    private static MapConfiguration _correctMapConfig = new MapConfiguration(1000, 0.5, 
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
    
    [Test]
    public void Test_IsDimensionGrowthOk_WithCorrectInput()
    {
        MapConfiguration mapConfig = _correctMapConfig;
        Assert.That(_mapConfigurationValidator.IsDimensionGrowthOk(mapConfig), Is.True);
    }
    [Test]
    public void Test_IsDimensionGrowthOk_WithBadInput()
    {
        MapConfiguration mapConfig = _badDimensionGrowthMapConfig;
        Assert.That(_mapConfigurationValidator.IsDimensionGrowthOk(mapConfig), Is.False);
    }
    
    [Test]
    public void Test_IsPreferredPlacementOk_WithCorrectInput()
    {
        MapConfiguration mapConfig = _correctMapConfig;
        Assert.That(_mapConfigurationValidator.IsPreferredPlacementOk(mapConfig), Is.True);
    }
    [Test]
    public void Test_IsPreferredPlacementOk_WithBadInput()
    {
        MapConfiguration mapConfig = _badPreferredPlaceConfig;
        Assert.That(_mapConfigurationValidator.IsPreferredPlacementOk(mapConfig), Is.False);
    }
    
    [Test]
    public void Test_DoSymbolAndNameMatch_WithCorrectInput()
    {
        MapConfiguration mapConfig = _correctMapConfig;
        Assert.That(_mapConfigurationValidator.DoSymbolAndNameMatch(mapConfig), Is.True);
    }
    [Test]
    public void Test_DoSymbolAndNameMatch_WithBadInput()
    {
        MapConfiguration mapConfig = _badSymbolAndNameConfig;
        Assert.That(_mapConfigurationValidator.DoSymbolAndNameMatch(mapConfig), Is.False);
    }
    
    [Test]
    public void Test_SingleOrMultiDimensionalOk_WithCorrectInput()
    {
        MapConfiguration mapConfig = _correctMapConfig;
        Assert.That(_mapConfigurationValidator.SingleOrMultiDimensionalOk(mapConfig), Is.True);
    }
    [Test]
    public void Test_SingleOrMultiDimensionalOk_WithBadInput()
    {
        MapConfiguration mapConfig = _badSingleOrMultiDimensionConfig;
        Assert.That(_mapConfigurationValidator.SingleOrMultiDimensionalOk(mapConfig), Is.False);
    }
    
    [Test]
    public void Test_TotalNumberOfElementsOk_WithCorrectInput()
    {
        MapConfiguration mapConfig = _correctMapConfig;
        Assert.That(_mapConfigurationValidator.TotalNumberOfElementsOk(mapConfig), Is.True);
    }
    [Test]
    public void Test_TotalNumberOfElementsOk_WithBadInput()
    {
        MapConfiguration mapConfig = _badNumberOfElementsConfig;
        Assert.That(_mapConfigurationValidator.TotalNumberOfElementsOk(mapConfig), Is.False);
    }
    
}