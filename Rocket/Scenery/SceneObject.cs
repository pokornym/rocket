using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.Scenery {
	internal class SceneObject {
		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public Matrix4 Transformation = Matrix4.Identity;
		public float Mass = 1;
		public readonly ICollider Collider;
		public readonly Model Model;

		public SceneObject(Model m, ICollider col) {
			Model = m ?? throw new ArgumentNullException(nameof(m));
			Collider = col ?? throw new ArgumentNullException(nameof(col));
		}
	}
}