using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC_9_1
{
	public class IntCodeComputer
	{
		private static readonly int[] amountofArgs = { 0, 3, 3, 1, 1, 2, 2, 3, 3 };
		protected object thisLock = new object();

		private string input = "";
		private int[] computerMemory;
		public ICollection<int> InputVariables { get; set; } = new List<int> { };
		private int InputIndex = 0;
		public int position { get; set; } = 0;

		public int ProgramOutput { get; set; } = -1;

		public IntCodeComputer(string program)
		{
			input = program;
			ClearMemory();
		}

		public void StartComputer()
		{

			// Start the execution of the program on pos 0
			ExecuteInstruction(0);
		}

		public void ClearMemory()
		{
			computerMemory = input.Split(',').Select(int.Parse).ToArray();
		}

		public void ExecuteInstruction(int pos)
		{
			position = pos;
			int skippings = 0;
			// Expected to be atleast 2 numbers
			var instruction = computerMemory[pos].ToString();
			var instructionCode = int.Parse(instruction);
			IList<char> parameterSettings = new List<char>();
			if (instruction.Length > 1)
			{
				instructionCode = int.Parse(instruction.Substring(instruction.Length - 2, 2));
				parameterSettings = instruction.Substring(0, instruction.Length - 2).ToCharArray().ToList();
			}

			if (instructionCode == 99)
			{
				return;
			}

			for (int i = parameterSettings.Count(); i < amountofArgs[instructionCode]; i++)
			{
				parameterSettings.Insert(0, '0');
			}

			parameterSettings = parameterSettings.Reverse().ToArray();

			if (amountofArgs[instructionCode] == 1) // in and output
			{
				if (instructionCode == 3)
				{
					var shouldLoop = true;
					while (shouldLoop) // Checks for new instructions
					{
						lock (thisLock)
						{
							shouldLoop = !InputVariables.Any();
						}
					}
					computerMemory[computerMemory[pos + 1]] = InputVariables.First(); // Take the first variable and do something with it.
					InputVariables.Remove(InputVariables.First()); // Remove the consumed variable, this way the next one is ready for the queue.
				}

				if (instructionCode == 4)
				{
					if (parameterSettings[0] == '1')
					{
						SendOutput(computerMemory[pos + 1]);
					}
					else
					{
						SendOutput(computerMemory[computerMemory[pos + 1]]);
					}
				}
				skippings = 2;
			}

			if (amountofArgs[instructionCode] == 2 || amountofArgs[instructionCode] == 3) // Jump functions
			{
				int parm1 = 0;
				if (parameterSettings[0] == '1')
				{
					parm1 = computerMemory[pos + 1];
				}
				else
				{
					parm1 = computerMemory[computerMemory[pos + 1]];
				}

				int parm2 = 0;
				if (parameterSettings[1] == '1')
				{
					parm2 = computerMemory[pos + 2];
				}
				else
				{
					parm2 = computerMemory[computerMemory[pos + 2]];
				}

				if (instructionCode == 5 || instructionCode == 6)
				{
					skippings = 3;
					if (instructionCode == 5 && parm1 != 0) // Jump 1
					{
						skippings = 0;
						pos = parm2;
					}
					if (instructionCode == 6 && parm1 == 0) // Jump 2
					{
						skippings = 0;
						pos = parm2;
					}
				}

				if (instructionCode == 1) // Adding
				{
					computerMemory[computerMemory[pos + 3]] = parm2 + parm1;
					skippings = 4;
				}

				if (instructionCode == 2) // Multiple
				{
					computerMemory[computerMemory[pos + 3]] = parm2 * parm1;
					skippings = 4;
				}

				if (instructionCode == 7) // Less than
				{
					if (parm1 < parm2)
					{
						computerMemory[computerMemory[pos + 3]] = 1;
					}
					else
					{
						computerMemory[computerMemory[pos + 3]] = 0;
					}

					skippings = 4;
				}

				if (instructionCode == 8)
				{
					if (parm1 == parm2)
					{
						computerMemory[computerMemory[pos + 3]] = 1;
					}
					else
					{
						computerMemory[computerMemory[pos + 3]] = 0;
					}

					skippings = 4;
				}
			}

			ExecuteInstruction(pos + skippings);
		}
		public virtual void SendOutput(int output)
		{
			ProgramOutput = output;
		}
	}

}
