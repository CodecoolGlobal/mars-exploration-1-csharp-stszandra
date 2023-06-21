using Codecool.MarsExploration.Calculators.Model;
using Codecool.MarsExploration.Calculators.Service;
using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.MapElements.Service.Builder;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsExplorationTests
{
    [TestFixture]
    public class MapElementBuilderTests
    {
        private IDimensionCalculator _dimensionCalculator;
        private ICoordinateCalculator _coordinateCalculator;
        private MapElementBuilder _mapElementBuilder;

        [SetUp]
        public void Setup()
        {
            _dimensionCalculator = new DimensionCalculator();
            _coordinateCalculator = new CoordinateCalculator();
            _mapElementBuilder = new MapElementBuilder(_dimensionCalculator, _coordinateCalculator);
        }
       

        [Test]
        public void Build_Map_Element() 
        {
            int size = 10;
            string symbol = "#";
            string name = "mountain";
            int dimensionGrowth = 3;
            string? preferredLocationSymbol = null;

            MapElement result = _mapElementBuilder.Build(size, symbol, name, dimensionGrowth, preferredLocationSymbol);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(preferredLocationSymbol, Is.EqualTo(result.PreferredLocationSymbol));
            Assert.That(7, Is.EqualTo(result.Dimension));
            Assert.IsNotNull(result.Representation);
            Assert.That(7, Is.EqualTo(result.Representation.GetLength(0)));
            Assert.That(7, Is.EqualTo(result.Representation.GetLength(1)));
        }

    }
}
