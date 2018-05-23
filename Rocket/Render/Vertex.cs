using OpenTK;

namespace Rocket.Render {
	internal struct Vertex {
		public readonly Vector3 Position;
		public readonly Color Color;
		public readonly Vector3 Normal;

		public Vertex(Vector3 pos, Color col, Vector3 norm) {
			Position = pos;
			Color = col;
			Normal = norm;
		}
	}
}