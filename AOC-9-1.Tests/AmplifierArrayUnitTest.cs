using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC_9_1.Tests
{
    [TestClass]
    public class AmplifierArrayUnitTest
    {
        // Day 7 tests
        [TestMethod]
        public void TestFeedBackLoop1()
        {
            var phaseSettings = new int[] { 9, 8, 7, 6, 5 }.ToList();
            var program = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            var expected = 139629729;

            var array = new AmplifierArray(phaseSettings, program);
            var result = array.Amplify(0);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFeedBackLoop2()
        {
            var phaseSettings = new int[] { 9, 7, 8, 5, 6 }.ToList();
            var program = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            var expected = 18216;

            var array = new AmplifierArray(phaseSettings, program);
            var result = array.Amplify(0);
            Assert.AreEqual(expected, result);
        }
    }
}
