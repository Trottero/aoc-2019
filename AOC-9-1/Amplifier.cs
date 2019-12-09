using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC_9_1
{
    public class Amplifier : IntCodeComputer
    {
        private const string AmplifierProgram = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
        public Amplifier NextAmp { get; set; }
        public string AmpId { get; set; }
        public Amplifier(string ampProgram = AmplifierProgram) : base(ampProgram)
        {
        }

        public override void SendOutput(long x)
        {
            ProgramOutput = x;
            NextAmp.AddNewVariable(x);
        }

        public void AddNewVariable(long variable)
        {
            lock (thisLock)
            {
                InputVariables.Add(variable);
            }
        }
    }

    public class AmplifierArray
    {
        private string[] AmpIds = new[] { "A", "B", "C", "D", "E" };
        private readonly IEnumerable<int> _phaseCodes;
        private readonly List<Amplifier> _amplifiers;

        public AmplifierArray(List<int> PhaseCodes, string amplifierProgram = null)
        {
            _phaseCodes = PhaseCodes;
            var Amplifiers = new List<Amplifier>();
            if (string.IsNullOrEmpty(amplifierProgram))
            {
                Amplifiers = PhaseCodes.Select(
                    r => new Amplifier()
                ).ToList();
            }
            else
            { 
                Amplifiers = PhaseCodes.Select(
                   r => new Amplifier(amplifierProgram)
               ).ToList();
            }

            Amplifier prev = new Amplifier();
            foreach (var a in Amplifiers)
            {
                prev.NextAmp = a;
                prev = a;
            }
            Amplifiers[Amplifiers.Count() - 1].NextAmp = Amplifiers.First();

            for (int i = 0; i < Amplifiers.Count(); i++)
            {
                Amplifiers[i].InputVariables.Add(PhaseCodes[i]);
                Amplifiers[i].AmpId = AmpIds[i];
            }
            this._amplifiers = Amplifiers;
        }

        public long Amplify(long input)
        {
            var ts = new List<Thread>();
            _amplifiers[0].AddNewVariable(input);
            _amplifiers.ForEach(a =>
            {
                var t = new Thread(new ThreadStart(a.StartComputer)) { Name = "Computer_Thread" };
                t.Start();
                ts.Add(t);
            });
            while (ts.Any(r => r.IsAlive))
            {

            }
            return _amplifiers.Last().ProgramOutput;
        }
    }
}
