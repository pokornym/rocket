using System;

namespace Rocket.Engine.OpenGL {
	internal sealed class ShaderException : Exception {
		public readonly Shader Shader;

		public ShaderException(Shader sh, string msg) : base(msg) {
			Shader = sh;
		}
	}
}