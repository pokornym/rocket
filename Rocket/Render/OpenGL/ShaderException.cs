using System;

namespace Rocket.Render.OpenGL {
	internal sealed class ShaderException : Exception {
		public readonly Shader Shader;

		public ShaderException(Shader sh, string msg) : base(msg) {
			Shader = sh;
		}
	}
}