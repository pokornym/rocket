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

		public override void Tick() {
			_body.Transformation.Angle += 0.001f;
			_atmosphere.Transformation.Angle -= 0.01f;
		}
	}
}