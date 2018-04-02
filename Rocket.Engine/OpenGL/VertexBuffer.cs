using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public class VertexBuffer : Buffer, IVertexCollection, IVertexRenderer {
		public readonly int VertexSize;
		public int VertexCount { get; }

		public VertexBuffer(int count, int size) : base(BufferTypes.Vertex, count * size) {
			VertexCount = count;
			VertexSize = size;
		}

		public int VertexIndex(int idx) {
			return VertexSize * idx;
		}

		public int AttributeIndex(int idx, int off) {
			return VertexIndex(idx) + off;
		}

		public void Draw(GeometricPrimitives gp) {
			Draw(gp, 0, VertexCount);
		}

		public void Draw(GeometricPrimitives gp, int count) {
			Draw(gp, 0, count);
		}

		public void Draw(GeometricPrimitives gp, int start, int count) {
			if (start < 0 || start + count > VertexCount)
				throw new IndexOutOfRangeException();
			Use(() => GL.DrawArrays((PrimitiveType) gp, start, count));
		}
	}
}