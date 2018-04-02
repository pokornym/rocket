using System;
using Rocket.Engine.OpenGL;

namespace Rocket.Engine {
	public sealed class Face {
		public readonly IVertexRenderer Indices;
		public readonly GeometricPrimitives Primitive;

		public Face(IVertexRenderer ic, GeometricPrimitives pr) {
			Indices = ic ?? throw new ArgumentNullException(nameof(ic));
			Primitive = pr;
		}

		public void Draw() {
			Indices.Draw(Primitive);
		}
	}
}