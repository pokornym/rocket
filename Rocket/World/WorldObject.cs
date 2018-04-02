using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Engine;
using Rocket.Render;

namespace Rocket.World {
	internal abstract class WorldObject : WorldElement {
		public Vector2 Position {
			get => base.Position;
			set {
				Transformation.Position = value;
				base.Position = value;
			}
		}
		public Vector2 Scale {
			get => base.Scale;
			protected set {
				Transformation.Scale = value;
				base.Scale = value;
			}
		}
		public float Angle {
			get => base.Angle;
			protected set {
				Transformation.Angle = value;
				base.Angle = value;
			}
		}
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public float Mass = 1;
		public readonly Transformation Transformation = new Transformation();
		public IEnumerable<ModelHandle> Handles => _handels;
		protected Universe Universe { get; private set; }
		public readonly bool Bulk;
		private readonly List<ModelHandle> _handels = new List<ModelHandle>();

		public WorldObject(bool bulk, Vector2 a, ICollider col) : base(col) {
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