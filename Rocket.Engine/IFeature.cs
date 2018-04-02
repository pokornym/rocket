namespace Rocket.Engine {
	public interface IFeature {
		void Attach();
		void Detach();
		void Before();
		void After();
	}
}