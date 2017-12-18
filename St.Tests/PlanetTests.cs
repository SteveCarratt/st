﻿using System;
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
            Assert.AreEqual(new Quantity[0], new Planet().Output );
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile))).Output, Is.EquivalentTo(new[] { Mineral.Points(1) }) );
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile, _foodTile))).Output, Is.EquivalentTo(new[] { Mineral.Points(1), Food.Points(10) }) );
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile), new SurfaceRow(_foodTile))).Output, Is.EquivalentTo(new[] { Mineral.Points(1), Food.Points(10) }) );
            Assert.That(new Planet(new Surface(new SurfaceRow(_mineralTile, _foodTile, _foodTile, _mineralTile), new SurfaceRow(_mineralTile, _foodTile))).Output, Is.EquivalentTo(new[] { Mineral.Points(3), Food.Points(30) }) );
        }

        [Test]
        public void UnemployedCount()
        {
            var tile = new Tile(Mineral.Points(1));
            var surface = new Surface(new SurfaceRow(tile, new Tile(Food.Points(1))));
            Assert.AreEqual(0, surface.UnemployedCount);
            tile.Populate(Population.Worker);
            Assert.AreEqual(1, surface.UnemployedCount);
        }

    }
}
