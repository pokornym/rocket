namespace Rocket.Engine.OpenGL {
	public interface IPixelReader {
		int Width { get; }
		int Height { get; }

		void Rewind();
		Pixel Read();
	}
}