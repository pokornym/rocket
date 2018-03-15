using System.IO;

namespace Rocket.Render.OpenGL {
	internal interface IVertexCoder<T> where T : struct {
		VertexLayout Layout { get; }

		T FromBytes(Stream str);
		void ToBytes(T vtx, Stream str);
	}
}