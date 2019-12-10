using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC_10_1.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSample1()
        {
            var map = "......#.#.\r\n#..#.#....\r\n..#######.\r\n.#.#.###..\r\n.#..#.....\r\n..#....#.#\r\n#..#....#.\r\n.##.#..###\r\n##...#..#.\r\n.#....####";
            map.Replace("\r", "");
            var split = map.Split("\n");
            var astMap = new AstroidMap();
            astMap.FromFile(split);
            Assert.AreEqual(5.5, astMap.OptimalAstroid.X);
            Assert.AreEqual(8.5, astMap.OptimalAstroid.Y);
        }
    }
}