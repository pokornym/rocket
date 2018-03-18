﻿using OpenTK;
using Rocket.Render.OpenGL;

namespace Rocket.Render {
	internal sealed class RenderHandle {
		public readonly GlProgram Program;
		private readonly Uniform _uModel;
		private readonly Uniform _uView;
		private readonly Uniform _uProjection;

		public RenderHandle(GlProgram program, string mod = "uModel", string view = "uView", string proj = "uProjection") {
			Program = program;
			_uModel = program.GetUniform(mod);
			_uView = program.GetUniform(view);
			_uProjection = program.GetUniform(proj);
			
			if (_uModel.Type.Type != PrimitiveTypes.Matrix || _uModel.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uModel.Type);
			if (_uView.Type.Type != PrimitiveTypes.Matrix || _uView.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uView.Type);
			if (_uProjection.Type.Type != PrimitiveTypes.Matrix || _uProjection.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uProjection.Type);
		}

		public void SetModel(Matrix4 mtx) => _uModel.Set(mtx);
		
		public void SetView(Matrix4 mtx) => _uView.Set(mtx);
		
		public void SetProjection(Matrix4 mtx) => _uProjection.Set(mtx);
	}
}