using NUnit.Framework;

namespace St.Tests
{
    [TestFixture]
    public class ResourcelessTileTest
    {
        private Tile _testee;

        [SetUp]
        public void Setup()
        {
            _testee = new Tile();
        }

        [Test]
        public void Tick()
        {
            var res = _testee.Tick();
            Assert.AreEqual(new Resources(energy: 0,minerals: 0), res);
            _testee.Populate(Population.Worker);
            res = _testee.Tick();
            Assert.AreEqual(new Resources(energy: 0, minerals: 0), res);
        }

    }
}