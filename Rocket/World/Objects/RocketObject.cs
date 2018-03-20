using System;
using OpenTK;
using Rocket.Render;
using Rocket.World.Colliders;

namespace Rocket.World.Objects {
	internal sealed class RocketObject : WorldObject {
		private const float FUEL_MASS = 0.5f;
		private const float FORCE = 2f;
		private const float DRAG = 0.2f;

		public float Fuel {
			get => _fuel;
			set {
				_fuel = value;
				Mass = _bMass + _fuel * FUEL_MASS;
			}
		}
		public readonly float MaxFuel;
		private readonly ModelHandle _model;
		private float _fuel;
		private readonly float _bMass;
		private readonly Stabilizer _slz = new Stabilizer();
		private Vector2 _engine = Vector2.Zero;

		public RocketObject(float s, float m, float f, Model r) : base(false, new Vector2(2f / 3, 1f), new ObbCollider()) {
			_model = Attach(r);
			_bMass = m;
			Scale = Vector2.One * s;
			MaxFuel = Fuel = f;
		}

		public void MoveCW(float m) => Move(-(float) (Math.PI * 3 / 8), m);

		public void MoveForward(float m) => Move(0, m);

		public void MoveCCW(float m) => Move((float) (Math.PI * 3 / 8), m);

		public void StopMovement() {
			_engine = Vector2.Zero;
		}

		public override bool Tick() {
			if (Velocity != Vector2.Zero) {
				float target = (float) Math.Atan2(Velocity.Y, Velocity.X);
				float start = Angle + (float) Math.PI / 2;

				if (target - start <= start - target)
					Angle += (target - start) / 3f;
				else
					Angle -= (start - target) / 3f;
			}

			float delta = _slz.GetDelta();
			Vector2 acc = Vector2.Zero;
			if (_engine != Vector2.Zero && _fuel > 0) {
				float m = _engine.Length * delta;
				m = Math.Min(m, _fuel);
				acc = _engine * m;
				_fuel = Math.Max(_fuel - m, 0f);
			}

			foreach (WorldObject obj in Universe) {
				if (obj is SpaceObject s && IsCollision(s.AtmosphereBody))
					acc += -Velocity * DRAG;
			}

			Acceleration = acc;
			return base.Tick();
		}

		private void Move(float ang, float m) {
			Vector2 vec = Velocity.Normalized();
			vec = new Vector2((float) Math.Cos(ang) * vec.X - (float) Math.Sin(ang) * vec.Y, (float) Math.Sin(ang) * vec.X + (float) Math.Cos(ang) * vec.Y);
			_engine = vec * FORCE * m;
		}
	}
}