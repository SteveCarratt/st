using NUnit.Framework;
using static St.Unit;
using static St.Building;

namespace St.Tests
{
    [TestFixture]
    public class ResourcelessTileTest
    {
        [Test]
        public void Tick()
        {
            Assert.AreEqual(new Quantity[0], new Tile().Output());
            var testee = new Tile();
            testee.Populate(Population.Worker);
            Assert.AreEqual(new Quantity[0], new Tile().Output());
            testee.Construct(BasicMine);
            Assert.AreEqual(new []{Mineral.Points(1)}, testee.Output());
        }
    }
}