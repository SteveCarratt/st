using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using static St.Unit;
namespace St.Tests
{
    [TestFixture]
    public class QuantityTest
    {
        [Test]
        public void Inequality()
        {
            Assert.True(Mineral.Points(1) > Mineral.Points(0));
            Assert.True(Mineral.Points(1) < Mineral.Points(10));
            Assert.True(Mineral.Points(1) <= Mineral.Points(1));
            Assert.True(Mineral.Points(1) >= Mineral.Points(1));
            Assert.True(Mineral.Points(0) <= Mineral.Points(1));
            Assert.True(Mineral.Points(200) >= Mineral.Points(10));
        }

        [Test]
        public void Increment()
        {
            var q = Mineral.Points(1);
            Assert.AreEqual(Mineral.Points(2), ++q);
            Assert.AreEqual(Mineral.Points(1), --q);
        }

        [Test]
        public void Addition()
        {
            Assert.AreEqual(Mineral.Points(4), Mineral.Points(3)+ Mineral.Points(1));
        }

        [Test]
        public void Equality()
        {
            Assert.True(Mineral.Points(1) == Mineral.Points(1));
        }

        [Test]
        public void Add()
        {
            Assert.That(Quantity.Add(new[] {Food.Points(1)}, new[] {Mineral.Points(-1)}),
                Is.EquivalentTo(new Quantity[] {Food.Points(1), Mineral.Points(-1)}));
            Assert.That(Quantity.Add(new[] { Food.Points(1) }, new[] { Food.Points(-1) }),
                Is.EquivalentTo(new Quantity[0]));
        }
    }
}
