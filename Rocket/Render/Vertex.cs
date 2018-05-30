using OpenTK;

namespace Rocket.Render {
	internal struct Vertex {
		public readonly Vector3 Position;
		public readonly Vector3 Normal;

		public Vertex(Vector3 pos, Vector3 norm) {
			Position = pos;
			Normal = norm;
		}
	}
}