using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AOC_8_1
{
	public class LayeredImage
	{
		public LayeredImage(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public Bitmap AsBitMap()
		{
			var reversedLayers = Layers.Reverse(); // Makes iteration easier
			Bitmap flag = new Bitmap(Width, Height);
			foreach (var layer in reversedLayers)
			{
				for (int y = 0; y < layer.Colours.Count(); y++)
				{
					var line = layer.Colours[y];
					for (int x = 0; x < line.Length; x++)
					{
						var val = line[x];
						if (val == 2)
						{
							continue;
						}
						if (val == 0)
						{
							flag.SetPixel(x, y, Color.Black);
						}
						if (val == 1)
						{
							flag.SetPixel(x, y, Color.White);
						}

					}
				}
			}
			return flag;
		}
		public int Width { get; }
		public int Height { get; }

		public ICollection<Layer> Layers { get; set; } = new List<Layer>();

		public void FromString(string input)
		{
			var amount_of_layers = input.Length / (Width * Height);

			for (int i = 0; i < amount_of_layers; i++)
			{
				var layer_offset = i * (Width * Height);
				var layer_to_add = new Layer(Width, Height);

				for (int j = 0; j < Height; j++)
				{
					var line_offset = j * Width;
					var line = new List<int>();
					for (int k = 0; k < Width; k++)
					{
						line.Add(int.Parse(input[k + layer_offset + line_offset].ToString()));
					}
					layer_to_add.Colours.Add(line.ToArray());
				}
				Layers.Add(layer_to_add);
			}
		}

		public int GetValidationNumber()
		{
			var s = Layers.OrderByDescending(r => r.Colours.SelectMany(r => r).Where(x => x != 0).Count()).First();
			var numberCounts = s.Colours.SelectMany(r => r).GroupBy(r => r).Select(x => new { k = x.Key, v = x.Count() });
			return numberCounts.First(e => e.k == 1).v * numberCounts.First(e => e.k == 2).v;
		}
	}

	public class Layer
	{
		public Layer(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Width { get; }
		public int Height { get; }

		public List<int[]> Colours { get; set; } = new List<int[]>();
	}
}
