namespace Rocket.Render.OpenGL {
	internal abstract class BoundElement : GlElement, IBindable {
		protected abstract int? BoundId { get; set; }

		public void Bind() {
			if (BoundId == Id)
				return;
			BindElement();
			BoundId = Id;
		}

		public void Unbind() {
			if (BoundId != Id)
				return;
			Unbind();
			BoundId = null;
		}

		protected abstract void BindElement();

		protected abstract void UnbindElement();
	}
}