using System;
using Rocket.Engine.Geometry;
using Rocket.Engine.OpenGL;

namespace Rocket.Engine {
	public abstract class Model {
		protected abstract IVertexRenderer Vertices { get; }
		private readonly GeometricPrimitives _primitive;

		public Model(GeometricPrimitives prim = GeometricPrimitives.Triangles) => _primitive = prim;

		public void Draw() => Vertices?.Draw(_primitive);
	}
}