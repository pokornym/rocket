namespace Rocket.Engine.OpenGL {
	internal interface IPixelReader {
		int Width { get; }
		int Height { get; }

		void Rewind();
		Pixel Read();
	}
}