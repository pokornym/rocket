using Rocket.Render;

namespace Rocket.World.Objects {
	internal sealed class SpaceObject : WorldObject {
		private readonly ModelHandle _body;
		private readonly ModelHandle _atmosphere;
		
		public SpaceObject(float r, float a, float m, Model body, Model atm) : base(new CircleCollider(r)) {
			Mass = m;
			_body = Attach(body);
			_body.Transformation.Scale = r;
			_atmosphere = Attach(atm);
			_atmosphere.Transformation.Scale = r + a;
		}

		public override bool Tick() {
			_body.Angle += 0.005f;
			_atmosphere.Angle -= 0.01f;
			return true;
		}
	}
}