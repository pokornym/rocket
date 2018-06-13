using Rocket.Engine.OpenGL;

namespace Rocket.Engine.Text {
	public interface IFontRenderer {
		void RenderChar(Texture tex, float x, float y, float w, float h);
	}
}