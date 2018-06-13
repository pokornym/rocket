namespace Rocket.Render.Gui {
	internal abstract class Control {
		public float X;
		public float Y;
		public float Width;
		public float Height;
		public bool IsVisible = true;

		public abstract void Render(GuiRenderer gui);
	}
}