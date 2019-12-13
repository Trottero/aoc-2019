using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AOC_12_1
{
    class Program
    {
        private static string str = "<x=-14, y=-4, z=-11>\n<x=-9, y=6, z=-7>\n<x=4, y=1, z=4>\n<x=2, y=-14, z=-9>";
        static void Main(string[] args)
        {
            var split = str.Split("\n");
            var gg = new GravitationalEmulator();
            gg.LoadMoonsFromFile(split);
            gg.DoSteps(1000);
            var res = gg.GetMoonEnergy();
        }
    }
}
