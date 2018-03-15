namespace Rocket.Render.OpenGL {
	internal interface IVertexRenderer {
		void Draw(GeometricPrimitives prim);
		void Draw(GeometricPrimitives prim, int count);
		void Draw(GeometricPrimitives prim, int offset, int count);
	}
}