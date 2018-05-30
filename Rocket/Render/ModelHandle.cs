using System;
using OpenTK;
using Rocket.Engine;
using Rocket.World;

namespace Rocket.Render {
	public sealed class ModelHandle {
		public Vector3 Position {
			get => Transformation.Position;
			set => Transformation.Position = value;
		}
		public Vector3 Rotation {
			get => Transformation.Rotation;
			set => Transformation.Rotation = value;
		}
		public Vector3 Scale {
			get => Transformation.Scale;
			set => Transformation.Scale = value;
		}
		public readonly Transformation Transformation = new Transformation();
		public readonly Model Model;
		public Material Material;

		public ModelHandle(Model mod) => Model = mod ?? throw new ArgumentNullException(nameof(mod));

		public ModelHandle(Model mod, Material mat) {
			Model = mod ?? throw new ArgumentNullException(nameof(mod));
			Material = mat;
		}
	}
}