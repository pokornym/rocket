using OpenTK;

namespace Rocket.Render {
	internal struct Vertex {
		public readonly Vector3 Position;
		public readonly Color Color;
		public readonly Vector2 UV;

		public Vertex(Vector3 pos, Color col, Vector2 uv) {
			Position = pos;
			Color = col;
			UV = uv;
		}
	}
}