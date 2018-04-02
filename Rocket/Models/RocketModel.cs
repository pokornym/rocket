using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RocketModel : Model {
		private readonly VertexArray<Vertex> _hull;
		private readonly VertexArray<Vertex> _tip;
		private readonly VertexArray<Vertex> _lFin;
		private readonly VertexArray<Vertex> _rFin;

		public RocketModel(IVertexCoder<Vertex> coder) : base(4) {
			_tip = new VertexArray<Vertex>(coder, 3) {
				[0] = new Vertex(new Vector2(0, 1), Color.IndianRed, new Vector2(1 / 2f, 0)),
				[1] = new Vertex(new Vector2(-1 / 3f, 1 / 3f), Color.IndianRed, new Vector2(0, 1)),
				[2] = new Vertex(new Vector2(1 / 3f, 1 / 3f), Color.IndianRed, new Vector2(1, 1))
			};
			Faces[0] = new Face(_tip, GeometricPrimitives.Triangles);
			_hull = new VertexArray<Vertex>(coder, 4) {
				[0] = new Vertex(new Vector2(-1 / 3f, 1 / 3f), Color.Blue, new Vector2(0, 0)),
				[1] = new Vertex(new Vector2(-1 / 3f, -1), Color.Blue, new Vector2(0, 1)),
				[2] = new Vertex(new Vector2(1 / 3f, -1), Color.Blue, new Vector2(1, 1)),
				[3] = new Vertex(new Vector2(1 / 3f, 1 / 3f), Color.Blue, new Vector2(1, 0))
			};
			Faces[1] = new Face(_hull, GeometricPrimitives.Quads);
			_lFin = new VertexArray<Vertex>(coder, 3) {
				[0] = new Vertex(new Vector2(-1 / 3f, -1 / 3f), Color.Orange, new Vector2(1, 0)),
				[1] = new Vertex(new Vector2(-1 / 3f, -1), Color.Orange, new Vector2(1, 1)),
				[2] = new Vertex(new Vector2(-2 / 3f, -1), Color.Orange, new Vector2(0, 1))
			};
			Faces[2] = new Face(_lFin, GeometricPrimitives.Triangles);
			_rFin = new VertexArray<Vertex>(coder, 3) {
				[0] = new Vertex(new Vector2(1 / 3f, -1 / 3f), Color.Orange, new Vector2(0, 0)),
				[1] = new Vertex(new Vector2(1 / 3f, -1), Color.Orange, new Vector2(0, 1)),
				[2] = new Vertex(new Vector2(2 / 3f, -1), Color.Orange, new Vector2(1, 1))
			};
			Faces[3] = new Face(_rFin, GeometricPrimitives.Triangles);
		}
	}
}