using System;
using System.Collections.Generic;
using Rocket.Render;
using Rocket.World.Objects;

namespace Rocket.World {
	internal sealed class RocketFactory {
		private readonly float _scale;
		private readonly Model[] _models;
		private readonly Random _r = new Random();
		
		public RocketFactory(float s, params Model[] mods) {
			_scale = s;
			_models = mods ?? throw new ArgumentNullException(nameof(mods));
			if (mods.Length == 0)
				throw new ArgumentException("Must contain at least one model!", nameof(mods));
		}

		public RocketObject Create(int f, int m) => new RocketObject(_scale, m, f, _models[_r.Next(_models.Length)]);
	}
}