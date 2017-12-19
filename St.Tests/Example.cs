using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static St.ResourceVectorBuilder;
namespace St.Tests
{
    [TestFixture]
    public class Example
    {
        [Test]
        public void OptimiseSurface()
        {


            var row1 = new SurfaceRow(new Tile(RVB.Mineral(1).Vector), new Tile(RVB.Energy(2).Vector), new Tile(RVB.Mineral(1).Vector), new Tile());
            var row2 = new SurfaceRow(row1, 0, new Tile(RVB.Mineral(1).Vector), new Tile(RVB.Energy(1).Vector), new Tile(), new Tile(RVB.Mineral(1).Vector));
            var row3 = new SurfaceRow(row2, 0, new Tile(), new Tile(), new Tile());
            var planet = new Planet(new Surface(
                row1,
                row2,
                row3));

            var options = planet.Options;

        }
    }
}
