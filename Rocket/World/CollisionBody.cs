using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World {
	internal class CollisionBody {
		public virtual Vector2 Position { get; set; } = Vector2.Zero;
		public virtual Vector2 Aspect { get; set; } = Vector2.One;
		public virtual Vector2 Scale { get; set; } = Vector2.One;
		public virtual float Angle { get; set; } = 0;

		public bool IsCollision(CollisionBody other) {
			if ((Position - other.Position).Length > GetExtent() + other.GetExtent())
				return false;
			Vector2[] selfV = GetCorners();
			Vector2[] otherV = other.GetCorners();
			return SAT(GetNormals(), selfV, otherV) && SAT(other.GetNormals(), selfV, otherV);
		}

		private Vector2[] GetCorners() {
			Vector2 hBox = Scale * Aspect;
			float sin = (float) Math.Sin(Angle);
			float cos = (float) Math.Cos(Angle);
			return new[] {
				new Vector2(-hBox.X * cos - hBox.Y * sin, -hBox.X * sin + hBox.Y * cos) + Position,
				new Vector2(hBox.X * cos - hBox.Y * sin, hBox.X * sin + hBox.Y * cos) + Position,
				new Vector2(hBox.X * cos + hBox.Y * sin, -hBox.X * sin - hBox.Y * cos) + Position,
				new Vector2(-hBox.X * cos + hBox.Y * sin, -hBox.X * sin - hBox.Y * cos) + Position
			};
		}

		private Vector2[] GetNormals() {
			float sin = (float) Math.Sin(Angle);
			float cos = (float) Math.Cos(Angle);
			return new[] { new Vector2(cos, sin), new Vector2(-sin, cos) };
		}

		private float GetExtent() => (Aspect * Scale).Length;

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