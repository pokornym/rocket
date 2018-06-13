using OpenTK;

namespace Rocket.Render.Gui {
	internal class ProgressBar : Control {
		public enum Orientations {
			Vertical,
			Horizontal
		}

		public float Value = 0;
		public float MaxValue = 100;
		public Orientations Orientation = Orientations.Vertical;
		public Color Color = Color.Red;

		public override void Render(GuiRenderer gui) {
			if (Orientation == Orientations.Vertical) {
				float fill = Height * (Value / MaxValue);
				gui.Fill(0, (Height - fill) / 2, Width, fill, Color);
			} else {
				float fill = Width * (Value / MaxValue);
				gui.Fill((Width - fill) / 2, 0, fill, Height, Color);
			}
		}
	}
}