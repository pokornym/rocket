using System;
using OpenTK;

namespace Rocket.World {
	internal sealed class SimpleObject : WorldElement {
		public readonly ICollider Collider;

		public SimpleObject(ICollider coll) : base(coll) { }
	}
}