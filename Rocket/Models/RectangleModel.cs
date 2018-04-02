using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RectangleModel : Model {
		private readonly VertexArray<Vertex> _vtx;

		public RectangleModel(IVertexCoder<Vertex> coder, Color c) : base(1) {
			_vtx = new VertexArray<Vertex>(coder, 4) {
				[0] = new Vertex(new Vector2(-1, -1), c, new Vector2(-1, -1)),
				[1] = new Vertex(new Vector2(-1, 1), c, new Vector2(-1, 1)),
				[2] = new Vertex(new Vector2(1, 1), c, new Vector2(1, 1)),
				[3] = new Vertex(new Vector2(1, -1), c, new Vector2(1, -1))
			};
			Faces[0] = new Face(_vtx, GeometricPrimitives.Quads);
		}
	}
}