using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Rocket.Engine.OpenGL;

namespace Rocket.Engine.Geometry {
	public abstract class Mesh<T> : ICollection<Triangle> where T : struct {
		public int Count => _triangles.Count;
		public bool IsReadOnly => false;
		private readonly List<Triangle> _triangles = new List<Triangle>();

		public IndexBuffer ToBuffer(IVertexCoder<T> coder) {
			List<T> vertices = new List<T>();
			List<Vector3> vecs = new List<Vector3>();
			List<uint> indices = new List<uint>();

			foreach (Triangle t in _triangles) {
				foreach (Vector3 v in t) {
					uint idx;
					if (vecs.Contains(v))
						idx = (uint) vecs.IndexOf(v);
					else {
						idx = (uint) vecs.Count;
						vecs.Add(v);
						vertices.Add(ToVertex(v, _triangles.Where(i => i.Contains(v)).Aggregate(Vector3.Zero, (current, i) => current + i.Normal).Normalized()));
					}

					indices.Add(idx);
				}
			}

			return new IndexBuffer(indices, new VertexArray<T>(coder, vertices));
		}

		protected abstract T ToVertex(Vector3 pos, Vector3 norm);

		public IEnumerator<Triangle> GetEnumerator() => _triangles.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(Triangle i) {
			if (!_triangles.Contains(i))
				_triangles.Add(i);
		}

		public void AddRange(IEnumerable<Triangle> range) {
			foreach (Triangle t in range.Where(i => !_triangles.Contains(i)))
				_triangles.Add(t);
		}

		public void Clear() => _triangles.Clear();

		public bool Contains(Triangle i) => _triangles.Contains(i);

		public void CopyTo(Triangle[] arr, int idx) => _triangles.CopyTo(arr, idx);

		public bool Remove(Triangle i) => _triangles.Remove(i);
	}
}