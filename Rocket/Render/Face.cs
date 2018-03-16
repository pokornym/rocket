using System;
using Rocket.Render.OpenGL;

namespace Rocket.Render {
	internal sealed class Face {
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