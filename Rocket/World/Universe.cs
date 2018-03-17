using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;

namespace Rocket.World {
	internal sealed class Universe : IEnumerable<WorldObject> {
		private const float G_CONSTNAT = 5;
		private readonly List<WorldObject> _objects = new List<WorldObject>();

		public void Add(WorldObject obj) {
			_objects.Add(obj);
			obj.OnCreation(this);
		}

		public void Remove(WorldObject obj) => _objects.Remove(obj);

		public void Tick() {
			foreach (WorldObject obj in _objects) {
				obj.Tick();

				// Gravity
				Vector2 accel = obj.Acceleration;
				foreach (WorldObject g in _objects) {
					if (g == obj)
						continue;
					accel += Gravity(obj, g);
				}

				// Dynamics
				obj.Velocity += accel;
				obj.Position += obj.Velocity;
			}
		}

		private static Vector2 Gravity(WorldObject from, WorldObject to) {
			Vector2 dir = to.Position - from.Position;
			return G_CONSTNAT * from.Mass * to.Mass / dir.LengthSquared / from.Mass * dir.Normalized();
		}

		public IEnumerator<WorldObject> GetEnumerator() => _objects.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}