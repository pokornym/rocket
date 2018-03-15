using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.Features {
	internal sealed class DepthFeature : IFeature {
		public enum Functions {
			Never = 0x200,
			Less = 0x201,
			Equal = 0x202,
			Lequal = 0x203,
			Greater = 0x204,
			Notequal = 0x205,
			Gequal = 0x206,
			Always = 0x207
		}

		public readonly Functions Function;

		public DepthFeature(Functions f = Functions.Less) {
			Function = f;
		}

		public void Attach() {
			GL.Enable(EnableCap.DepthTest);
			GL.DepthFunc((DepthFunction) Function);
		}

		public void Before() {
			GL.Clear(ClearBufferMask.DepthBufferBit);
		}

		public void After() { }

		public void Detach() {
			GL.Disable(EnableCap.DepthTest);
		}
	}
}