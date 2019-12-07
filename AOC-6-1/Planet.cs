using System.Collections.Generic;

namespace AOC_6_1
{
    public class Planet
    {
        public string Name { get; set; }

        public ICollection<Planet> Moons { get; set; }
        public Planet Orbits { get; set; }

    }
}
