using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World.Colliders {
	internal sealed class ObbCollider : ICollider {
		public int Order => 1;
		
		public bool IsCollision(WorldElement self, WorldElement other) {
			Vector2[] selfV = GetCorners(self);
			Vector2[] otherV = GetCorners(other);
			return SAT(GetNormals(self), selfV, otherV) && SAT(GetNormals(other), selfV, otherV);
		}
		
		private Vector2[] GetCorners(WorldElement obj) {
			Vector2 hBox = obj.Scale * obj.Aspect;
			float sin = (float) Math.Sin(obj.Angle);
			float cos = (float) Math.Cos(obj.Angle);
			return new[] {
				new Vector2(-hBox.X * cos - hBox.Y * sin, -hBox.X * sin + hBox.Y * cos) + obj.Position,
				new Vector2(hBox.X * cos - hBox.Y * sin, hBox.X * sin + hBox.Y * cos) + obj.Position,
				new Vector2(hBox.X * cos + hBox.Y * sin, -hBox.X * sin - hBox.Y * cos) + obj.Position,
				new Vector2(-hBox.X * cos + hBox.Y * sin, -hBox.X * sin - hBox.Y * cos) + obj.Position
			};
		}

		private Vector2[] GetNormals(WorldElement obj) {
			float sin = (float) Math.Sin(obj.Angle);
			float cos = (float) Math.Cos(obj.Angle);
			return new[] { new Vector2(cos, sin), new Vector2(-sin, cos) };
		}

		private static bool SAT(Vector2[] normals, Vector2[] self, Vector2[] other) {
			foreach (Vector2 n in normals) {
				Project(n, self, out float selfMin, out float selfMax);
				Project(n, other, out float otherMin, out float otherMax);
				if (!(selfMin <= otherMin && otherMin <= selfMax || otherMin <= selfMin && selfMin <= otherMax))
					return false;
			}

			return true;
		}

		private static void Project(Vector2 axis, Vector2[] vtx, out float min, out float max) {
			min = float.PositiveInfinity;
			max = float.NegativeInfinity;
			foreach (Vector2 v in vtx) {
				float dot = Vector2.Dot(v, axis);
				if (dot < min) min = dot;
				if (dot > max) max = dot;
			}
		}
	}
}