namespace Rocket.Scenery {
	public sealed class CircleCollider : ICollider {
		public readonly int Radius;

		public CircleCollider(int r) => Radius = r;
		
		public bool IsCollision(ICollider col) {
			return false;
		}
	}
}