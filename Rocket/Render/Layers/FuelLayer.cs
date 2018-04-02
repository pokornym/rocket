using System;
using OpenTK;
using Rocket.Engine;
using Rocket.World.Objects;

namespace Rocket.Render.Layers {
	internal sealed class FuelLayer : ILayer {
		private RocketObject _rocket;
		private Matrix4 _projection;
		private int _w;
		private int _h;
		private readonly RenderHandle _ren;
		private readonly ModelHandle _bar;

		public FuelLayer(RocketObject r, RenderHandle ren, Model m) {
			_rocket = r ?? throw new ArgumentNullException(nameof(r));
			_ren = ren ?? throw new ArgumentNullException(nameof(ren));
			_bar = new ModelHandle(m ?? throw new ArgumentNullException(nameof(m)));
		}

		public void Resize(int w, int h) => _projection = Matrix4.CreateOrthographic(_w = w, _h = h, -100, 100);

		public void Render() {
			_ren.Program.Bind();

			_ren.SetProjection(_projection);

			_bar.Position = new Vector2(-((float) _w / 2 * 15 / 16), 0);
			_bar.Scale = new Vector2(8, (float) _h * 3 / 8 * _rocket.Fuel / _rocket.MaxFuel);

			_ren.SetView(Matrix4.Identity);
			_ren.SetModel(_bar.Transformation.Matrix * Matrix4.CreateTranslation(0, 0, 0.1f));

			_bar.Draw();

			_ren.Program.Unbind();
		}
	}
}