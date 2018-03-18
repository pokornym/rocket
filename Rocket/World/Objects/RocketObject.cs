using System;
using OpenTK;
using Rocket.Render;

namespace Rocket.World.Objects {
	internal sealed class RocketObject : WorldObject {
		private const float FUEL_MASS = 0.5f;
		private const float FORCE = 1f;

		public int Fuel {
			get => _fuel;
			set {
				_fuel = value;
				Mass = _bMass + _fuel * FUEL_MASS;
			}
		}
		private readonly ModelHandle _model;
		private int _fuel;
		private readonly float _bMass;
		private readonly Stabilizer _slz = new Stabilizer();

		public RocketObject(float s, float m, Model r) : base(new CircleCollider(s)) {
			_model = Attach(r);
			_bMass = m;
			Transformation.Scale = s;
			Fuel = 100;
		}

		public void MoveCW(float m) => Move(-(float)(Math.PI / 4), m);

		public void MoveForward(float m) => Move(0, m);

		public void MoveCCW(float m) => Move((float)(Math.PI / 4), m);

		public void StopMovement() {
			Acceleration = Vector2.Zero;
		}

		public override bool Tick() {
			if (Velocity != Vector2.Zero) {
				float target = (float)Math.Atan2(Velocity.Y, Velocity.X);
				float start = Transformation.Angle + (float)Math.PI / 2;

				if (target - start <= start - target)
					Transformation.Angle += (target - start) / 3f;
				else
					Transformation.Angle -= (start - target) / 3f;
			}

			return base.Tick();
		}

		private void Move(float ang, float m) {
//			float delta = _slz.GetDelta();
			Vector2 vec = Velocity.Normalized();
			vec = new Vector2((float)Math.Cos(ang) * vec.X - (float)Math.Sin(ang) * vec.Y, (float)Math.Sin(ang) * vec.X + (float)Math.Cos(ang) * vec.Y);
			Acceleration = vec * FORCE * m;
		}
	}
}