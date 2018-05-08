using OpenTK;
using Rocket.Engine;
using Rocket.Models;
using Rocket.World.Colliders;

namespace Rocket.World.Objects {
	internal sealed class RocketObject : WorldObject {
		private readonly ModelHandle _model;
		
		public RocketObject(float s, Model m) : base(false, Vector3.One, new CircleCollider()) {
			_model = Attach(m);
			Scale = s * Vector3.One;
		}
	}
}