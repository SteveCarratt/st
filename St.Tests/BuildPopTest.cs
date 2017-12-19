using System;
using NUnit.Framework;
using static St.ResourceVectorBuilder;
using static St.Unit;

namespace St.Tests
{
    [TestFixture]
    public class BuildPopTest
    {
        [Test]
        public void Build()
        {
            var bestTile = new Tile(RVB.Mineral(1).Vector);
            var worstTile = new Tile(RVB.Food(1).Vector);
            var planet = new Planet(new Surface(new[] {new SurfaceRow(worstTile, bestTile)}));
            var testee = new BuildPop(planet);
            planet.Visit(testee);
            Console.WriteLine(testee.BestAction());
        }
    }
}
