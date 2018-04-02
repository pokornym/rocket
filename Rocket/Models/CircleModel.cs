using System;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Render;

namespace Rocket.Models {
	internal class CircleModel : Model {
		private readonly VertexArray<Vertex> _vertices;
		private readonly IndexBuffer _indices;
		private int _triangles;

		public CircleModel(IVertexCoder<Vertex> coder, int t, Color c) : base(1) {
			_triangles = t;
			_vertices = new VertexArray<Vertex>(coder, t + 1);
			_indices = new IndexBuffer(t + 2, _vertices);
			float step = (float) (Math.PI * 2 / t);
			_vertices[0] = new Vertex(new Vector2(0, 0), c, new Vector2(0, 0));
			_indices[0] = 0;
			for (int i = 0; i < t; i++) {
				Vector2 vec = new Vector2((float) Math.Cos(step * (i - 1)), (float) Math.Sin(step * (i - 1)));
				_vertices[i + 1] = new Vertex(vec, c, vec);
				_indices[i + 1] = (uint) (i + 1);
			}

			_indices[t + 1] = 1;
			Faces[0] = new Face(_indices, GeometricPrimitives.TriangleFan);
		}
	}
}