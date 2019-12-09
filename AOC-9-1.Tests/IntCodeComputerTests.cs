using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOC_9_1.Tests
{
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
        [DataRow("3,3,1108,-1,8,3,4,3,99", 8, 1, "Immediate mode equal to 8")]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 7, 0, "Immediate mode equal to 8")]
        [DataRow("3,3,1108,-1,8,3,4,3,99", 9, 0, "Immediate mode equal to 8")]
        public void Test_Compares_Equal(string program, int input, int expectedOutput, string testName)
        {
            var intCodeComputer = new IntCodeComputer(program);
            intCodeComputer.InputVariables.Add(input);
            intCodeComputer.StartComputer();
            Assert.AreEqual(expectedOutput, intCodeComputer.ProgramOutput);
        }

        [TestMethod]
        [DataRow("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", 1, "Equal output")]
        [DataRow("1102,34915192,34915192,7,4,7,99,0",  1, "Outputs a 16 bit number")]
        [DataRow("104,1125899906842624,99", 1125899906842624, "Output large number")]
        public void Test_Relative_Base(string program, long expectedOutput, string testName)
        {
            var intCodeComputer = new IntCodeComputer(program); 
            intCodeComputer.StartComputer();
            Assert.AreEqual(expectedOutput, intCodeComputer.ProgramOutput);
        }
    }
}