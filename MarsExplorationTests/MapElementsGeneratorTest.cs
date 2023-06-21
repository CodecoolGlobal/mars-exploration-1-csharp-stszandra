using Codecool.MarsExploration.Configuration.Model;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;
using Codecool.MarsExploration.MapElements.Service.Generator;
using Moq;

namespace MarsExplorationTests
{
    [TestFixture]
    public class MapElementsGeneratorTest
    {
        private MapElementsGenerator _mapElementsGenerator;
        private Mock<IMapElementBuilder> _mapElementBuilderMock;

        [SetUp]
        public void Setup()
        {
            _mapElementBuilderMock = new Mock<IMapElementBuilder>();
            _mapElementsGenerator = new MapElementsGenerator(_mapElementBuilderMock.Object);
        }
        [Test]
        public void ShouldReturnAllMapElements() 
        {
            MapConfiguration mapConfig = GetConfiguration();
            List<MapElement> expectedMapElements = new List<MapElement>();

            foreach (MapElementConfiguration mapElementConfiguration in mapConfig.MapElementConfigurations)
            {
                foreach (ElementToSize config in mapElementConfiguration.ElementsToDimensions)
                {
                    int count = config.ElementCount;
                    int size = config.Size;
                    for (int i = 1; i < count; i++)
                    {
                        string?[,] representation = new string?[size, size];
                        MapElement mapElement = new MapElement(representation, mapElementConfiguration.Name, size, mapElementConfiguration.PreferredLocationSymbol);
                        _mapElementBuilderMock.Setup(builder => builder.Build(size, mapElementConfiguration.Symbol, mapElementConfiguration.Name, 
                            mapElementConfiguration.DimensionGrowth, mapElementConfiguration.PreferredLocationSymbol)).Returns(mapElement);
                        expectedMapElements.Add(mapElement);
                    }
                }
            }
            IEnumerable<MapElement> result = _mapElementsGenerator.CreateAll(mapConfig);

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedMapElements, result); 
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
}
