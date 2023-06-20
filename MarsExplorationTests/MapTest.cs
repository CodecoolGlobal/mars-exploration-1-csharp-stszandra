using Codecool.MarsExploration.MapElements.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsExplorationTests
{
    public class MapTest
    {
        [Test]
        public void CreateStringRepresentation_Should_Return_Correct_String()
        {
            string?[,] representation = new string?[2, 2] { { "A", "B" }, { "C", "D" } };
            Map map = new Map(representation);
            
            string result = map.ToString();
            string expected = "A B \nC D \n";

            Assert.That(expected, Is.EqualTo(result));
        }
        [Test]
        public void CreateStringRepresentation_Should_Handle_Null_Values()
        {
            string?[,] representation = new string?[2, 2] { { null, "B" }, { "C", null } };
            Map map = new Map(representation);

            string result = map.ToString();
            string expected = "null B \nC null \n";

            Assert.That(expected,Is.EqualTo(result));
        }
    }
}
