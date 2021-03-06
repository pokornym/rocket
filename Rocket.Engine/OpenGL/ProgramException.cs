﻿using System;

namespace Rocket.Engine.OpenGL {
	internal sealed class GlProgramException : Exception {
		public readonly GlProgram Program;

		public GlProgramException(GlProgram prog, string msg) : base(msg) {
			Program = prog;
		}
	}
}