using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC_11_1
{
    public class HullPaintingRobot : IntCodeComputer
    {
        private List<HullTile> KnownTiles { get; set; } = new List<HullTile>();
        private HullTile CurrentPosition { get; set; } = new HullTile();
        private Direction Direction { get; set; } = Direction.UP;
        private int DirectionNum { get; set; } = 0;

        private List<int> CommandBuffer { get; set; } = new List<int>();

        public HullPaintingRobot(string program) : base(program)
        {
        }

        public override void SendOutput(long output)
        {
            base.SendOutput(output);
            CommandBuffer.Add((int)output);
            if (CommandBuffer.Count == 2)
            {
                ProcessMovementCommand(CommandBuffer[1], CommandBuffer[0]);
                CommandBuffer.Clear();
            }
        }

        public override long GetInput()
        {
            return GetNumberFromColor(CurrentPosition.Color);
        }

        public int GetCount()
        {
            return KnownTiles.Count;
        }

        public void ProcessMovementCommand(int turnDirection, int colorToPaint)
        {
            // Turndirection
            // 0: left 90
            // 1: right 90

            // Colortopaint
            // 0: black
            // 1: white
            CurrentPosition.Color = GetColorForNumber(colorToPaint);

            // Rotate robot
            Direction = GetDirectionFromNumber(turnDirection);

            // Move to the next position
            var nextHullPos = GetNextRelativeHullPosition(CurrentPosition.Position.Clone(), Direction);
            
            // Check if the tile exists
            var existingTile = KnownTiles.FirstOrDefault(h =>
                h.Position.X == CurrentPosition.Position.X && h.Position.Y == CurrentPosition.Position.Y);

            if (existingTile != null)
            {
                KnownTiles = KnownTiles.Where(t => t.Position.X != existingTile.Position.X || t.Position.Y != existingTile.Position.Y).ToList();
            }
            KnownTiles.Add(new HullTile
            {
                Position = nextHullPos.Clone()
            });
        }
        public Position GetNextRelativeHullPosition(Position currentPos, Direction direction)
        {
            var nextPosition = new Position
            {
                X = currentPos.X,
                Y = currentPos.Y
            };

            switch (direction)
            {
                case Direction.UP:
                    nextPosition.Y += 1;
                    break;
                case Direction.DOWN:
                    nextPosition.Y -= 1;
                    break;
                case Direction.LEFT:
                    nextPosition.X -= 1;
                    break;
                case Direction.RIGHT:
                    nextPosition.X += 1;
                    break;
            }

            return nextPosition;
        }

        public Direction GetDirectionFromNumber(int turnDirection)
        {
            if (turnDirection == 0)
            {
                DirectionNum -= 90;
                if (DirectionNum < 0)
                {
                    DirectionNum += 360;
                }
            }
            if (DirectionNum == 1)
            {
                DirectionNum += 90;
                DirectionNum %= 360;
            }

            switch (DirectionNum)
            {
                case 0:
                    return Direction.UP;
                case 90:
                    return Direction.RIGHT;
                case 180:
                    return Direction.DOWN;
                case 270:
                    return Direction.LEFT;
                default:
                    break;
            }
            throw new Exception("Direction Number is illegal!");
        }

        public Color GetColorForNumber(int number)
        {
            if (number == 0)
            {
                return Color.Black;
            }
            if (number == 1)
            {
                return Color.White;
            }
            return Color.Red;
        }

        public int GetNumberFromColor(Color color)
        {
            if (color == Color.Black)
            {
                return 0;
            }
            if (color == Color.White)
            {
                return 1;
            }
            return -1;
        }
    }

    public class HullTile
    {
        public Position Position { get; set; } = new Position();
        public Color Color { get; set; } = Color.Black;
    }

    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            var cast = (Position)obj;
            return X == cast.X && Y == cast.Y;
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public Position Clone()
        {
            return new Position
            {
                Y = Y,
                X = X
            };
        }
    }

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
}
