using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RectangleModel : Model {
		protected override IVertexRenderer Vertices => _vtx;
		private readonly VertexArray<Vertex> _vtx;

		public RectangleModel() : base(GeometricPrimitives.Quads) { }

		public RectangleModel(IVertexCoder<Vertex> coder, Color c) {
			_vtx = new VertexArray<Vertex>(coder, 4) {
				[0] = new Vertex(new Vector3(-1, -1, 0), c, new Vector3(-1, -1, 0).Normalized()),
				[1] = new Vertex(new Vector3(-1, 1, 0), c, new Vector3(-1, 1, 0).Normalized()),
				[2] = new Vertex(new Vector3(1, 1, 0), c, new Vector3(1, 1, 0).Normalized()),
				[3] = new Vertex(new Vector3(1, -1, 0), c, new Vector3(1, -1, 0).Normalized())
			};
		}
	}
}