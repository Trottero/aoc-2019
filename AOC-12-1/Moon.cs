using System;
using System.Linq;

namespace AOC_12_1
{
    public class Moon
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int VX { get; set; } = 0;
        public int VY { get; set; } = 0;
        public int VZ { get; set; } = 0;

        public int GetPotentialEnergy()
        {
            var h = new[] { X, Y, Z };
            var sum = h.Select(Math.Abs).Sum();
            return sum;
        }

        public int GetKineticEnergy()
        {
            var h = new[] { VX, VY, VZ };
            var sum = h.Select(Math.Abs).Sum();
            return sum;
        }

        public void UpdatePosition()
        {
            X += VX;
            Y += VY;
            Z += VZ;
        }

        public void UpdateGravity(Moon othermoon)
        {
            // For every axis, apply the gravity rule to pull them more together
            if (X != othermoon.X)
            {

                if (X > othermoon.X)
                {
                    VX--;
                    othermoon.VX++;
                }
                else
                {
                    VX++;
                    othermoon.VX--;
                }
            }

            if (Y != othermoon.Y)
            {

                if (Y > othermoon.Y)
                {
                    VY--;
                    othermoon.VY++;
                }
                else
                {
                    VY++;
                    othermoon.VY--;
                }
            }

            if (Z != othermoon.Z)
            {

                if (Z > othermoon.Z)
                {
                    VZ--;
                    othermoon.VZ++;
                }
                else
                {
                    VZ++;
                    othermoon.VZ--;
                }
            }
        }
    }
}