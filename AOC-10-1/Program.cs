using System;
using System.Collections.Generic;
using System.Globalization;

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
                            X = x + (float)0.5,
                            Y = y + (float)0.5
                        });
                    }
                }
            }
            // https://revisionmaths.com/gcse-maths-revision/trigonometry/sin-cos-and-tan
            var maxastroids = 0;
            foreach (var source in Astroids)
            {
                var angles = new Dictionary<float, float>();// Dictionary with angle as key and c as value
                foreach (var astroid in Astroids)
                {
                    if (source == astroid)
                    {
                        continue;
                    }

                    // Calculate the adj and opposite side to get the hypo
                    var a = source.X - astroid.X;
                    var b = source.Y - astroid.Y;
                    var c = (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)); // length of hypo

                    var alpha = (float)Math.Atan(b / a);

                    if (angles.ContainsKey(alpha))
                    {
                        if (c < angles[alpha])
                        {
                            angles[alpha] = c;
                        }
                    }
                    else
                    {
                        angles.Add(alpha, c);
                    }
                }

                var totalAstroids = angles.Count;
                if (maxastroids < totalAstroids)
                {
                    maxastroids = totalAstroids;
                    OptimalAstroid = source;
                }
            }
        }

        public ICollection<Astroid> Astroids { get; set; } = new List<Astroid>();

        public Astroid OptimalAstroid { get; set; }
    }

    public class Astroid
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
