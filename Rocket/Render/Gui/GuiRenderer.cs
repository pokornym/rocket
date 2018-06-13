using System;
using OpenTK;

namespace Rocket.Render.Gui {
	internal sealed class GuiRenderer {
		public readonly Control Control;
		private readonly GuiLayer _gui;

		public GuiRenderer(Control ctrl, GuiLayer gui) {
			Control = ctrl ?? throw new ArgumentNullException(nameof(ctrl));
			_gui = gui ?? throw new ArgumentNullException(nameof(gui));
		}

		public void Render() => Control.Render(this);

		public void Write(string txt, float x, float y, float size, Color cl) => _gui.Write(txt, x + Control.X, y + Control.Y, size, cl);

		public void Draw(float x, float y, float w, float h, float t, Color cl) => _gui.Draw(x + Control.X, y + Control.Y, w, h, t, cl);

		public void Fill(float x, float y, float w, float h, Color cl) => _gui.Fill(x + Control.X, y + Control.Y, w, h, cl);
	}
}