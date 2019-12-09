using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AOC_9_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var pc = new IntCodeComputer("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            pc.StartComputer();

        }
	}
}
