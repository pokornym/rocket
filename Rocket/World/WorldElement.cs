using System;
using OpenTK;

namespace Rocket.World {
	internal abstract class WorldElement {
		public virtual Vector3 Aspect { get; set; } = Vector3.One;
		public virtual Vector3 Position { get; set; }
		public virtual Vector3 Scale { get; set; } = Vector3.One;
		public virtual Vector3 Rotation { get; set; }
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