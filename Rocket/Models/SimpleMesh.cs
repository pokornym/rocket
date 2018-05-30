using OpenTK;
using Rocket.Engine.Geometry;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class SimpleMesh : Mesh<Vertex> {
		protected override Vertex ToVertex(Vector3 pos, Vector3 norm) => new Vertex(pos, norm);
	}
}