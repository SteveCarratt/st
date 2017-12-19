using NUnit.Framework;
using static St.Building;
using static St.ResourceVectorBuilder;

namespace St.Tests
{
    [TestFixture]
    public class ResourcelessTileTest
    {
        [Test]
        public void Output()
        {
            Assert.AreEqual(ResourceVector.Empty, new Tile().Output());
            var testee = new Tile();
            testee.Populate(Population.Worker);
            Assert.AreEqual(ResourceVector.Empty, new Tile().Output());
            testee.Construct(BasicMine);
            Assert.AreEqual(RVB.Mineral(1).Vector, testee.Output());
        }
    }
}