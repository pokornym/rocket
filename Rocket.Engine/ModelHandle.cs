using System;
using OpenTK;

namespace Rocket.Engine {
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
		public readonly Model Model;
		public readonly Transformation Transformation = new Transformation();

		public ModelHandle(Model mod) => Model = mod ?? throw new ArgumentNullException(nameof(mod));

		public void Draw() => Model.Draw();
	}
}