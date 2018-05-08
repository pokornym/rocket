using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RectangleModel : Model {
		protected override Face[] Faces { get; }
		private readonly VertexArray<Vertex> _vtx;

		public RectangleModel(IVertexCoder<Vertex> coder, Color c) {
			_vtx = new VertexArray<Vertex>(coder, 4) {
				[0] = new Vertex(new Vector3(-1, -1, 0), c, new Vector2(-1, -1)),
				[1] = new Vertex(new Vector3(-1, 1, 0), c, new Vector2(-1, 1)),
				[2] = new Vertex(new Vector3(1, 1, 0), c, new Vector2(1, 1)),
				[3] = new Vertex(new Vector3(1, -1, 0), c, new Vector2(1, -1))
			};
			Faces = new[] { new Face(_vtx, GeometricPrimitives.Quads) };
		}
	}
}