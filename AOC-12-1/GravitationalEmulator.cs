using System.Collections.Generic;
using System.Linq;

namespace AOC_12_1
{
    public class GravitationalEmulator
    {
        public Moon[] Moons { get; set; }
        public void LoadMoonsFromFile(string[] str)
        {
            var tList = new List<Moon>();
            foreach (var s in str)
            {
                var trimmed = s.Replace("<", "")
                    .Replace(">", "")
                    .Replace("x=", "")
                    .Replace("y=", "")
                    .Replace("z=", "");
                var splitted = trimmed.Split(',').Select(int.Parse).ToList();
                tList.Add(new Moon
                {
                    X = splitted[0],
                    Y = splitted[1],
                    Z = splitted[2]
                });
            }

            Moons = tList.ToArray();
        }

        public void DoSteps(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                Step();
            }
        }

        /// <summary>
        /// Updates all of the moons positiions
        /// </summary>
        public void Step()
        {
            // For every pair of moons, update velocity by applying gravity
            for (var i = 0; i < Moons.Length; i++)
            {
                for (var j = 0; j < Moons.Length; j++)
                {
                    Moons[i].UpdateGravity(Moons[j]);
                }
            }

            // Move the moons based on their velocity
            for (int i = 0; i < Moons.Length; i++)
            {
                Moons[i].UpdatePosition();
            }
        }

        public int GetMoonEnergy()
        {
            return Moons.Select(r => r.GetKineticEnergy() * r.GetPotentialEnergy()).Sum();
        }
    }
}