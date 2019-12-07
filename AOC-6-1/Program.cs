using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_6_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = System.IO.File.ReadAllLines("D6Puzzle.txt");
			var enumerable = lines.AsEnumerable().Select(r => r.Split(')'));

			ICollection<Planet> planets = new List<Planet>();
			enumerable.Select(r =>
			{
				var parentPlanet = planets.FirstOrDefault(x => x.Name == r[0]);
				var childPlanet = planets.FirstOrDefault(x => x.Name == r[1]);
				if (parentPlanet == null)
				{
					parentPlanet = new Planet
					{
						Name = r[0],
						OrbitalPlanets = new List<Planet>()
					};
					planets.Add(parentPlanet);
				};
				if (childPlanet == null)
				{
					childPlanet = new Planet
					{
						Name = r[1],
						OrbitalPlanets = new List<Planet>(),
						Orbits = parentPlanet
					};
					planets.Add(childPlanet);
				}
				if (childPlanet.Orbits == null)
				{
					childPlanet.Orbits = parentPlanet;
				}
				parentPlanet.OrbitalPlanets.Add(childPlanet);

				return new Planet();
			}).ToList();

			var x = CalculateOrbitNumber(planets.First(r => r.Orbits == null), 0);
			var meplanet = planets.First(r => r.Name == "YOU").Orbits;
			var santaplanet = planets.First(r => r.Name == "SAN").Orbits;
			var xx = getPathToSantaRecurisve(meplanet, santaplanet, 0);
		}

		static int CalculateOrbitNumber(Planet planet, int currentNumber)
		{
			if (!planet.OrbitalPlanets.Any())
			{
				return currentNumber;
			}
			return planet.OrbitalPlanets.Select(r => CalculateOrbitNumber(r, currentNumber + 1)).Sum() + currentNumber;
		}


		// Recursively find santa
		static int getPathToSantaRecurisve(Planet currentPlanet, Planet santaPlanet, int currentNumber, bool shouldDelete = true)
		{
			if (currentPlanet.Name == santaPlanet.Name)
			{
				return currentNumber;
			}

			// I guess it isn't it chief
			foreach (var childPlanet in currentPlanet.OrbitalPlanets)
			{
				var x = getPathToSantaRecurisve(childPlanet, santaPlanet, currentNumber + 1, false); // Dont delete them, that makes em angry
				if (x != -1)
				{
					return x; // we found it chief
				}
			}

			if (shouldDelete) // Delete the current node if we didnt find it.
			{
				currentPlanet.Orbits.OrbitalPlanets.Remove(currentPlanet);
				return getPathToSantaRecurisve(currentPlanet.Orbits, santaPlanet, currentNumber + 1);
			}

			return -1;
		}
	}
}
