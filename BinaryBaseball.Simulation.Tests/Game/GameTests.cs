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
    public class GameTests
    {
        [TestMethod()]
        public void CalculateLog5Test()
        {
            // Arrange
            var parameters = new GameParameters();
            var game = new Game(parameters);
            var privateObject = new PrivateObject(game);

            // Act            
            object[] args = new object[3] { 0.2122, 0.1784, 0.1559 };

            Double result = (Double)privateObject.Invoke(
                "CalculateLog5",
                new Type[] { typeof(Double), typeof(Double), typeof(Double) },
                args);

            // Assert
            Assert.AreEqual(0.2405, result, 0.0001);
        }
    }
}