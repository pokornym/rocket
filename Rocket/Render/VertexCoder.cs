using System;
using System.IO;
using OpenTK;
using Rocket.Engine.OpenGL;

namespace Rocket.Render {
	internal sealed class VertexCoder : IVertexCoder<Vertex> {
		public const int SIZE = sizeof(float) * 3 * 2;

		public VertexLayout Layout { get; }

		public VertexCoder() {
			Layout = new VertexLayout();
			Layout.AddFloat("Position", 3);
			Layout.AddFloat("Normal", 3);
		}

		public Vertex FromBytes(Stream str) {
			byte[] vtx = new byte[SIZE];
			str.Read(vtx, 0, vtx.Length);
			return
				new Vertex(
					new Vector3(
						BitConverter.ToSingle(vtx, 0),
						BitConverter.ToSingle(vtx, sizeof(float)),
						BitConverter.ToSingle(vtx, sizeof(float) * 2)
					),
					new Vector3(
						BitConverter.ToSingle(vtx, sizeof(float) * 3),
						BitConverter.ToSingle(vtx, sizeof(float) * 4),
						BitConverter.ToSingle(vtx, sizeof(float) * 5)
					)
				);
		}

		public void ToBytes(Vertex vtx, Stream str) {
			byte[] buffer = new byte[SIZE];
			// Position
			Array.Copy(BitConverter.GetBytes(vtx.Position.X), 0, buffer, 0, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Position.Y), 0, buffer, sizeof(float), sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Position.Z), 0, buffer, sizeof(float) * 2, sizeof(float));

			// Normal
			Array.Copy(BitConverter.GetBytes(vtx.Normal.X), 0, buffer, sizeof(float) * 3, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Normal.Y), 0, buffer, sizeof(float) * 4, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Normal.Z), 0, buffer, sizeof(float) * 5, sizeof(float));

			str.Write(buffer, 0, buffer.Length);
		}
	}
}