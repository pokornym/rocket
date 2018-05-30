using System;
using OpenTK;
using Rocket.Engine;
using Rocket.Render;
using Rocket.World.Colliders;

namespace Rocket.World.Objects {
	internal sealed class SpaceObject : WorldObject {
		public Vector3 Position {
			get => base.Position;
			set {
				AtmosphereBody.Position = value;
				base.Position = value;
			}
		}
		public Vector3 Scale {
			get => base.Scale;
			set {
				AtmosphereBody.Scale = _atRatio * value;
				base.Scale = value;
			}
		}
		public readonly SimpleObject AtmosphereBody = new SimpleObject(new CircleCollider());
		private readonly ModelHandle _body;
		private readonly ModelHandle _atmosphere;
		private readonly float _atRatio;

		public SpaceObject(float a, float m, Model sph) : base(true, Vector3.One, new CircleCollider()) {
			Mass = m;
			_body = Attach(sph, new Material { Color = new Vector4(0, 1, 0, 1) });
			_body.Scale = Vector3.One;
			_atRatio = a;
			if (_atRatio > 1) {
				_atmosphere = Attach(sph, new Material { Color = new Vector4(0.3f, 0.3f, 1f, 0.5f), Ambient = 0.1f, Diffusion = 5f });
				_atmosphere.Scale = new Vector3(_atRatio);
			}

			if (Mass > 10000)
				Light = new Light { Color = Vector3.One, Intensity = 100f };

			SetProperties();
		}

		public override void OnCollision(WorldObject obj) {
			Mass += obj.Mass;
			SetProperties();
			base.OnCollision(obj);
		}

		public override bool Tick() {
			_body.Rotation = new Vector3(_body.Rotation.X, _body.Rotation.Y + 0.002f, _body.Rotation.Z + 0.005f);
			if (_atmosphere != null)
				_atmosphere.Rotation = new Vector3(_body.Rotation.X + 0.02f, _body.Rotation.Y, _body.Rotation.Z - 0.01f);
			return true;
		}

		private void SetProperties() {
			Scale = new Vector3((float) Math.Log(Mass, 2) * 5f);
		}
	}
}