using Rocket.Render;

namespace Rocket.World {
	internal interface ICollider {
		int Order { get; }
		bool IsCollision(WorldElement self, WorldElement other);
	}
}