using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.File.ReadAllLines(@"C:\Users\nielsw\OneDrive - Delta-N\Documents\input.txt")
                .Select(int.Parse)
                .Aggregate(0, (a, b) => a + CalculateFuelReqForModule(b, 0)));
        }

        private static int CalculateFuelReqForModule(int moduleMass, int fuel)
        {
            var initialfuel = (moduleMass / 3) - 2;
            return initialfuel <= 0 ? fuel : CalculateFuelReqForModule(initialfuel, fuel + initialfuel);
        }
    }
}
