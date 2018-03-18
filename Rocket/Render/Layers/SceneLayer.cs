using System;
using OpenTK;
using Rocket.Render.OpenGL;
using Rocket.World;

namespace Rocket.Render.Layers {
	internal sealed class SceneLayer : ILayer {
		public readonly Universe Universe;
		public readonly Camera Camera = new Camera();
		private Matrix4 _projection;
		private readonly RenderHandle _ren;
		public SceneLayer(Universe s, RenderHandle ren) {
			Universe = s ?? throw new ArgumentNullException(nameof(s));
			_ren = ren ?? throw new ArgumentNullException(nameof(ren));
		}

		public void Resize(int w, int h) => _projection = Matrix4.CreateOrthographic(w, h, -100, 100);

		public void Render() {
			_ren.Program.Bind();

			_ren.SetProjection(_projection);
			_ren.SetView(Camera.Matrix);

			foreach (WorldObject obj in Universe) {
				int l = 0;
				foreach (ModelHandle h in obj.Handles) {
					_ren.SetModel((h.Transformation + obj.Transformation) * Matrix4.CreateTranslation(0, 0, -0.01f * l++));

					h.Draw();
				}
			}

			_ren.Program.Unbind();
		}
	}
}