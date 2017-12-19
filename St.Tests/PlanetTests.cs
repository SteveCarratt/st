using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using static St.ResourceVectorBuilder;

namespace St.Tests
{
    [TestFixture]
    public class PlanetTests
    {
        private Tile _mineralTile;
        private Tile _foodTile;

        [SetUp]
        public void Setup()
        {
            _mineralTile = new Tile(RVB.Mineral(1).Vector);
            _foodTile = new Tile(RVB.Food(10).Vector);
            _mineralTile.Populate(Population.Worker);
            _foodTile.Populate(Population.Worker);  
        }

        [Test]
        public void Output()
        {
            Assert.That(new Planet().Output, Is.EqualTo(ResourceVector.Empty));
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile))).Output, Is.EqualTo(RVB.Mineral(1).Vector));
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile, _foodTile))).Output, Is.EqualTo(RVB.Mineral(1).Food(10).Vector));
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile), new SurfaceRow(_foodTile))).Output, Is.EqualTo(RVB.Mineral(1).Food(10).Vector));
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile, _foodTile, _foodTile, _mineralTile), new SurfaceRow(_mineralTile, _foodTile))).Output, Is.EqualTo(RVB.Mineral(3).Food(30).Vector));
        }

        [Test]
        public void UnemployedCount()
        {
            var tile = new Tile(RVB.Mineral(1).Vector);
            var surface = new Surface(new SurfaceRow(tile, new Tile(RVB.Food(1).Vector)));
            Assert.AreEqual(0, surface.UnemployedCount);
            tile.Populate(Population.Worker);
            Assert.AreEqual(1, surface.UnemployedCount);
        }

    }
}
