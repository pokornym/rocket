﻿using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.Features {
	public sealed class Blending : IFeature {
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