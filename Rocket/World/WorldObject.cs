using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Engine;
using Rocket.Render;

namespace Rocket.World {
	internal abstract class WorldObject : WorldElement {
		public override Vector3 Position {
			get => base.Position;
			set {
				Transformation.Position = value;
				base.Position = value;
			}
		}
		public override Vector3 Scale {
			get => base.Scale;
			set {
				Transformation.Scale = value;
				base.Scale = value;
			}
		}
		public override Vector3 Rotation {
			get => base.Rotation;
			set {
				Transformation.Rotation = value;
				base.Rotation = value;
			}
		}
		public Vector3 Velocity = Vector3.Zero;
		public Vector3 Force = Vector3.Zero;
		public Vector3 Torque = Vector3.Zero;
		public Vector3 AngularMomentum = Vector3.Zero;
		public float Mass = 1;
		public readonly Transformation Transformation = new Transformation();
		public IEnumerable<ModelHandle> Handles => _handels;
		protected Universe Universe { get; private set; }
		public readonly bool Bulk;
		private readonly List<ModelHandle> _handels = new List<ModelHandle>();

		public WorldObject(bool bulk, Vector3 a, ICollider col) : base(col) {
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