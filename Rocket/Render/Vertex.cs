using OpenTK;

namespace Rocket.Render {
	internal struct Vertex {
		public readonly Vector2 Position;
		public readonly Color Color;
		public readonly Vector2 UV;

		public Vertex(Vector2 pos, Color col, Vector2 uv) {
			Position = pos;
			Color = col;
			UV = uv;
		}
	}
}