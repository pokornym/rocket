using System;
using System.IO;
using OpenTK;
using Rocket.Engine.OpenGL;

namespace Rocket.Render {
	internal sealed class VertexCoder : IVertexCoder<Vertex> {
		public VertexLayout Layout { get; }

		public VertexCoder() {
			Layout = new VertexLayout();
			Layout.AddFloat("Position", 2);
			Layout.AddByte("Color", 4, true);
			Layout.AddFloat("UV", 2);
		}

		public Vertex FromBytes(Stream str) {
			byte[] vtx = new byte[sizeof(float) * 4 + sizeof(byte) * 4];
			str.Read(vtx, 0, vtx.Length);
			return
				new Vertex(
					new Vector2(
						BitConverter.ToSingle(vtx, 0), 
						BitConverter.ToSingle(vtx,sizeof(float))
						),
					new Color(
						vtx[sizeof(float) * 2],
						vtx[sizeof(float) * 2+1],
						vtx[sizeof(float) * 2+2],
						vtx[sizeof(float) * 2+3]
						), 
					new Vector2(
						BitConverter.ToSingle(vtx, sizeof(float) * 2 + 4),
						BitConverter.ToSingle(vtx,sizeof(float) * 3 + 4)
						)
				);
		}

		public void ToBytes(Vertex vtx, Stream str) {
			byte[] buffer = new byte[sizeof(float) * 4 + sizeof(byte) * 4];
			// Position
			Array.Copy(BitConverter.GetBytes(vtx.Position.X), 0, buffer, 0, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.Position.Y), 0, buffer, sizeof(float), sizeof(float));
			
			// Color
			buffer[sizeof(float) * 2] = vtx.Color.R;
			buffer[sizeof(float) * 2+1] = vtx.Color.G;
			buffer[sizeof(float) * 2+2] = vtx.Color.B;
			buffer[sizeof(float) * 2+3] = vtx.Color.A;
			
			
			// UV
			Array.Copy(BitConverter.GetBytes(vtx.UV.X), 0, buffer, sizeof(float) * 2 + 4, sizeof(float));
			Array.Copy(BitConverter.GetBytes(vtx.UV.Y), 0, buffer, sizeof(float) * 3 + 4, sizeof(float));
			str.Write(buffer, 0, buffer.Length);
		}
	}
}