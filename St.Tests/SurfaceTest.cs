using NUnit.Framework;
using static St.Unit;

namespace St.Tests
{
    [TestFixture]
    public class SurfaceTest
    {
        [SetUp]
        public void Setup()
        {
            _mineralTile = new Tile(Mineral.Points(1));
            _foodTile = new Tile(Food.Points(10));
            _mineralTile.Populate(Population.Worker);
            _foodTile.Populate(Population.Worker);
        }

        private Tile _mineralTile;
        private Tile _foodTile;

        [Test]
        public void Output()
        {
            Assert.AreEqual(new Quantity[0], new Planet().Output);
            Assert.That(new Surface(new SurfaceRow(_mineralTile)).Output, Is.EquivalentTo(new[] {Mineral.Points(1)}));
            Assert.That(new Surface(new SurfaceRow(_mineralTile, _foodTile)).Output,
                Is.EquivalentTo(new[] {Mineral.Points(1), Food.Points(10)}));
            Assert.That(new Surface(new SurfaceRow(_mineralTile), new SurfaceRow(_foodTile)).Output,
                Is.EquivalentTo(new[] {Mineral.Points(1), Food.Points(10)}));
            Assert.That(
                new Surface(new SurfaceRow(_mineralTile, _foodTile, _foodTile, _mineralTile),
                    new SurfaceRow(_mineralTile, _foodTile)).Output,
                Is.EquivalentTo(new[] {Mineral.Points(3), Food.Points(30)}));

            var hqTile = new Tile();
            hqTile.Populate(Population.Worker);
            hqTile.Construct(Building.PlanetaryAdministration);
            var energyTile = new Tile(Energy.Points(10));
            energyTile.Populate(Population.Worker);

            var topRow = new SurfaceRow(new Tile(), _foodTile, _mineralTile);
            var middleRow = new SurfaceRow(topRow, 2, hqTile, energyTile);
            var bottomRow = new SurfaceRow(middleRow, -1, energyTile.Copy(), energyTile.Copy(), energyTile.Copy());
            var complexSurface = new Surface(topRow, middleRow, bottomRow);

            Assert.That(complexSurface.Output,
                Is.EquivalentTo(new[] {Energy.Points(47), Unity.Points(1), Mineral.Points(4), Food.Points(13)}));
        }

        [Test]
        public void TileCount()
        {
            Assert.AreEqual(new Surface().TileCount, 0);
            Assert.AreEqual(new Surface(new SurfaceRow(new Tile())).TileCount, 1);
            var surfaceRow1 = new SurfaceRow(new Tile());
            Assert.AreEqual(new Surface(surfaceRow1, new SurfaceRow(surfaceRow1, 0, new Tile())).TileCount, 2);
        }
    }
}