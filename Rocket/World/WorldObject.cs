using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World {
	internal class WorldObject {
		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public Matrix4 Transformation = Matrix4.Identity;
		public float Mass = 1;
		public readonly Collider Collider;
		public readonly Model Model;
		protected Universe Universe { get; private set; }

		public WorldObject(Model m, Collider col) {
			Model = m ?? throw new ArgumentNullException(nameof(m));
			Collider = col ?? throw new ArgumentNullException(nameof(col));
		}

		public virtual void Tick() { }

		public virtual void OnCreation(Universe uni) => Universe = uni;
	}
}