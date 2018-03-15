using System;
using OpenTK;
using Rocket.Render.OpenGL;

namespace Rocket.Render {
	internal sealed class Image : IPixelReader {
		public int Width { get; }
		public int Height { get; }
		private readonly Color[,] _data;
		private int _column;
		private int _row;

		public Image(int w, int h) {
			_data = new Color[Width = w, Height = h];
		}

		public void Rewind() {
			_row = 0;
			_column = 0;
		}

		public Pixel Read() {
			if (_row == Height)
				throw new InvalidOperationException();
			Color col = _data[_column, _row];
			Pixel pxl = new Pixel(_column, _row, col.R, col.G, col.B, col.A);
			_column++;
			if (_column == Width) {
				_column = 0;
				_row++;
			}

			return pxl;
		}

		public void Draw(int x, int y, Image img) {
			int tX = Math.Min(Width - 1, x + img.Width - 1);
			int tY = Math.Min(Height - 1, y + img.Height - 1);

			for (int i = Math.Max(x, 0); i <= tX; i++)
				for (int j = Math.Max(y, 0); j <= tY; j++)
					_data[i, j] = img._data[i - x, j - y];
		}

		public Image Trim(int x, int y, int w, int h) {
			Image ret = new Image(w, h);
			for (int i = 0; i < w; i++)
				for (int j = 0; j < h; j++)
					ret._data[i, j] = _data[i + x, j + y];
			return ret;
		}
		
		public Color this[int x, int y] {
			get => _data[x, y];
			set => _data[x, y] = value;
		}
	}
}