using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AOC_7_1
{
	public class Amplifier : IntCodeComputer
	{
		static string AmplifierProgram = "3,8,1001,8,10,8,105,1,0,0,21,34,59,68,85,102,183,264,345,426,99999,3,9,101,3,9,9,102,3,9,9,4,9,99,3,9,1002,9,4,9,1001,9,2,9,1002,9,2,9,101,5,9,9,102,5,9,9,4,9,99,3,9,1001,9,4,9,4,9,99,3,9,101,3,9,9,1002,9,2,9,1001,9,5,9,4,9,99,3,9,1002,9,3,9,1001,9,5,9,102,3,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,1,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,99";		public Amplifier NextAmp { get; set; }
		public string AmpId { get; set; }
		public Amplifier() : base(AmplifierProgram)
		{

		}

		public override void SendOutput(int x)
		{
			ProgramOutput = x;
			NextAmp.AddNewVariable(x);
		}

		public void AddNewVariable(int variable)
		{
			lock (thisLock)
			{
				this.InputVariables.Add(variable);
			}
		}
	}

	public class AmplifierArray
	{
		private string[] AmpIds = new[] { "A", "B", "C", "D", "E" };
		private readonly IEnumerable<int> phaseCodes;
		private List<Amplifier> Amplifiers = new List<Amplifier>();

		public AmplifierArray(List<int> PhaseCodes)
		{
			phaseCodes = PhaseCodes;
			var Amplifiers = PhaseCodes.Select(
				r => new Amplifier()
			).ToList();

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
			this.Amplifiers = Amplifiers;
		}

		public int Amplify(int input)
		{
			var ts = new List<Thread>();
			Amplifiers[0].AddNewVariable(input);
			Amplifiers.ForEach(a =>
			{
				var t = new Thread(new ThreadStart(a.StartComputer));
				t.Name = "Computer_Thread";
				t.Start();
				ts.Add(t);
			});
			while (ts.Any(r => r.IsAlive))
			{

			}
			return Amplifiers.Last().ProgramOutput;
		}
	}
}
