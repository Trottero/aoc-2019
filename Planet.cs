using System;

public class Planet
{
	public Planet()
	{

	}


    public string Name { get; set; }

    public IEnumerable<Planet> OrbitalPlanets { get; set; }

}
