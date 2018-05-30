using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RectangleModel : Model {
		protected override IVertexRenderer Vertices => _idx;
		private readonly VertexArray<Vertex> _vtx;
		private readonly IndexBuffer _idx;

		public RectangleModel() : base(GeometricPrimitives.Quads) { }

		public RectangleModel(IVertexCoder<Vertex> coder) {
			_vtx = new VertexArray<Vertex>(coder, 4) {
				[0] = new Vertex(new Vector3(-1, -1, 0), new Vector3(-1, -1, 0).Normalized()),
				[1] = new Vertex(new Vector3(-1, 1, 0), new Vector3(-1, 1, 0).Normalized()),
				[2] = new Vertex(new Vector3(1, 1, 0), new Vector3(1, 1, 0).Normalized()),
				[3] = new Vertex(new Vector3(1, -1, 0), new Vector3(1, -1, 0).Normalized())
			};
			_idx = new IndexBuffer(6, _vtx) {
				[0] = 0,
				[1] = 1,
				[2] = 2,
				[3] = 2,
				[4] = 3,
				[5] = 0
			};
		}
	}
}