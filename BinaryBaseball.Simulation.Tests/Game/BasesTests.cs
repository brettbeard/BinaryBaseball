using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryBaseball.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBaseball.Simulation.Tests
{
    [TestClass()]
    public class BasesTests
    {
        [TestMethod()]
        public void BasesTest()
        {
            var bases = new Bases();

            // All bases should be null (not occupied by players)
            foreach (var b in bases.Occupied)
            {
                Assert.IsNull(b);
            }
        }

        [TestMethod()]
        public void Bases_EmptyTest()
        {
            var bases = new Bases();

            //  Bases should be empty
            Assert.AreEqual(true, bases.Empty);

            // Put a player on first
            var player = new BinaryBaseball.Common.Player();
            bases.Occupied[0] = player;

            // Should NOT be empty
            Assert.AreEqual(false, bases.Empty);
        }

        [TestMethod()]
        public void ClearTest()
        {
            var bases = new Bases();

            // Put a player on first
            var player = new BinaryBaseball.Common.Player();
            bases.Occupied[0] = player;

            // Clear the bases
            bases.Clear();

            // All bases should be null (not occupied by players)
            foreach (var b in bases.Occupied)
            {
                Assert.IsNull(b);
            }
        }
    }
}