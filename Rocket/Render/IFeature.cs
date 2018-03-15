namespace Rocket.Render {
	internal interface IFeature {
		void Attach();
		void Detach();
		void Before();
		void After();
	}
}