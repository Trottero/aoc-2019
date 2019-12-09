using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC_9_1
{
    public class IntCodeComputer
    {
        private static readonly int[] AmountofArgs = { 0, 3, 3, 1, 1, 2, 2, 3, 3, 1 };
        protected object thisLock = new object();

        private readonly string _input;
        private long[] _computerMemory;

        public ICollection<long> InputVariables { get; set; } = new List<long> { };
        public long Position { get; set; } = 0;

        public long ProgramOutput { get; set; } = -1;

        public long RelativeBase { get; set; } = 0;

        public IntCodeComputer(string program)
        {
            _input = program;
            ClearMemory();
        }

        public void StartComputer()
        {

            // Start the execution of the program on pos 0
            ExecuteInstruction(0);
        }

        public void ClearMemory()
        {
            _computerMemory = _input.Split(',').Select(long.Parse).ToArray();
        }

        public void ExecuteInstruction(long pos)
        {
            Position = pos;
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
                return;
            }

            for (var i = parameterSettings.Count; i < AmountofArgs[instructionCode]; i++)
            {
                parameterSettings.Insert(0, 0);
            }
            parameterSettings = parameterSettings.Reverse().ToArray();

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

                _computerMemory[GetPosition(1, 0)] = InputVariables.First(); // Take the first variable and do something with it.
                InputVariables.Remove(InputVariables.First()); // Remove the consumed variable, this way the next one is ready for the queue.
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
                _computerMemory[GetPosition(3, 0)] = GetValue(1, parameterSettings[0]) + GetValue(2, parameterSettings[1]);
                skippings = 4;
            }

            if (instructionCode == 2) // Multiple
            {
                _computerMemory[GetPosition(3, 0)] = GetValue(1, parameterSettings[0]) * GetValue(2, parameterSettings[1]);
                skippings = 4;
            }

            if (instructionCode == 7) // Less than
            {
                if (GetValue(1, parameterSettings[0]) < GetValue(2, parameterSettings[1]))
                {
                    _computerMemory[GetPosition(3, 0)] = 1;
                }
                else
                {
                    _computerMemory[GetPosition(3, 0)] = 0;
                }

                skippings = 4;
            }

            if (instructionCode == 8) // equals
            {
                if (GetValue(1, parameterSettings[0]) == GetValue(2, parameterSettings[1]))
                {
                    _computerMemory[GetPosition(3, 0)] = 1;
                }
                else
                {
                    _computerMemory[GetPosition(3, 0)] = 0;
                }

                skippings = 4;

            }

            if (instructionCode == 9)
            {
                RelativeBase += GetValue(1, parameterSettings[0]);
                skippings = 2;
            }

            ExecuteInstruction(pos + skippings);
        }

        public virtual void SendOutput(long output)
        {
            ProgramOutput = output;
        }

        public long GetPosition(long parameterOffset, int parameterMode)
        {
            switch (parameterMode)
            {
                // Position mode
                case 0:
                    return _computerMemory[Position + parameterOffset];
                // Direct interpretation mode
                case 1:
                    return Position + parameterOffset;
                case 2:
                    return RelativeBase + _computerMemory[Position + parameterOffset];
            }
            return -1;
        }

        public long GetValue(long parameterOffset, int parameterMode)
        {
            return _computerMemory[GetPosition(parameterOffset, parameterMode)];
        }
    }

}
