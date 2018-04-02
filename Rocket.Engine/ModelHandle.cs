using System;
using OpenTK;

namespace Rocket.Engine {
	public sealed class ModelHandle {
		public Vector2 Position {
			get => Transformation.Position;
			set => Transformation.Position = value;
		}
		public float Angle {
			get => Transformation.Angle;
			set => Transformation.Angle = value;
		}
		public Vector2 Scale {
			get => Transformation.Scale;
			set => Transformation.Scale = value;
		}
		public readonly Model Model;
		public readonly Transformation Transformation = new Transformation();

		public ModelHandle(Model mod) => Model = mod ?? throw new ArgumentNullException(nameof(mod));

		public void Draw() => Model.Draw();
	}
}