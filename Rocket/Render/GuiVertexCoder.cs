using System;
using System.IO;
using OpenTK;
using Rocket.Engine.OpenGL;

namespace Rocket.Render {
	public sealed class GuiVertexCoder : IVertexCoder<GuiVertex> {
		public const int SIZE = sizeof(float) * 2 * 2;
		public VertexLayout Layout { get; } = new VertexLayout();

		public GuiVertexCoder() {
			Layout.AddFloat("position", 2);
			Layout.AddFloat("texCoords", 2);
		}

		public GuiVertex FromBytes(Stream str) {
			byte[] vtx = new byte[SIZE];
			str.Read(vtx, 0, vtx.Length);
			return
				new GuiVertex(
					new Vector2(
						BitConverter.ToSingle(vtx, 0),
						BitConverter.ToSingle(vtx, sizeof(float))
					),
					new Vector2(
						BitConverter.ToSingle(vtx, sizeof(float) * 2),
						BitConverter.ToSingle(vtx, sizeof(float) * 3)
					)
				);
		}

		public void ToBytes(GuiVertex vtx, Stream str) {
			byte[] buffer = new byte[SIZE];
			// Position
			Array.Copy(BitConverter.GetBytes(vtx.Position.X), 0, buffer, 0, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Position.Y), 0, buffer, sizeof(float), sizeof(float));

			// Texture coordinates
			Array.Copy(BitConverter.GetBytes(vtx.TexCoords.X), 0, buffer, sizeof(float) * 2, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.TexCoords.Y), 0, buffer, sizeof(float) * 3, sizeof(float));

			str.Write(buffer, 0, buffer.Length);
		}
	}
}