using System;

namespace Rocket {
	internal sealed class Stabilizer {
		private const int LAG_THRESHOLD = 500;
		private readonly float _unit;
		private int? _tick;

		public Stabilizer(float unit = 100f) => _unit = unit;

		public float GetDelta() {
			float delta = _tick == null || (Environment.TickCount - _tick) >= LAG_THRESHOLD ? 1 : (float) (Environment.TickCount - _tick) / _unit;
			_tick = Environment.TickCount;
			return delta;
		}

		public void Tick() => _tick = Environment.TickCount;
	}
}