using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.Features {
	public sealed class BlendingFeature : IFeature {
		public void Attach() {
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void Before() { }

		public void After() { }

		public void Detach() {
			GL.Disable(EnableCap.Blend);
		}
	}
}