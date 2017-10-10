using System;
using System.Linq;
using NUnit.Framework;
using static St.Unit;

namespace St.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void RegisterTimer()
        {
            var g = new Game();
            var callbackInvoked = false;
            g.RegisterTimer(Turn.s(1), () => callbackInvoked = true);

            g.Tick();

            Assert.True(callbackInvoked);
        }

        [Test]
        public void Games()
        {
            Assert.AreEqual(0, new Game().Games(Turn.s(1)).Count());
            Assert.AreEqual(5, new Game(new []{new Planet(new Tile())}).Games(Turn.s(1)).Count());
            Assert.AreEqual(25, new Game(new []{new Planet(new Tile())}).Games(Turn.s(2)).Count());
            foreach (var game in new Game(new[] { new Planet(new Tile()) }).Games(Turn.s(3)))
            {
                Console.Write(game);
                Console.WriteLine();
            }
        }
    }
}