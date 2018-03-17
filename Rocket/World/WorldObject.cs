using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Render;

namespace Rocket.World {
	internal abstract class WorldObject {
		public Vector2 Position {
			get => Transformation.Position;
			set => Transformation.Position = value;
		}
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public float Mass = 1;
		public readonly Collider Collider;
		public readonly Transformation Transformation = new Transformation();
		public IEnumerable<ModelHandle> Handles => _handels;
		protected Universe Universe { get; private set; }
		private readonly List<ModelHandle> _handels = new List<ModelHandle>();

		public WorldObject(Collider col) {
			Collider = col ?? throw new ArgumentNullException(nameof(col));
		}

		public virtual void Tick() { }

		public virtual void OnCreation(Universe uni) => Universe = uni;

		protected ModelHandle Attach(Model mod) {
			ModelHandle handle = new ModelHandle(mod);
			_handels.Add(handle);
			return handle;
		}

		protected void Detach(ModelHandle handle) => _handels.Remove(handle);
	}
}