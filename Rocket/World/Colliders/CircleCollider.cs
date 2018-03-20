using System;

namespace Rocket.World.Colliders {
	internal sealed class CircleCollider : ICollider {
		public int Order => 2;

		public bool IsCollision(WorldElement self, WorldElement other) {
			if (other.Collider is CircleCollider)
				return (self.Position - other.Position).Length < self.Scale.Length + other.Scale.Length;

			throw new NotImplementedException();
		}
	}
}