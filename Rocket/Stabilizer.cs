using System;

namespace Rocket {
	internal sealed class Stabilizer {
		private readonly float _unit;
		private int? _tick;

		public Stabilizer(float unit = 100f) => _unit = unit;

		public float GetDelta() {
			float delta = _tick == null ? 1 : (float) (Environment.TickCount - _tick) / _unit;
			_tick = Environment.TickCount;
			return delta;
		}
	}
}