using NUnit.Framework;
using static St.ResourceVectorBuilder;
using static St.Unit;

namespace St.Tests
{
    [TestFixture]
    public class SurfaceTest
    {
        [SetUp]
        public void Setup()
        {
            _mineralTile = new Tile(RVB.Mineral(1).Vector);
            _foodTile = new Tile(RVB.Food(10).Vector);
            _mineralTile.Populate(Population.Worker);
            _foodTile.Populate(Population.Worker);
        }

        private Tile _mineralTile;
        private Tile _foodTile;

        [Test]
        public void Output()
        {
            Assert.That(new Planet().Output, Is.EqualTo(ResourceVector.Empty));
            Assert.That(new Surface(new SurfaceRow(_mineralTile)).Output, Is.EqualTo(RVB.Mineral(1).Vector));
            Assert.That(new Surface(new SurfaceRow(_mineralTile, _foodTile)).Output, Is.EqualTo(RVB.Mineral(1).Food(10).Vector));
            Assert.That(new Surface(new SurfaceRow(_mineralTile), new SurfaceRow(_foodTile)).Output, Is.EqualTo(RVB.Mineral(1).Food(10).Vector));
            Assert.That(new Surface(new SurfaceRow(_mineralTile, _foodTile, _foodTile, _mineralTile), new SurfaceRow(_mineralTile, _foodTile)).Output, Is.EqualTo(RVB.Mineral(3).Food(30).Vector));

            var hqTile = new Tile();
            hqTile.Populate(Population.Worker);
            hqTile.Construct(Building.PlanetaryAdministration);
            var energyTile = new Tile(RVB.Energy(10).Vector);
            energyTile.Populate(Population.Worker);

            var topRow = new SurfaceRow(new Tile(), _foodTile, _mineralTile);
            var middleRow = new SurfaceRow(topRow, 2, hqTile, energyTile);
            var bottomRow = new SurfaceRow(middleRow, -1, energyTile.Copy(), energyTile.Copy(), energyTile.Copy());
            var complexSurface = new Surface(topRow, middleRow, bottomRow);

            Assert.That(complexSurface.Output,
                Is.EqualTo(RVB.Energy(47).Unity(1).Mineral(4).Food(13).Vector));
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