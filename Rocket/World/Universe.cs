using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;

namespace Rocket.World {
	internal sealed class Universe : IEnumerable<WorldObject> {
		private const float G_CONSTNAT = 5;
		public int TimeWrap = 1;
		private readonly List<WorldObject> _objects = new List<WorldObject>();
		private readonly Stabilizer _stz = new Stabilizer();

		public void Add(WorldObject obj) {
			_objects.Add(obj);
			obj.OnCreation(this);
		}

		public void Remove(WorldObject obj) => _objects.Remove(obj);

		public void Tick() {
			float delta = _stz.GetDelta();
			for (int i = 0; i < TimeWrap; i++)
			for (int j = _objects.Count - 1; j >= 0; j--) {
				WorldObject obj = _objects[j];
				if (!obj.Tick()) {
					_objects.RemoveAt(j);
					continue;
				}

				// Gravity and collisions
				Vector2 accel = obj.Acceleration;
				bool col = false;
				foreach (WorldObject g in _objects) {
					if (g == obj)
						continue;
					if (g.Bulk && (!obj.Bulk || obj.Mass <= g.Mass) && obj.IsCollision(g)) {
						g.OnCollision(obj);
						col = true;
					}

					accel += Gravity(obj, g);
				}

				if (col) {
					_objects.RemoveAt(j);
					continue;
				}

				// Dynamics
				obj.Velocity += accel * delta;
				obj.Position += obj.Velocity * delta;
			}
		}

		private static Vector2 Gravity(WorldObject from, WorldObject to) {
			Vector2 dir = to.Position - from.Position;
			return G_CONSTNAT * to.Mass / dir.LengthSquared * dir.Normalized();
		}

		public IEnumerator<WorldObject> GetEnumerator() => _objects.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}