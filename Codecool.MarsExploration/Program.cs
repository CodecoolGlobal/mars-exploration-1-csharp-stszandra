using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.Configuration.Service;
using Codecool.MarsExploration.MapElements.Service.Builder;
using Codecool.MarsExploration.MapElements.Service.Generator;
using Codecool.MarsExploration.MapElements.Service.Placer;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.Output.Service;

internal class Program
{
    //You can change this to any directory you like
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        Console.WriteLine("Mars Exploration Sprint 1");
        var mapConfig = GetConfiguration();

        IDimensionCalculator dimensionCalculator = new DimensionCalculator();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();

        IMapElementBuilder mapElementFactory = new MapElementBuilder();
        IMapElementsGenerator mapElementsGenerator = null;

        IMapConfigurationValidator mapConfigValidator = null;
        IMapElementPlacer mapElementPlacer = null;

        IMapGenerator mapGenerator = null;

        CreateAndWriteMaps(3, mapGenerator, mapConfig);

        Console.WriteLine("Mars maps successfully generated.");

        // string filePath = WorkDir + "map.txt";
        // IMapFileWriter mapFileWriter = new MapFileWriter();
        // string?[,] representation = new string?[3, 3];
        // bool successfullyGenerated = true;
        // Map map = new Map(representation, successfullyGenerated);
        //   mapFileWriter.WriteMapFile(map, filePath);
        // Console.WriteLine("Map file created succesfully.");

        Console.ReadKey();
    }

    private static void CreateAndWriteMaps(int count, IMapGenerator mapGenerator, MapConfiguration mapConfig)
    {
    }

    private static MapConfiguration GetConfiguration()
    {
        const string mountainSymbol = "#";
        const string pitSymbol = "&";
        const string mineralSymbol = "%";
        const string waterSymbol = "*";

        var mountainsCfg = new MapElementConfiguration(mountainSymbol, "mountain", new[]
        {
            new ElementToSize(2, 20),
            new ElementToSize(1, 30),
        }, 3);

        var pitCfg = new MapElementConfiguration(pitSymbol, "pit", new[]
        {
            new ElementToSize(1, 5),
            new ElementToSize(1, 10),
        }, 10);

        var mineralCfg = new MapElementConfiguration(mineralSymbol, "mineral", new[]
        {
            new ElementToSize(10, 1)
        }, 0, mountainSymbol);

        var waterCfg = new MapElementConfiguration(waterSymbol, "water", new[]
        {
            new ElementToSize(8, 1)
        }, 0, pitSymbol);
        
        List<MapElementConfiguration> elementsCfg = new() { mountainsCfg, pitCfg, mineralCfg, waterCfg };
        return new MapConfiguration(1000, 0.5, elementsCfg);
    }
}
