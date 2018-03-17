using System;
using OpenTK;
using Rocket.Render.OpenGL;
using Rocket.World;

namespace Rocket.Render.Layers {
	internal sealed class SceneLayer : ILayer {
		public readonly Universe Universe;
		public readonly Camera Camera = new Camera();
		private Matrix4 _projection;
		private readonly GlProgram _program;
		private readonly Uniform _uModel;
		private readonly Uniform _uView;
		private readonly Uniform _uProjection;

		public SceneLayer(Universe s,GlProgram prog, Uniform model, Uniform view, Uniform proj) {
			Universe = s ?? throw new ArgumentNullException(nameof(s));
			_program = prog ?? throw new ArgumentNullException(nameof(prog));
			_uModel = model ?? throw new ArgumentNullException(nameof(model));
			_uView = view ?? throw new ArgumentNullException(nameof(view));
			_uProjection = proj ?? throw new ArgumentNullException(nameof(proj));
			if (_uModel.Type.Type != PrimitiveTypes.Matrix || _uModel.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uModel.Type);
			if (_uView.Type.Type != PrimitiveTypes.Matrix || _uView.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uView.Type);
			if (_uProjection.Type.Type != PrimitiveTypes.Matrix || _uProjection.Type.Dimension != 4)
				throw new TypeMismatchException(new ShaderElementType(PrimitiveTypes.Matrix, 4), _uProjection.Type);
		}

		public void Resize(int w, int h) => _projection = Matrix4.CreateOrthographic(w, h, -100, 100);

		public void Render() {
			_program.Bind();
			
			_uProjection.Set(_projection);
			_uView.Set(Camera.View);
			
			foreach (WorldObject obj in Universe) {
				_uModel.Set(obj.Transformation * Matrix4.CreateTranslation(obj.Position.X, obj.Position.Y, 0));
			
				obj.Model.Draw();
			}
			
			_program.Unbind();
		}
	}
}