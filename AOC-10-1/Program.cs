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
                    if (source == astroid)
                    {
                        continue;
                    }

                    // Calculate the adj and opposite side to get the hypo
                    var a = Math.Abs(source.X - astroid.X);
                    var b = Math.Abs(source.Y - astroid.Y);
                    var c = (double)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)); // length of hypo

                    var dy = (source.Y - astroid.Y);
                    var dx = (astroid.X - source.X);


                    var alpha = (double)Math.Atan2(dy, dx);

                    astroid.Distance = c;
                    astroid.Angle = alpha;

                    if (angles.ContainsKey(alpha))
                    {

                        angles[alpha].Add(astroid);
                    }
                    else
                    {
                        angles.Add(alpha, new List<Astroid> { astroid });
                    }
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

            Get200ShootingAstroid();
        }

        public Astroid Get200ShootingAstroid()
        {
            IEnumerable<IOrderedEnumerable<Astroid>> enumerable = OptimalAstroid.AstroidPaths.Select(r => r.Value.OrderBy(x => x.Distance));
            int counter = 1;
            var dict = OptimalAstroid.AstroidPaths.OrderBy(r => r.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            while (OptimalAstroid.AstroidPaths.Any())
            {
                foreach (var lineToDestroy in dict)
                {

                    lineToDestroy.Value.Remove(lineToDestroy.Value.OrderBy(r=>r.Distance).First());
                    counter++;
                    if (!lineToDestroy.Value.Any())
                    {
                        dict.Remove(lineToDestroy.Key);
                    }
                    if (counter == 200)
                    {

                    }
                }
            }
            return null;
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
