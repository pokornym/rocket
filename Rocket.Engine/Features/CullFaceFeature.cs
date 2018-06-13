using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.Features {
	public sealed class CullFaceFeature : IFeature {
		public void Attach() {
			GL.Enable(EnableCap.CullFace);
		}

		public void Detach() {
			GL.Disable(EnableCap.CullFace);
		}

		public void Before() { }

		public void After() { }
	}
}