using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC_11_1.Tests
{
    [TestClass]
    public class HullPaintingRobotTest
    {
        [TestMethod]
        public void TestUpDirection()
        {
            var currentPos = new Position { X = 0, Y = 0 };
            var hp = new HullPaintingRobot("10");

            var res = hp.GetNextRelativeHullPosition(currentPos, Direction.UP);
            Assert.AreEqual(0, res.X);
            Assert.AreEqual(-1, res.Y);
        }

        [TestMethod]
        public void TestDownDirection()
        {
            var currentPos = new Position { X = 0, Y = 0 };
            var hp = new HullPaintingRobot("10");

            var res = hp.GetNextRelativeHullPosition(currentPos, Direction.DOWN);
            Assert.AreEqual(0, res.X);
            Assert.AreEqual(1, res.Y);
        }

        [TestMethod]
        public void TestLeftDirection()
        {
            var currentPos = new Position { X = 0, Y = 0 };
            var hp = new HullPaintingRobot("10");

            var res = hp.GetNextRelativeHullPosition(currentPos, Direction.LEFT);
            Assert.AreEqual(-1, res.X);
            Assert.AreEqual(0, res.Y);
        }

        [TestMethod]
        public void TestRightDirection()
        {
            var currentPos = new Position { X = 0, Y = 0 };
            var hp = new HullPaintingRobot("10");

            var res = hp.GetNextRelativeHullPosition(currentPos, Direction.RIGHT);
            Assert.AreEqual(1, res.X);
            Assert.AreEqual(0, res.Y);
        }

        [TestMethod]
        public void Test0ReturningBlackColour()
        {
            var hp = new HullPaintingRobot("10");

            var res = hp.GetColorForNumber(0);
            Assert.AreEqual(Color.Black, res);
        }

        [TestMethod]
        public void Test1ReturningWhiteColour()
        {
            var hp = new HullPaintingRobot("10");

            var res = hp.GetColorForNumber(1);
            Assert.AreEqual(Color.White, res);
        }

        [TestMethod]
        public void Test2ReturningException()
        {
            var hp = new HullPaintingRobot("10");

            Assert.ThrowsException<ArgumentException>(() => hp.GetColorForNumber(2)); 
        }

        [TestMethod]
        [DataRow(0,Direction.RIGHT)]
        [DataRow(90,Direction.DOWN)]
        [DataRow(180, Direction.LEFT)]
        [DataRow(270,Direction.UP)]
        public void TurningRightTests(int currentAngle, Direction expectedAngle)
        {
            var hp = new HullPaintingRobot("10");
            hp.DirectionNum = currentAngle;
            var newd = hp.GetDirectionFromNumber(1);
            Assert.AreEqual(expectedAngle, newd);
        }

        [TestMethod]
        [DataRow(0, Direction.LEFT)]
        [DataRow(270, Direction.DOWN)]
        [DataRow(180, Direction.RIGHT)]
        [DataRow(90, Direction.UP)]
        public void TurningLeftTests(int currentAngle, Direction expectedAngle)
        {
            var hp = new HullPaintingRobot("10");
            hp.DirectionNum = currentAngle;
            var newd = hp.GetDirectionFromNumber(0);
            Assert.AreEqual(expectedAngle, newd);
        }
    }
}
