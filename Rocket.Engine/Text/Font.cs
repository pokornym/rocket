using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK;
using Rocket.Engine.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Rocket.Engine.Text {
	public sealed class Font {
		private readonly int _size;
		private readonly int _lineHeight;
		private readonly Dictionary<char, Glyph> _map = new Dictionary<char, Glyph>();

		public Font(string fl) {
			string dir = Path.GetDirectoryName(fl);
			Dictionary<int, Image<Rgba32>> pages = new Dictionary<int, Image<Rgba32>>();
			foreach (FontInstruction i in File.ReadAllLines(fl).Select(i => i.Trim()).Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => new FontInstruction(i))) {
				switch (i.Instruction) {
					case "info":
						_size = int.Parse(i["size"]);
						break;
					case "common":
						_lineHeight = int.Parse(i["lineheight"]);
						break;
					case "page":
						pages.Add(int.Parse(i["id"]), SixLabors.ImageSharp.Image.Load(Path.Combine(dir, i["file"])));
						break;
					case "char":
						AddGlyph(Encoding.Unicode.GetChars(BitConverter.GetBytes(int.Parse(i["id"])))[0], pages[int.Parse(i["page"])], int.Parse(i["x"]), int.Parse(i["y"]), int.Parse(i["width"]), int.Parse(i["height"]), int.Parse(i["xoffset"]), int.Parse(i["yoffset"]), int.Parse(i["xadvance"]));
						break;
				}
			}
		}

		public void Render(IFontRenderer ren, string txt, float x, float y, float s) {
			s = s / _size;
			int lns = 0;
			using (StringReader sr = new StringReader(txt)) {
				string ln = null;
				while ((ln = sr.ReadLine()) != null) {
					float offset = 0;
					foreach (Glyph g in ln.Select(i => _map[i])) {
						ren.RenderChar(g.Texture, x + offset + g.Offset.X * s, y + (lns * _lineHeight * s) + g.Offset.Y * s, g.Size.X * s, g.Size.Y * s);
						offset += g.Spacing * s;
					}

					lns++;
				}
			}
		}

		private void AddGlyph(char c, Image<Rgba32> p, int x, int y, int w, int h, int ox, int oy, int sp) {
			Image img = new Image(w, h);
			for (int i = 0; i < w; i++)
				for (int j = 0; j < h; j++) {
					Rgba32 px = p[x + i, y + j];
					img[i, j] = new Color(px.R, px.G, px.B, px.A);
				}

			_map.Add(c, new Glyph {
				Spacing = sp,
				Texture = new Texture(img, TextureWrapModes.ClampToBorder, TextureFilterModes.Nearest, TextureMipmapModes.Nearest),
				Offset = new Vector2(ox, oy),
				Size = new Vector2(w, h)
			});
		}

		private struct Glyph {
			public Texture Texture;
			public int Spacing;
			public Vector2 Offset;
			public Vector2 Size;
		}
	}
}