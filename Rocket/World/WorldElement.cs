using System;
using OpenTK;

namespace Rocket.World {
	internal abstract class WorldElement {
		public virtual Vector3 Aspect { get; set; } = Vector3.One;
		public virtual Vector3 Position { get; set; }
		public virtual Vector3 Scale { get; set; } = Vector3.One;
		public virtual Vector3 Rotation {
			get => _rotation;
			set {
				_rotation = new Vector3(Normalize(value.X), Normalize(value.Y), Normalize(value.Z));
				float Normalize(float v) => v < 0 ? ((float) Math.PI * 2) + (v % ((float) Math.PI * 2)) : v % ((float) Math.PI * 2);
			}
		}
		public Light? Light { get; protected set; }
		public LightSource? LightSource => Light == null ? null : (LightSource?) new LightSource { Light = Light.Value, Position = Position };
		public readonly ICollider Collider;
		private Vector3 _rotation = Vector3.Zero;

		public WorldElement(ICollider coll) => Collider = coll ?? throw new ArgumentNullException(nameof(coll));

		public bool IsCollision(WorldElement other) {
			if ((Position - other.Position).Length > GetExtent() + other.GetExtent())
				return false;
			return Collider.Order >= other.Collider.Order ? other.Collider.IsCollision(other, this) : Collider.IsCollision(this, other);
		}

		private float GetExtent() => (Aspect * Scale).Length;
	}
}