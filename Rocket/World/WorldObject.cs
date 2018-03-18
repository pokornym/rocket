using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Render;

namespace Rocket.World {
	internal abstract class WorldObject {
		public Vector2 Position {
			get => Transformation.Position;
			set => Transformation.Position = value;
		}
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public float Mass = 1;
		public readonly Transformation Transformation = new Transformation();
		public IEnumerable<ModelHandle> Handles => _handels;
		protected Universe Universe { get; private set; }
		public readonly bool Bulk;
		private readonly List<ModelHandle> _handels = new List<ModelHandle>();
		private readonly Vector2 _aspect;

		public WorldObject(bool bulk, Vector2 a) {
			Bulk = bulk;
			_aspect = a;
		}

		public bool IsCollision(WorldObject other) {
			if ((Transformation.Position - other.Transformation.Position).Length > GetExtent() + other.GetExtent())
				return false;
			Vector2[] selfV = GetCorners();
			Vector2[] otherV = other.GetCorners();
			return SAT(GetNormals(), selfV, otherV) && SAT(other.GetNormals(), selfV, otherV);
		}

		public virtual bool Tick() => true;

		public virtual void OnCreation(Universe uni) => Universe = uni;

		public virtual void OnCollision(WorldObject obj) { }

		protected ModelHandle Attach(Model mod) {
			ModelHandle handle = new ModelHandle(mod);
			_handels.Add(handle);
			return handle;
		}

		protected void Detach(ModelHandle handle) => _handels.Remove(handle);

		private Vector2[] GetCorners() {
			Vector2 hBox = Transformation.Scale * _aspect;
			return new[] {
				new Vector2(-hBox.X, hBox.Y) + Transformation.Position,
				new Vector2(hBox.X, hBox.Y) + Transformation.Position,
				new Vector2(hBox.X, -hBox.Y) + Transformation.Position,
				new Vector2(-hBox.X, -hBox.Y) + Transformation.Position
			};
		}

		private Vector2[] GetNormals() {
			float sin = (float) Math.Sin(Transformation.Angle);
			float cos = (float) Math.Cos(Transformation.Angle);
			return new[] { new Vector2(cos, sin), new Vector2(-sin, cos) };
		}

		private float GetExtent() => (_aspect * Transformation.Scale).Length;

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