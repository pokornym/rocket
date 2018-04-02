using System.IO;

namespace Rocket.Engine.OpenGL {
	public interface IVertexCoder<T> where T : struct {
		VertexLayout Layout { get; }

		T FromBytes(Stream str);
		void ToBytes(T vtx, Stream str);
	}
}