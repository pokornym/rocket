using System;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;

namespace Rocket.Render {
	public sealed class RenderHandle {
		public readonly Window Window;
		public readonly GlProgram Program;
		private readonly Uniform _uModel;
		private readonly Uniform _uView;
		private readonly Uniform _uProjection;
		private readonly Uniform _uShade;

		public RenderHandle(Window win, GlProgram program, string mod = "uModel", string view = "uView", string proj = "uProjection",  string shade = "uShade") {
			Window = win ?? throw new ArgumentNullException(nameof(win));
			Program = program;
			_uModel = program.GetUniform(mod);
			_uView = program.GetUniform(view);
			_uProjection = program.GetUniform(proj);
			_uShade = program.GetUniform(shade);

			_uModel.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uView.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uProjection.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uShade.EnsureType(new ShaderElementType(PrimitiveTypes.Bool, 1));
		}

		public void SetShade(bool b) => _uShade.Set(b);

		public void SetModel(Matrix4 mtx) => _uModel.Set(mtx);
		
		public void SetView(Matrix4 mtx) => _uView.Set(mtx);
		
		public void SetProjection(Matrix4 mtx) => _uProjection.Set(mtx);
	}
}