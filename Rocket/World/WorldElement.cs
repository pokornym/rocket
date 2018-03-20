using System;
using OpenTK;

namespace Rocket.World {
	internal abstract class WorldElement {
		public virtual Vector2 Aspect { get; set; } = Vector2.One;
		public virtual Vector2 Position { get; set; }
		public virtual Vector2 Scale { get; set; } = Vector2.One;
		public virtual float Angle { get; set; }
		public readonly ICollider Collider;

		public WorldElement(ICollider coll) => Collider = coll ?? throw new ArgumentNullException(nameof(coll));
		
		public bool IsCollision(WorldElement other) {
			if ((Position - other.Position).Length > GetExtent() + other.GetExtent())
				return false;
			return Collider.Order >= other.Collider.Order ? other.Collider.IsCollision(other, this) : Collider.IsCollision(this, other);
		}
		
		private float GetExtent() => (Aspect * Scale).Length;
	}
}