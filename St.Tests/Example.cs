using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static St.Unit;
namespace St.Tests
{
    [TestFixture]
    public class Example
    {
        [Test]
        public void OptimiseSurface()
        {
            var planet = new Planet(new Surface(
                new SurfaceRow(new Tile(Mineral.s(1)), new Tile(Energy.Points(2)).Blocked(), new Tile(Mineral.s(1)), new Tile()),
                new SurfaceRow(new Tile(Mineral.s(1)), new Tile(Energy.Points(2)), new Tile(Mineral.s(1)), new Tile())
                ))));
        }
    }
}
