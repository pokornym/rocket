using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World.Objects {
	internal sealed class SpaceObject : WorldObject {
		private readonly ModelHandle _body;
		private readonly ModelHandle _atmosphere;

		public SpaceObject(float a, float m, Model body, Model atm) : base(true, Vector2.One) {
			Mass = m;
			Transformation.Scale = new Vector2((float) Math.Log(m, 2) * 5f);
			_body = Attach(body);
			_body.Transformation.Scale = Vector2.One;
			_atmosphere = Attach(atm);
			_atmosphere.Transformation.Scale = new Vector2(a);
		}

		public override bool Tick() {
			_body.Angle += 0.005f;
			_atmosphere.Angle -= 0.01f;
			return true;
		}
	}
}