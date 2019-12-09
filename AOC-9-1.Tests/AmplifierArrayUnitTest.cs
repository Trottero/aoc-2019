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

    [TestClass]
    public class IntCodeComputerTests
    {
        [TestMethod]
        public void TestJumpInputZero_Returns_Zero_Position_Mode()
        {
            var program = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(0);
            intCodeComputer.StartComputer();
            Assert.AreEqual(0, intCodeComputer.ProgramOutput);
        }

        [TestMethod]
        public void TestJumpInputNonZero_Returns_NonZero_Position_Mode()
        {
            var program = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(1);
            intCodeComputer.StartComputer();
            Assert.AreEqual(1, intCodeComputer.ProgramOutput);
        }

        [TestMethod]
        public void TestJumpInputNonZero_Returns_NonZero_ImmediateMode()
        {
            var program = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(1);
            intCodeComputer.StartComputer();
            Assert.AreEqual(1, intCodeComputer.ProgramOutput);
        }

        [TestMethod]
        public void TestJumpInputZeroReturnsZero_Immediate_Mode()
        {
            var program = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(0);
            intCodeComputer.StartComputer();
            Assert.AreEqual(0, intCodeComputer.ProgramOutput);
        }


        [TestMethod]

        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", 8, 1, "Position mode equal to 8")]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", 7, 0, "Position mode equal to 8")]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", 9, 0, "Position mode equal to 8")]

        [DataRow("3,3,1108,-1,8,3,4,3,99", 8, 1, "Position mode equal to 8")]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 7, 0, "Position mode equal to 8")]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 9, 0, "Position mode equal to 8")]
        public void Test_Compares_Equal(string program, int input, int expectedOutput, string testName)
        {
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(input);
            intCodeComputer.StartComputer();
            Assert.AreEqual(expectedOutput, intCodeComputer.ProgramOutput);
        }
    }
}
