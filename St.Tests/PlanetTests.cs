using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using static St.Unit;

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
            _mineralTile = new Tile(Mineral.Points(1));
            _foodTile = new Tile(Food.Points(10));
            _mineralTile.Populate(Population.Worker);
            _foodTile.Populate(Population.Worker);  
        }

        [Test]
        public void Output()
        {
            Assert.AreEqual(new Quantity[0], new Planet().Output() );
            Assert.That(new Planet(_mineralTile).Output(), Is.EquivalentTo(new[] { Mineral.Points(1) }) );
            Assert.That(new Planet(_mineralTile, _foodTile).Output(), Is.EquivalentTo(new[] { Mineral.Points(1), Food.Points(10) }) );
        }
    }
}
