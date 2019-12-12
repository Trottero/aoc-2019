using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC_11_1
{
	public class IntCodeComputer
	{
		private static readonly int[] AmountofArgs = { 0, 3, 3, 1, 1, 2, 2, 3, 3, 1 };
		protected object thisLock = new object();

		private readonly string _input;
		private long[] _computerMemory;

		public ICollection<long> InputVariables { get; set; } = new List<long> { };
		public long Position { get; set; } = 0;

		public ICollection<long> ProgramOutput { get; set; } = new List<long>();

		public long RelativeBase { get; set; } = 0;

		public ComputerState State { get; set; } = ComputerState.RUNNING;

		public IntCodeComputer(string program)
		{
			_input = program;
			ClearMemory();
		}

		public void StartComputer()
		{

			// Start the execution of the program on pos 0
			while (State != ComputerState.STOPPED)
			{
				ExecuteInstruction();
			}
		}

		public void ClearMemory()
		{
			_computerMemory = _input.Split(',').Select(long.Parse).ToArray();
		}

		public void ExecuteInstruction()
		{
			var pos = Position;
			long skippings = 0;
			// Expected to be at least 2 numbers
			var instruction = _computerMemory[pos].ToString();
			var instructionCode = int.Parse(instruction);
			IList<int> parameterSettings = new List<int>();


			if (instruction.Length > 1)
			{
				instructionCode = int.Parse(instruction.Substring(instruction.Length - 2, 2));
				parameterSettings = instruction.Substring(0, instruction.Length - 2)
					.ToCharArray()
					.Select(r => int.Parse(r.ToString()))
					.ToList();
			}

			if (instructionCode == 99)
			{
				State = ComputerState.STOPPED;
				return;
			}

			for (var i = parameterSettings.Count; i < AmountofArgs[instructionCode]; i++)
			{
				parameterSettings.Insert(0, 0);
			}
			parameterSettings = parameterSettings.Reverse().ToArray();

			if (instructionCode == 3)
			{
				Memory(GetPosition(1, parameterSettings[0])) = GetInput();
				skippings = 2;
			}

			if (instructionCode == 4)
			{
				SendOutput(GetValue(1, parameterSettings[0]));
				skippings = 2;
			}

			if (instructionCode == 5 || instructionCode == 6)
			{
				skippings = 3;
				if (instructionCode == 5 && GetValue(1, parameterSettings[0]) != 0) // Jump 1
				{
					skippings = 0;
					pos = GetValue(2, parameterSettings[1]);
				}
				if (instructionCode == 6 && GetValue(1, parameterSettings[0]) == 0) // Jump 2
				{
					skippings = 0;
					pos = GetValue(2, parameterSettings[1]);
				}
			}

			if (instructionCode == 1) // Adding
			{
				Memory(GetPosition(3, parameterSettings[2])) = GetValue(1, parameterSettings[0]) + GetValue(2, parameterSettings[1]);
				skippings = 4;
			}

			if (instructionCode == 2) // Multiple
			{
				Memory(GetPosition(3, parameterSettings[2])) = GetValue(1, parameterSettings[0]) * GetValue(2, parameterSettings[1]);
				skippings = 4;
			}

			if (instructionCode == 7) // Less than
			{
				if (GetValue(1, parameterSettings[0]) < GetValue(2, parameterSettings[1]))
				{
					Memory(GetPosition(3, parameterSettings[2])) = 1;
				}
				else
				{
					Memory(GetPosition(3, parameterSettings[2])) = 0;
				}

				skippings = 4;
			}

			if (instructionCode == 8) // equals
			{
				if (GetValue(1, parameterSettings[0]) == GetValue(2, parameterSettings[1]))
				{
					Memory(GetPosition(3, parameterSettings[2])) = 1;
				}
				else
				{
					Memory(GetPosition(3, parameterSettings[2])) = 0;
				}

				skippings = 4;

			}

			if (instructionCode == 9)
			{
				RelativeBase += GetValue(1, parameterSettings[0]);
				skippings = 2;
			}

			var b = RelativeBase;

			Position = pos + skippings;
		}

		public virtual void SendOutput(long output)
		{
			ProgramOutput.Add(output);
		}

		public virtual long GetInput()
		{
			var shouldLoop = true;
			while (shouldLoop) // Checks for new instructions
			{
				lock (thisLock)
				{
					shouldLoop = !InputVariables.Any();
				}
			}
			var longtoret = InputVariables.First();
			InputVariables.Remove(InputVariables.First()); // Remove the consumed variable, this way the next one is ready for the queue.
			return longtoret; // Take the first variable and do something with it.
		}
		public long GetPosition(long parameterOffset, int parameterMode)
		{
			switch (parameterMode)
			{
				// Position mode
				case 0:
					return Memory(Position + parameterOffset);
				// Direct interpretation mode
				case 1:
					return Position + parameterOffset;
				case 2:
					return RelativeBase + Memory(Position + parameterOffset);
			}
			return -1;
		}

		public long GetValue(long parameterOffset, int parameterMode)
		{
			return Memory(GetPosition(parameterOffset, parameterMode));
		}

		private ref long Memory(long index)
		{
			if (index > _computerMemory.Length - 1)
			{
				Array.Resize(ref _computerMemory, (int)index + 1);
			}

			return ref _computerMemory[index];
		}

		public enum ComputerState
		{
			RUNNING,
			STOPPED
		}
	}
}
