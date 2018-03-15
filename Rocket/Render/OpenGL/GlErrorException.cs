using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal sealed class GlErrorException : Exception {
		public readonly ErrorCode Code;

		public GlErrorException(ErrorCode code) : base($"OpenGL ERROR: {code:G}") {
			Code = code;
		}
	}
}