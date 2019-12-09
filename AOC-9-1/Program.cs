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
			var max = 0;

			var range = Enumerable.Range(5, 5).ToList();
			foreach (var i in range)
			{
				var rangej = range.ToList();
				rangej.Remove(i);
				foreach (var j in rangej)
				{
					var rangek = rangej.ToList();
					rangek.Remove(j);
					foreach (var k in rangek)
					{
						var rangel = rangek.ToList();
						rangel.Remove(k);
						foreach (var l in rangel)
						{
							var rangem = rangel.ToList();
							rangem.Remove(l);
							foreach (var m in rangem)
							{
								AmplifierArray amplifierArray = new AmplifierArray(new[] { i, j, k, l, m }.ToList());
								var res = amplifierArray.Amplify(0);
								if (res > max)
								{
									max = res;
								}
							}
						}
					}

				}
			}
			//AmplifierArray amplifierArray = new AmplifierArray(new[] { 9, 7, 8, 5, 6 }.ToList());
			//var res = amplifierArray.Amplify(0);
		}
	}
}
