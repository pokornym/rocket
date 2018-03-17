using System;

namespace Rocket.Render {
	internal sealed class ModelHandle {
		public readonly Model Model;
		public readonly Transformation Transformation = new Transformation();

		public ModelHandle(Model mod) => Model = mod ?? throw new ArgumentNullException(nameof(mod));

		public void Draw() => Model.Draw();
	}
}