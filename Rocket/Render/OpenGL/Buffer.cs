using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal class Buffer : BoundElement {
		private static int? _boundId;
		private readonly byte[] _data;
		private readonly BufferStream _stream;
		protected override int? BoundId {
			get => _boundId;
			set => _boundId = value;
		}
		public int Length => _data.Length;
		public Stream Stream => _stream;
		public readonly BufferTypes Type;

		public Buffer(BufferTypes type, int l) {
			Type = type;
			Create();
			_data = new byte[l];
			_stream = new BufferStream(_data);
		}

		protected override void BindElement() {
			GL.BindBuffer((BufferTarget) Type, Id.Value);
			if (_stream.Update())
				GL.BufferData((BufferTarget) Type, _data.Length, _data, BufferUsageHint.StaticDraw);
		}

		protected override int CreateElement() {
			return GL.GenBuffer();
		}

		protected override void DeleteElement() {
			GL.DeleteBuffer(Id.Value);
		}

		protected override void UnbindElement() {
			GL.BindBuffer((BufferTarget) Type, 0);
		}

		private sealed class BufferStream : MemoryStream {
			private bool _change;

			public BufferStream(byte[] buff) : base(buff) {
				_change = true;
			}

			public bool Update() {
				if (!_change)
					return false;

				_change = false;
				return true;
			}

			public override void Write(byte[] buffer, int offset, int count) {
				_change = true;
				base.Write(buffer, offset, count);
			}

			public override void WriteByte(byte value) {
				_change = true;
				base.WriteByte(value);
			}
		}
	}
}