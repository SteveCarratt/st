using NUnit.Framework;

namespace St.Tests
{
    [TestFixture]
    public class ResourcefulTileTest
    {
        [SetUp]
        public void Setup()
        {
            _testee = new Tile(new Resources(energy: 10, minerals: 10));
        }

        private Tile _testee;

        [Test]
        public void Tick()
        {
            var res = _testee.Tick();
            Assert.AreEqual(new Resources(energy: 0, minerals: 0), res);
            _testee.Populate(Population.Worker);
            res = _testee.Tick();
            Assert.AreEqual(new Resources(energy: 10, minerals: 10), res);
        }
    }
}