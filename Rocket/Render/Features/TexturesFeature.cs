using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.Features {
	public sealed class TexturesFeature : IFeature {
		public void Attach() {
			GL.Enable(EnableCap.Texture2D);
		}

		public void Before() { }

		public void After() { }

		public void Detach() {
			GL.Disable(EnableCap.Texture2D);
		}
	}
}