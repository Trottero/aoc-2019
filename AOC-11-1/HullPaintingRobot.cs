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
        public int DirectionNum { get; set; } = 0;

        private List<int> CommandBuffer { get; set; } = new List<int>();

        public HullPaintingRobot(string program) : base(program)
        {
            var startingpos = new HullTile
            {
                Position = new Position
                {
                    X = 0,
                    Y = 0
                },
                Color = Color.Black
            };
            KnownTiles.Add(startingpos);
            CurrentPosition = startingpos;
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
            var nextHullPos = GetNextRelativeHullPosition(CurrentPosition.Position, Direction);

            // Check if the tile exists
            var existingTile = KnownTiles.FirstOrDefault(h =>
                h.Position.X == nextHullPos.X && h.Position.Y == nextHullPos.Y);

            if (existingTile == null)
            {
                existingTile = new HullTile
                {
                    Position = nextHullPos
                };
                KnownTiles.Add(existingTile);
            }

            CurrentPosition = existingTile;

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
                    nextPosition.Y -= 1;
                    break;
                case Direction.DOWN:
                    nextPosition.Y += 1;
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
            if (turnDirection == 1)
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
            throw new ArgumentException($"number: {number} is not supported!");
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

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

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
