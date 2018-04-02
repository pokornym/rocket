namespace Rocket.Engine {
	public interface ILayer {
		void Resize(int w, int h);
		void Render();
	}
}