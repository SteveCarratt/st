using System;
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
            var bestTile = new Tile(Mineral.Points(1));
            var worstTile = new Tile(Food.Points(1));
            var planet = new Planet(worstTile, bestTile);
            var testee = new BuildPop(planet);
            planet.Visit(testee);
            Console.WriteLine(testee.BestAction());
        }
    }
}
