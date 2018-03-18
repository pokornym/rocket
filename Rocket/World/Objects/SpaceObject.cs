using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World.Objects {
	internal sealed class SpaceObject : WorldObject {
		public override Vector2 Position {
			get => base.Position;
			set {
				AtmosphereBody.Position = value;
				base.Position = value;
			}
		}
		public override Vector2 Scale {
			get => base.Scale;
			set {
				AtmosphereBody.Scale = _atRatio * value;
				base.Scale = value;
			}
		}
		public readonly CollisionBody AtmosphereBody = new CollisionBody();
		private readonly ModelHandle _body;
		private readonly ModelHandle _atmosphere;
		private readonly float _atRatio;

		public SpaceObject(float a, float m, Model body, Model atm) : base(true, Vector2.One) {
			Mass = m;
			_atmosphere = Attach(atm);
			_atmosphere.Scale = new Vector2(_atRatio = a);
			_body = Attach(body);
			_body.Scale = Vector2.One;
			SetProperties();
		}

		public override void OnCollision(WorldObject obj) {
			Mass += obj.Mass;
			SetProperties();
			base.OnCollision(obj);
		}

		public override bool Tick() {
			_body.Angle += 0.005f;
			_atmosphere.Angle -= 0.01f;
			return true;
		}

		private void SetProperties() {
			Scale = new Vector2((float) Math.Log(Mass, 2) * 5f);
		}
	}
}