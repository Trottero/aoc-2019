using System.Collections.Generic;

namespace AOC_6_1
{
    public class Planet
    {
        public string Name { get; set; }

        public IEnumerable<Planet> OrbitalPlanets { get; set; }
        public Planet Orbits { get; set; }

    }
}
