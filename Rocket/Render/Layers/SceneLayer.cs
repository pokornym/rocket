using System;
using System.Linq;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.Features;
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

		public void Resize(int w, int h) => _projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)w / h, 1, 1000000);

		public void Render() {
			_ren.Program.Bind();

			_ren.SetProjection(_projection);
			_ren.SetView(Camera.Matrix);

			foreach (WorldObject obj in Universe.OrderByDescending(i => (i.Position - Camera.Position).LengthSquared)) {
				_ren.SetShade(true);
				foreach (ModelHandle h in obj.Handles.Where(i => !i.Model.IsTransparent)) {
					_ren.SetModel(h.Transformation + obj.Transformation);

					h.Draw();
				}

//				_ren.SetShade(false);
				_ren.Window.Disable<DepthFeature>();
				foreach (ModelHandle h in obj.Handles.Where(i => i.Model.IsTransparent)) {
					_ren.SetModel(h.Transformation + obj.Transformation);

					h.Draw();
				}
				_ren.Window.Enable<DepthFeature>();
			}

			_ren.Program.Unbind();
		}
	}
}