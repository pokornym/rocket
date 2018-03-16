namespace Rocket.Render {
	public interface ILayer {
		void Resize(int w, int h);
		void Render();
	}
}