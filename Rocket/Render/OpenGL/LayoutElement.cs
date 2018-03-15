using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal sealed class LayoutElement {
		public int TotalSize => Count * Size;
		public readonly int Count;
		public readonly int Index;
		public readonly string Name;
		public readonly bool Normalized;
		public readonly int Offset;
		public readonly int Size;
		public readonly DataTypes Type;

		public LayoutElement(int idx, int off, string nm, int c, DataTypes t, bool norm = false) {
			Index = idx;
			Offset = off;
			Name = nm ?? throw new ArgumentNullException(nameof(nm));
			Count = c;
			Type = t;
			switch (t) {
				case DataTypes.SByte:
				case DataTypes.Byte:
					Size = sizeof(byte);
					break;
				case DataTypes.Short:
				case DataTypes.UShort:
					Size = sizeof(short);
					break;
				case DataTypes.Int:
				case DataTypes.UInt:
					Size = sizeof(int);
					break;
				case DataTypes.Float:
					Size = sizeof(float);
					break;
				case DataTypes.Double:
					Size = sizeof(double);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(t), t, null);
			}

			Normalized = norm;
		}

		public void SetPointer(int stride) {
			GL.VertexAttribPointer(Index, Count, (VertexAttribPointerType) Type, Normalized, stride, Offset);
		}

		public void Enable() {
			GL.EnableVertexAttribArray(Index);
		}

		public void Disable() {
			GL.DisableVertexAttribArray(Index);
		}
	}
}