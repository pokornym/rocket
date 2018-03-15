using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal class IndexBuffer : Buffer, IVertexRenderer {
		public int ElementCount { get; }
		private readonly IVertexCollection _vtx;

		public IndexBuffer(int count, IVertexCollection vtx = null) : base(BufferTypes.Index, count * sizeof(uint)) {
			ElementCount = count;
			_vtx = vtx;
		}

		public void Draw(GeometricPrimitives gp) {
			Draw(gp, 0, ElementCount);
		}

		public void Draw(GeometricPrimitives gp, int count) {
			Draw(gp, 0, count);
		}

		public void Draw(GeometricPrimitives gp, int start, int count) {
			Bind();
			if (start < 0 || start + count > ElementCount)
				throw new IndexOutOfRangeException();
			GL.DrawElements((PrimitiveType) gp, count, DrawElementsType.UnsignedInt, start);
		}

		protected override void BindElement() {
			_vtx?.Bind();
			base.BindElement();
		}

		protected override void UnbindElement() {
			_vtx?.Unbind();
			base.UnbindElement();
		}
		
		public uint this[int idx] {
			get {
				if (idx < 0 || idx >= ElementCount)
					throw new ArgumentOutOfRangeException(nameof(idx));
				Stream.Position = idx * sizeof(uint);
				byte[] buffer = new byte[sizeof(uint)];
				Stream.Read(buffer, 0, sizeof(uint));
				return BitConverter.ToUInt32(buffer, 0);
			}

			set {
				if (idx < 0 || idx >= ElementCount)
					throw new ArgumentOutOfRangeException(nameof(idx));
				if (value >= _vtx.VertexCount)
					throw new ArgumentOutOfRangeException(nameof(value));
				Stream.Position = idx * sizeof(uint);
				Stream.Write(BitConverter.GetBytes(value), 0, sizeof(uint));
			}
		}
	}
}