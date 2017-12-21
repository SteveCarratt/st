using System;
using NUnit.Framework;
using static St.Building;
using static St.Population;
using static St.ResourceVectorBuilder;
namespace St.Tests
{
    [TestFixture]
    public class Example
    {
        [Test]
        public void OptimiseSurface()
        {
            var row1 = new SurfaceRow(2, new Tile(), new Tile(RVB.Mineral(2).Vector));
            var row2 = row1.AppendRow(-2, new Tile(RVB.Energy(2).Vector), new Tile(RVB.Engineering(1).Vector), new Tile(RVB.Vector), new Tile(RVB.Physics(1).Vector));
            var row3 = row2.AppendRow(new Tile(RVB.Physics(1).Vector), new Tile(RVB.Vector), new Tile(RVB.Mineral(2).Vector), new Tile(RVB.Food(2).Vector).Block());
            var row4 = row3.AppendRow(new Tile(RVB.Mineral(1).Vector), new Tile(RVB.Vector), new Tile(RVB.Energy(1).Vector).Construct(PlanetaryAdministration).Populate(Robot), new Tile(RVB.Vector));
            var row5 = row4.AppendRow(new Tile(RVB.Society(1).Vector).Block(), new Tile(RVB.Engineering(1).Vector), new Tile(RVB.Energy(2).Vector), new Tile(RVB.Energy(2).Vector));
            var planet = new Planet(new Surface(
                row1,
                row2,
                row3,
                row4,
                row5));


            var summary = new BestPlanet(planet, MiningNetworkI, PowerPlantI, BasicScienceLab);

            Console.WriteLine(summary.Summary());
        }
    }
}
