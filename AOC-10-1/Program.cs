using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AOC_10_1
{
	class Program
	{
		static void Main(string[] args)
		{

		}
	}

	public class AstroidMap
	{
		public void FromFile(string[] map)
		{
			for (var y = 0; y < map.Length; y++)
			{
				var line = map[y];
				for (var x = 0; x < line.Length; x++)
				{
					if (line[x] == '#')
					{
						Astroids.Add(new Astroid
						{
							X = x + (double)0.5,
							Y = y + (double)0.5
						});
					}
				}
			}
			// https://revisionmaths.com/gcse-maths-revision/trigonometry/sin-cos-and-tan
			var maxastroids = 0;
			foreach (var source in Astroids)
			{
				var angles = new Dictionary<double, List<Astroid>>();// Dictionary with angle as key and c as value
				foreach (var astroid in Astroids)
				{
					if (source.X == astroid.X && source.Y == astroid.Y)
					{
						continue;
					}
					var newAstro = new Astroid
					{
						X = astroid.X,
						Y = astroid.Y
					};
					// Calculate the adj and opposite side to get the hypo
					var a = Math.Abs(source.X - newAstro.X);
					var b = Math.Abs(source.Y - newAstro.Y);
					var c = (double)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)); // length of hypo

					var dy = (source.Y - newAstro.Y);
					var dx = (newAstro.X - source.X);


					var alpha = (double)Math.Atan2(dy, dx) * (180 / Math.PI) - 90;
					if (alpha < 0)
					{
						alpha = 360 + alpha;
					}
					alpha = 360 - alpha;
					newAstro.Distance = c;
					newAstro.Angle = alpha;

					if (!angles.ContainsKey(alpha))
					{
						angles.Add(alpha, new List<Astroid>());
					}
					angles[alpha].Add(newAstro);
				}

				var totalAstroids = angles.Count;
				if (maxastroids < totalAstroids)
				{
					maxastroids = totalAstroids;
					OptimalAstroid = source;
					OptimalAstroid.AstroidsWithinLineOfSight = totalAstroids;
					OptimalAstroid.AstroidPaths = angles;
				}
			}
		}

		public List<Astroid> GetAstroidDeletionOrder()
		{
			var l = new List<Astroid>();
			var dict = OptimalAstroid.AstroidPaths.OrderBy(r => r.Key).Select(r => new tt { a = r.Key, b = r.Value.ToList() }).ToList();
			foreach (tt v in dict)
			{
				var astToDestroy = v.b.OrderBy(r => r.Distance).First();
				l.Add(astToDestroy);
				v.b = v.b.Where(a => a.X == astToDestroy.X && a.Y == astToDestroy.Y).ToList();
			}
			return l;


		}
		private class tt
		{
			public double a { get; set; }
			public List<Astroid> b { get; set; }
		}


		public ICollection<Astroid> Astroids { get; set; } = new List<Astroid>();

		public Astroid OptimalAstroid { get; set; }
	}

	public class Astroid
	{
		public double X { get; set; }
		public double Y { get; set; }
		public int AstroidsWithinLineOfSight { get; set; }
		public double Distance { get; set; }
		public double Angle { get; set; }

		public Dictionary<double, List<Astroid>> AstroidPaths { get; set; } = new Dictionary<double, List<Astroid>>();
	}
}
