using OpenTK;
using Rocket.Engine.Geometry;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class SimpleMesh : Mesh<Vertex> {
		private readonly Color _colour;

		public SimpleMesh(Color colour) => _colour = colour;

		protected override Vertex ToVertex(Vector3 pos, Vector3 norm) => new Vertex(pos, _colour, norm);
	}
}