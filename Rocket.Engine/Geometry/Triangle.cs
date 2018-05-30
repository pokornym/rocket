using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Rocket.Engine.Geometry {
	public sealed class Triangle : IEnumerable<Vector3>, IEquatable<Triangle> {
		public readonly Vector3 A;
		public readonly Vector3 B;
		public readonly Vector3 C;
		public Vector3 Normal => Vector3.Cross(B - A, C - A).Normalized();

		public Triangle(Vector3 a, Vector3 b, Vector3 c) {
			A = a;
			B = b;
			C = c;
			
		}
		
		public IEnumerable<Triangle> BreakUp() {
			Vector3 ab = HalfVector(A, B).Normalized();
			Vector3 bc = HalfVector(B, C).Normalized();
			Vector3 ac = HalfVector(A, C).Normalized();

			yield return new Triangle(A, ab, ac);
			yield return new Triangle(ab, B, bc);
			yield return new Triangle(ab, bc, ac);
			yield return new Triangle(ac, bc, C);
		}
		
		public Triangle Reverse() => new Triangle(C, B, A);

		public IEnumerator<Vector3> GetEnumerator() {
			return Enumerator().GetEnumerator();
			
			IEnumerable<Vector3> Enumerator() {
				yield return A;
				yield return B;
				yield return C;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		public override string ToString() => $"{{A={ToString(A)}, B={ToString(B)}, C={ToString(C)}}}";

		private string ToString(Vector3 v) => $"[{v.X}, {v.Y}, {v.Z}]";
		
		private static Vector3 HalfVector(Vector3 a, Vector3 b) => b + (a - b) / 2;

		public bool Equals(Triangle other) {
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return A == other.A && B == other.B && C == other.C;
		}
	}
}