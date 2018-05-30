using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.Geometry;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal sealed class RocketModel : Model {
		protected override IVertexRenderer Vertices => _bfr;
		private readonly IndexBuffer _bfr;

		public RocketModel(IVertexCoder<Vertex> coder) {
			SimpleMesh mesh = new SimpleMesh();
			mesh.AddRange(Compose(new Triangle(
				new Vector3(0, 0, 1),
				new Vector3(1f / 3, -1f / 3, 1f / 3),
				new Vector3(1f / 3, 1f / 3, 1f / 3)
			)));
			mesh.AddRange(Compose(Mesh.Quad(
				new Vector3(1f / 3, -1f / 3, 1f / 3),
				new Vector3(1f / 3, -1f / 3, -1),
				new Vector3(1f / 3, 1f / 3, -1),
				new Vector3(1f / 3, 1f / 3, 1f / 3)
			)));
			mesh.AddRange(Compose(new Triangle(
				new Vector3(1f / 3, 0, -1f / 3),
				new Vector3(1f / 3, 0, -1f),
				new Vector3(2f / 3, 0, -1f)
			)));
			mesh.AddRange(Mesh.Quad(
				new Vector3(1f / 3, -1f / 3, -1f),
				new Vector3(-1f / 3, -1f / 3, -1f),
				new Vector3(-1f / 3, 1f / 3, -1f),
				new Vector3(1f / 3, 1f / 3, -1f)
			));
			_bfr = mesh.ToBuffer(coder);
		}

		private static IEnumerable<Triangle> Compose(IEnumerable<Triangle> tri) {
			foreach (Triangle t in tri.SelectMany(Compose))
				yield return t;
		}

		private static IEnumerable<Triangle> Compose(Triangle t) {
			yield return t;
			for (int i = 1; i <= 3; i++) {
				float cos = (float)Math.Cos(i * Math.PI / 2);
				float sin = (float)Math.Sin(i * Math.PI / 2);
				Vector3[] vec = t.Select(v => new Vector3(cos * v.X - sin * v.Y, sin * v.X + cos * v.Y, v.Z)).ToArray();
				yield return new Triangle(vec[0], vec[1], vec[2]);
			}
		}
	}
}