using OpenTK;

namespace Rocket.World {
	public abstract class Collider {
		public readonly int Order;

		public Collider(int o) => Order = o;

		public bool Collide(Vector2 aPos, Vector2 bPos, Collider bCol) => Order < bCol.Order ? bCol.IsCollision(bPos, aPos, this) : IsCollision(aPos, bPos, bCol);

		protected abstract bool IsCollision(Vector2 aPos, Vector2 bPos, Collider bCol);
	}
}