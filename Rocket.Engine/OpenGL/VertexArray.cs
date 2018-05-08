using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public class VertexArray : BoundElement, IVertexCollection, IVertexRenderer {
		private static int? _boundId;
		public readonly VertexBuffer Buffer;
		public readonly VertexLayout Layout;
		protected override int? BoundId {
			get => _boundId;
			set => _boundId = value;
		}
		public int VertexCount => Buffer.VertexCount;

		public VertexArray(VertexLayout layout, int count) {
			Layout = layout;
			Buffer = new VertexBuffer(count, layout.VertexSize);

			Create();

			Bind();
			Layout.SetPointers();
			Unbind();
		}

		public int VertexIndex(int idx) {
			return Buffer.VertexIndex(idx);
		}

		public int AttributeIndex(int idx, int off) {
			return Buffer.AttributeIndex(idx, off);
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

		protected override void BindElement() {
			GL.BindVertexArray(Id.Value);
			Buffer.Bind();
		}

		protected override void UnbindElement() {
			GL.BindVertexArray(0);
			Buffer.Unbind();
		}

		protected override int CreateElement() {
			return GL.GenVertexArray();
		}

		protected override void DeleteElement() {
			GL.DeleteVertexArray(Id.Value);
			Buffer.Dispose();
		}
	}

	public class VertexArray<TVertex> : VertexArray, IEnumerable<TVertex> where TVertex : struct {
		private readonly IVertexCoder<TVertex> _coder;
		private int _enumerators;

		public VertexArray(IVertexCoder<TVertex> coder, int count) : base(coder.Layout, count) {
			_coder = coder;
		}
		
		public VertexArray(IVertexCoder<TVertex> coder, ICollection<TVertex> vtx) : this(coder, vtx.Count) {
			int i = 0;
			foreach (TVertex v in vtx)
				this[i++] = v;
		}

		public IEnumerator<TVertex> GetEnumerator() {
			_enumerators++;
			return new VertexArrayEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public TVertex this[int idx] {
			get {
				if (idx < 0 || idx >= VertexCount)
					throw new ArgumentOutOfRangeException(nameof(idx));
				Buffer.Stream.Position = VertexIndex(idx);
				return _coder.FromBytes(Buffer.Stream);
			}

			set {
				if (_enumerators > 0)
					throw new InvalidOperationException("Can't change contents of buffer, while it's being enumerated!");
				if (idx < 0 || idx >= VertexCount)
					throw new ArgumentOutOfRangeException(nameof(idx));
				Buffer.Stream.Position = VertexIndex(idx);
				_coder.ToBytes(value, Buffer.Stream);
			}
		}

		private sealed class VertexArrayEnumerator : IEnumerator<TVertex> {
			private readonly VertexArray<TVertex> _array;
			private int _index;

			public VertexArrayEnumerator(VertexArray<TVertex> arr) {
				_array = arr;
			}

			public TVertex Current => _index < _array.VertexCount ? _array[_index] : default(TVertex);
			object IEnumerator.Current => Current;

			public bool MoveNext() {
				return ++_index < _array.VertexCount;
			}

			public void Reset() {
				_index = 0;
			}

			public void Dispose() {
				_array._enumerators--;
			}
		}
	}
}