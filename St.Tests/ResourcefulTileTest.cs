using NUnit.Framework;
using static St.ResourceVectorBuilder;
namespace St.Tests
{
    [TestFixture]
    public class ResourcefulTileTest
    {
        [SetUp]
        public void Setup()
        {
            _testee = new Tile(RVB.Energy(10).Mineral(10).Vector);
        }

        private Tile _testee;

        [Test]
        public void Output()
        {
            Assert.AreEqual(ResourceVector.Empty, _testee.Output());
            _testee.Populate(Population.Worker);
            Assert.AreEqual(RVB.Energy(10).Mineral(10).Vector, _testee.Output());
        }
    }
}