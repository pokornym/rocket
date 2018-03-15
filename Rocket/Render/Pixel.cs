namespace Rocket.Render {
	internal sealed class Pixel {
		public readonly byte A;
		public readonly byte B;
		public readonly byte G;
		public readonly byte R;
		public readonly int X;
		public readonly int Y;

		public Pixel(int x, int y, byte r, byte g, byte b, byte a) {
			R = r;
			G = g;
			B = b;
			A = a;
			X = x;
			Y = y;
		}

		public override string ToString() {
			return $"Color: [{R}, {G}, {B}, {A}], Location: [{X}, {Y}]";
		}
	}
}