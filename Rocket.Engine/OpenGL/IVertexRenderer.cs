namespace Rocket.Engine.OpenGL {
	public interface IVertexRenderer {
		void Draw(GeometricPrimitives prim);
		void Draw(GeometricPrimitives prim, int count);
		void Draw(GeometricPrimitives prim, int offset, int count);
	}
}