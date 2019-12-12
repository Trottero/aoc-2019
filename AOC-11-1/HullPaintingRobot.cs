using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC_11_1
{
	public class HullPaintingRobot : IntCodeComputer
	{
		private Dictionary<Position, HullTile> Dictionary { get; set; } = new Dictionary<Position, HullTile>();
		private Position HullPosition { get; set; } = new Position();
		private Direction Direction { get; set; } = Direction.UP;
		private int DirectionNum { get; set; } = 0;

		private List<int> CommandBuffer { get; set; } = new List<int>();

		public HullPaintingRobot(string program) : base(program)
		{
			Dictionary.Add(HullPosition, new HullTile());
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
			return GetNumberFromColor(Dictionary[HullPosition].Color);
		}

		public int GetCount()
		{
			return Dictionary.Keys.Count - 1;
		}

		public void ProcessMovementCommand(int turnDirection, int colorToPaint)
		{
			// Turndirection
			// 0: left 90
			// 1: right 90

			// Colortopaint
			// 0: black
			// 1: white
			if (!Dictionary.ContainsKey(HullPosition))
			{
				Dictionary.Add(HullPosition, new HullTile());
			}

			Dictionary[HullPosition] = new HullTile
			{
				Color = GetColorForNumber(colorToPaint)
			};

			// Rotate robot
			Direction = GetDirectionFromNumber(turnDirection);

			// Move to the next position
			HullPosition = GetNextRelativeHullPoistion(HullPosition, Direction);

			// Add this one aswell
			if (!Dictionary.ContainsKey(HullPosition))
			{
				Dictionary.Add(HullPosition, new HullTile());
			}
		}
		public Position GetNextRelativeHullPoistion(Position currentPos, Direction direction)
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
		public Color Color { get; set; } = Color.Black;
	}

	public class Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public override bool Equals(object obj)
		{
			var cast = (Position)obj;
			return X == cast.X && Y == cast.Y;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}

	public enum Direction
	{
		UP, DOWN, LEFT, RIGHT
	}
}
