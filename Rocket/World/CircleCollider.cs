using System;
using OpenTK;

namespace Rocket.World {
	internal sealed class CircleCollider : Collider {
		public readonly float Radius;

		public CircleCollider(float r) : base(1) => Radius = r;

		protected override bool IsCollision(Vector2 aPos, Vector2 bPos, Collider bCol) {
			if (bCol is CircleCollider circle)
				return Radius + circle.Radius >= Math.Sqrt(Math.Pow(aPos.X - bPos.X, 2) + Math.Pow(aPos.Y - bPos.Y, 2));
			
			throw new NotImplementedException();
		}
	}
}