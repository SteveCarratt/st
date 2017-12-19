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
            var row1 = new SurfaceRow(new Tile(Mineral.s(1)), new Tile(Energy.Points(2)), new Tile(Mineral.s(1)), new Tile());
            var row2 = new SurfaceRow(row1, 0, new Tile(Mineral.s(1)), new Tile(Energy.Points(1)), new Tile(), new Tile(Mineral.s(1)));
            var row3 = new SurfaceRow(row2, 0, new Tile(), new Tile(), new Tile());
            var planet = new Planet(new Surface(
                row1,
                row2,
                row3));

            var options = planet.Options;

            foreach (var command in options)
            {
                Console.WriteLine(command.ToString());
            }
        }
    }
}
