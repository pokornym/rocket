using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal class SphereModel : Model {
		protected override IVertexRenderer Vertices => _indices;
		private readonly IndexBuffer _indices;

		public SphereModel(IVertexCoder<Vertex> coder, int bups, Color c, bool trans = false) {
			IsTransparent = trans;
			List<Triangle> triangles = new List<Triangle> { new Triangle(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1)) };
			for (int i = 0; i < bups; i++)
				for (int j = triangles.Count - 1; j >= 0; j--) {
					triangles.AddRange(triangles[j].BreakUp());
					triangles.RemoveAt(j);
				}

			int octant = triangles.Count;
			for (int i = 1; i <= 3; i++) {
				float sin = (float) Math.Sin(i * (float) Math.PI / 2);
				float cos = (float) Math.Cos(i * (float) Math.PI / 2);
				for (int j = octant - 1; j >= 0; j--) {
					Triangle self = triangles[j];
					Triangle rot = self.RotateZ(sin, cos);
					triangles.Add(rot);
					if (i == 1)
						triangles.Add(self.FlipZ());
					triangles.Add(rot.FlipZ());
				}
			}

			SimpleMesh mesh = new SimpleMesh(c);
			mesh.AddRange(triangles.Select(i => new Engine.Geometry.Triangle(i.A, i.B, i.C)));
			_indices = mesh.ToBuffer(coder);
		}

		private sealed class Triangle {
			public readonly Vector3 A;
			public readonly Vector3 B;
			public readonly Vector3 C;

			public Triangle(Vector3 a, Vector3 b, Vector3 c) {
				A = a;
				B = b;
				C = c;
			}

			public Triangle RotateZ(float sin, float cos) => new Triangle(RotateZ(A, sin, cos), RotateZ(B, sin, cos), RotateZ(C, sin, cos));

			public Triangle FlipZ() => new Triangle(FlipZ(C), FlipZ(B), FlipZ(A));

			public IEnumerable<Triangle> BreakUp() {
				Vector3 ab = HalfVector(A, B).Normalized();
				Vector3 bc = HalfVector(B, C).Normalized();
				Vector3 ac = HalfVector(A, C).Normalized();

				yield return new Triangle(A, ab, ac);
				yield return new Triangle(ab, B, bc);
				yield return new Triangle(ab, bc, ac);
				yield return new Triangle(ac, bc, C);
			}

			private static Vector3 RotateZ(Vector3 v, float sin, float cos) => new Vector3(cos * v.X - sin * v.Y, sin * v.X + cos * v.Y, v.Z);

			private static Vector3 FlipZ(Vector3 v) => new Vector3(v.X, v.Y, -v.Z);

			private static Vector3 HalfVector(Vector3 a, Vector3 b) => b + (a - b) / 2;
		}
	}
}