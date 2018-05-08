using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public class IndexBuffer : Buffer, IVertexRenderer {
		public int ElementCount { get; }
		private readonly IVertexCollection _vtx;

		public IndexBuffer(int count, IVertexCollection vtx = null) : base(BufferTypes.Index, count * sizeof(uint)) {
			ElementCount = count;
			_vtx = vtx;
		}
		
		public IndexBuffer(ICollection<uint> idx, IVertexCollection vtx = null) : this(idx.Count, vtx) {
			int i = 0;
			foreach (uint j in idx)
				this[i++] = j;
		}

		public void Draw(GeometricPrimitives gp) {
			Draw(gp, 0, ElementCount);
		}

		public void Draw(GeometricPrimitives gp, int count) {
			Draw(gp, 0, count);
		}

		public void Draw(GeometricPrimitives gp, int start, int count) {
			if (start < 0 || start + count > ElementCount)
				throw new IndexOutOfRangeException();
			Use(() => GL.DrawElements((PrimitiveType) gp, count, DrawElementsType.UnsignedInt, start));
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