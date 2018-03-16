using System;

namespace Rocket.Render {
	internal class Model {
		protected readonly Face[] Faces;

		public Model(int f) => Faces = new Face[f];

		public void Draw() {
			foreach (Face f in Faces)
				f.Draw();
		}
	}
}