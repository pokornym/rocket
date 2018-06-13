using OpenTK;

namespace Rocket.Render.Gui {
	internal sealed class Label : Control {
		public string Text = "Label";
		public float Size = 24;
		public Color Color = Color.White;

		public override void Render(GuiRenderer gui) {
			gui.Write(Text, 0, 0, Size, Color);
		}
	}
}