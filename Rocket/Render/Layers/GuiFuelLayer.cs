using System;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.Engine.Text;
using Rocket.Render.Gui;
using Rocket.World.Objects;

namespace Rocket.Render.Layers {
	internal class GuiFuelLayer : GuiLayer {
		private readonly ProgressBar _fuelPbr;
		private readonly RocketObject _rkt;

		public GuiFuelLayer(RocketObject rkt, Window w, GlProgram pgm, IVertexCoder<GuiVertex> coder, Font fnt) : base(w, pgm, coder, fnt) {
			_rkt = rkt ?? throw new ArgumentNullException(nameof(rkt));
			_fuelPbr = new ProgressBar();
			Add(_fuelPbr);
		}

		public override void Resize(int w, int h) {
			_fuelPbr.X = 8;
			_fuelPbr.Width = 10;
			_fuelPbr.Y = 8;
			_fuelPbr.Height = h - 16;
			base.Resize(w, h);
		}

		public override void Render() {
			_fuelPbr.MaxValue = _rkt.MaxFuel;
			_fuelPbr.Value = _rkt.Fuel;
			base.Render();
		}
	}
}