using NUnit.Framework;
using static St.Unit;

namespace St.Tests
{
    [TestFixture]
    public class BuildPopTest
    {
        [Test]
        public void Build()
        {
            var tile = new Tile(Mineral.Points(1));
            var planet = new Planet(new Tile(Food.Points(1)), tile);
            var testee = new BuildPop(planet);
            planet.Visit(testee);
            testee.Build();
            Assert.IsTrue(tile.HasUnemployed);
        }
    }
}
