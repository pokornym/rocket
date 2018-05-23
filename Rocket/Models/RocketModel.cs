using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	/*internal sealed class RocketModel : Model {
		private static readonly float COS_HPI = (float)Math.Cos(Math.PI / 2);
		private static readonly float SIN_HPI = (float)Math.Sin(Math.PI / 2);
		protected override Face[] Faces { get; }

		public RocketModel(IVertexCoder<Vertex> coder) {
			Faces = new Face[4];
			Faces[0] = new Face(Compose(new[] {
				new Vector3(0, 0, 1),
				new Vector3(1f / 3, -1f / 3, 1f / 3),
				new Vector3(1f / 3, 1f / 3, 1f / 3)
			}, Color.IndianRed, coder), GeometricPrimitives.Triangles);
			Faces[1] = new Face(Compose(new[] {
				new Vector3(1f / 3, -1f / 3, 1f / 3),
				new Vector3(1f / 3, -1f / 3, -1),
				new Vector3(1f / 3, 1f / 3, -1),
				new Vector3(1f / 3, 1f / 3, 1f / 3)
			}, Color.Blue, coder), GeometricPrimitives.Quads);
			Faces[2] = new Face(Compose(new[] {
				new Vector3(1f / 3,0, -1f / 3),
				new Vector3(1f / 3,0, -1f),
				new Vector3(2f / 3,0, -1f)
			}, Color.Orange, coder), GeometricPrimitives.Triangles);
			Faces[3] = new Face(new VertexArray<Vertex>(coder, new List<Vertex> {
				new Vertex(new Vector3(1f / 3, -1f / 3, -1f), Color.Blue, Vector2.Zero),
				new Vertex(new Vector3(-1f / 3, -1f / 3, -1f), Color.Blue, Vector2.Zero),
				new Vertex(new Vector3(-1f / 3, 1f / 3, -1f), Color.Blue, Vector2.Zero),
				new Vertex(new Vector3(1f / 3, 1f / 3, -1f), Color.Blue, Vector2.Zero)
			}), GeometricPrimitives.Quads);
		}

		private static VertexArray<Vertex> Compose(Vector3[] buff, Color col, IVertexCoder<Vertex> coder) {
			int offset = 0;
			VertexArray<Vertex> vtx = new VertexArray<Vertex>(coder, buff.Length * 4);
			for (int i = 0; i < 4; i++) {
				Vertices(buff);
				Rotate(ref buff);
			}

			return vtx;

			void Vertices(IEnumerable<Vector3> vec) {
				foreach (Vector3 v in vec)
					vtx[offset++] = new Vertex(v, col, Vector2.Zero);
			}
		}

		private static void Rotate(ref Vector3[] buff) {
			for (int i = 0; i < buff.Length; i++) {
				Vector3 v = buff[i];
				buff[i] = new Vector3(COS_HPI * v.X - SIN_HPI * v.Y, SIN_HPI * v.X + COS_HPI * v.Y, v.Z);
			}
		}
	}*/
}