using System;

namespace Rocket.Render.OpenGL {
	internal sealed class TypeMismatchException : Exception {
		public readonly ShaderElementType ExpectedType;
		public readonly ShaderElementType GivenType;

		public TypeMismatchException(ShaderElementType e, ShaderElementType g) : base($"Expected type '{e}', got '{g}'!") {
			ExpectedType = e ?? throw new ArgumentNullException(nameof(e));
			GivenType = g ?? throw new ArgumentNullException(nameof(g));
		}
	}
}