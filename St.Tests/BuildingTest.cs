using System.Linq;
using NUnit.Framework;
using static St.Unit;
namespace St.Tests
{
    [TestFixture]
    public class BuildingTest
    {
        [Test]
        public void Build()
        {
            var game = new Game();
            var tile = new Tile();
            tile.Populate(Population.Worker);
            Assert.That(Building.BasicMine.BuildOn(tile, game), Is.EquivalentTo(new[] {Mineral.Points(-30)}));
            Assert.AreEqual(new Quantity[0] , tile.Output());
            Assert.That(tile.Maintenance(), Is.EquivalentTo(new []{Food.Points(-1)}));
            game.Ticks(Turn.s(120));
            Assert.That(tile.Output(), Is.EquivalentTo(new[] { Mineral.Points(1) }));
            Assert.That(tile.Maintenance(), Is.EquivalentTo(new[] { Energy.Points(-0.5), Food.Points(-1) }));
        }

        [Test]
        public void AvailableBuildings()
        {
            Assert.IsEmpty(Building.AvailableBuildings(new Quantity[0]));
            Assert.AreEqual(1, Building.AvailableBuildings(new []{ Mineral.Points(30) }).Count());
            Assert.AreEqual(4, Building.AvailableBuildings(new []{ Mineral.Points(200) }).Count());
        }
    }
}