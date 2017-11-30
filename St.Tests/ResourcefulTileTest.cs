using NUnit.Framework;
using static St.Unit;
namespace St.Tests
{
    [TestFixture]
    public class ResourcefulTileTest
    {
        [SetUp]
        public void Setup()
        {
            _testee = new Tile(Energy.Points(10), Mineral.Points(10));
        }

        private Tile _testee;

        [Test]
        public void Tick()
        {
            Assert.AreEqual(new Quantity[0], _testee.Output);
            _testee.Populate(Population.Worker);
            Assert.AreEqual(new[]{Energy.Points(10), Mineral.Points(10)}, _testee.Output);
        }
    }
}