using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal class Shader : GlElement {
		public readonly string Code;
		public readonly ShaderTypes Type;

		public Shader(ShaderTypes t, string code) {
			Code = code ?? throw new ArgumentNullException(nameof(code));
			Type = t;
			Create();
		}

		protected override int CreateElement() {
			int id = GL.CreateShader((ShaderType) Type);
			GL.ShaderSource(id, Code);
			GL.CompileShader(id);
			string error = GL.GetShaderInfoLog(id);
			if (!string.IsNullOrWhiteSpace(error))
				throw new ShaderException(this, error);
			return id;
		}

		protected override void DeleteElement() {
			GL.DeleteShader(Id.Value);
		}
	}
}