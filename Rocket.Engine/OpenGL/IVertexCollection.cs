namespace Rocket.Engine.OpenGL {
	public interface IVertexCollection : IBindable {
		int VertexCount { get; }

		int VertexIndex(int idx);
		int AttributeIndex(int idx, int off);
	}
}