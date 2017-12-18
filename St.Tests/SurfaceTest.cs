using NUnit.Framework;

namespace St.Tests
{
    [TestFixture]
    public class SurfaceTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TileCount()
        {
            Assert.AreEqual(new Surface(new SurfaceRow[0]).TileCount, 0);
            Assert.AreEqual(new Surface(new[] { new SurfaceRow(new Tile()) }).TileCount, 1);
            var surfaceRow1 = new SurfaceRow(new Tile());
            Assert.AreEqual(new Surface(new[] { surfaceRow1, new SurfaceRow(surfaceRow1, 0, new Tile()) }).TileCount, 2);
        }

        [Test]
        public void ForCount()
        {
            Assert.AreEqual(new Surface(new SurfaceRow[0]).TileCount, 0);
            Assert.AreEqual(new Surface(new[] { new SurfaceRow(new Tile()) }).TileCount, 1);
            var surfaceRow1 = new SurfaceRow(new Tile());
            Assert.AreEqual(new Surface(new[] { surfaceRow1, new SurfaceRow(surfaceRow1, 0, new Tile()), }).TileCount, 2);
        }
    }
}
