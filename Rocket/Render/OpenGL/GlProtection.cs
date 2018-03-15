using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal static class GlProtection {
		public static void Protect(Action f) {
			while (GL.GetError() != ErrorCode.NoError)
				continue;
			f();
			FailIfError();
		}

		public static void FailIfError() {
			ErrorCode err = GL.GetError();
			if (err != ErrorCode.NoError)
				throw new GlErrorException(err);
		}
	}
}