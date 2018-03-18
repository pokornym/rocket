using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Render;

namespace Rocket.World {
	internal abstract class WorldObject : CollisionBody {
		public override Vector2 Position {
			get => Transformation.Position;
			set => Transformation.Position = value;
		}
		public override Vector2 Scale {
			get => Transformation.Scale;
			set => Transformation.Scale = value;
		}
		public override float Angle {
			get => Transformation.Angle;
			set => Transformation.Angle = value;
		}
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public float Mass = 1;
		public readonly Transformation Transformation = new Transformation();
		public IEnumerable<ModelHandle> Handles => _handels;
		protected Universe Universe { get; private set; }
		public readonly bool Bulk;
		private readonly List<ModelHandle> _handels = new List<ModelHandle>();

		public WorldObject(bool bulk, Vector2 a) {
			Bulk = bulk;
			Aspect = a;
		}

		public virtual bool Tick() => true;

		public virtual void OnCreation(Universe uni) => Universe = uni;

		public virtual void OnCollision(WorldObject obj) { }

		protected ModelHandle Attach(Model mod) {
			ModelHandle handle = new ModelHandle(mod);
			_handels.Add(handle);
			return handle;
		}

		protected void Detach(ModelHandle handle) => _handels.Remove(handle);
	}
}