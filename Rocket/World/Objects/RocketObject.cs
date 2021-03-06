﻿using System;
using OpenTK;
using Rocket.Engine;
using Rocket.Models;
using Rocket.Render;
using Rocket.World.Colliders;

namespace Rocket.World.Objects {
	internal sealed class RocketObject : WorldObject {
		public float Fuel { get; private set; }
		public readonly float MaxFuel;
		private readonly ModelHandle _model;
		private const float BASE_MASS = 5;

		public RocketObject(float s, Model m, float f = 5000) : base(false, Vector3.One, new CircleCollider()) {
			_model = Attach(m, new Material { Color = new Vector4(1, 1, 0, 1), Diffusion = 5f, Ambient = 0.1f });
			Scale = s * Vector3.One;
			MaxFuel = f;
			Fuel = f;
		}

		public override bool Tick() {
			if (Fuel == 0) {
				Force = Vector3.Zero;
				Torque = Vector3.Zero;
			} else {
				Fuel = Math.Max(Fuel - Force.Length, 0);
				Fuel = Math.Max(Fuel - Torque.Length, 0);
			}

			Mass = BASE_MASS + Fuel * 0.05f;

			return base.Tick();
		}
	}
}