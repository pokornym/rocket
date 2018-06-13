using OpenTK;

namespace Rocket.Render {
	public struct GuiVertex {
		public Vector2 Position;
		public Vector2 TexCoords;

		public GuiVertex(Vector2 p, Vector2 t) {
			Position = p;
			TexCoords = t;
		}
	}
}