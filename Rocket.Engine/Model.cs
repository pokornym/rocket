namespace Rocket.Engine {
	public abstract class Model {
		public bool IsTransparent { get; protected set; }
		protected abstract Face[] Faces { get; }

		public void Draw() {
			foreach (Face f in Faces)
				f.Draw();
		}
	}
}