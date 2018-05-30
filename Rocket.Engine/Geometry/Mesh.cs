using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using Rocket.Engine.OpenGL;

namespace Rocket.Engine.Geometry {
	public abstract class Mesh<T> : Mesh where T : struct {
		public IndexBuffer ToBuffer(IVertexCoder<T> coder) {
			List<T> vertices = new List<T>();
			List<Vector3> vecs = new List<Vector3>();
			List<uint> indices = new List<uint>();

			foreach (Triangle t in Triangles) {
				foreach (Vector3 v in t) {
					uint idx;
					if (vecs.Contains(v))
						idx = (uint) vecs.IndexOf(v);
					else {
						idx = (uint) vecs.Count;
						vecs.Add(v);
						vertices.Add(ToVertex(v, Triangles.Where(i => i.Contains(v)).Aggregate(Vector3.Zero, (current, i) => current + i.Normal).Normalized()));
					}

					indices.Add(idx);
				}
			}

			return new IndexBuffer(indices, new VertexArray<T>(coder, vertices));
		}

		protected abstract T ToVertex(Vector3 pos, Vector3 norm);
	}

	public class Mesh : ICollection<Triangle> {
		public int Count => Triangles.Count;
		public bool IsReadOnly => false;
		protected readonly List<Triangle> Triangles = new List<Triangle>();

		public IEnumerator<Triangle> GetEnumerator() => Triangles.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(Triangle i) {
			if (!Triangles.Contains(i))
				Triangles.Add(i);
		}

		public void AddRange(IEnumerable<Triangle> range) {
			foreach (Triangle t in range.Where(i => !Triangles.Contains(i)))
				Triangles.Add(t);
		}

		public void Clear() => Triangles.Clear();

		public bool Contains(Triangle i) => Triangles.Contains(i);

		public void CopyTo(Triangle[] arr, int idx) => Triangles.CopyTo(arr, idx);

		public bool Remove(Triangle i) => Triangles.Remove(i);

		public static IEnumerable<Triangle> Quad(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
			yield return new Triangle(a, b, d);
			yield return new Triangle(b, c, d);
		}
	}
}