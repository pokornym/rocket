using System;

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
			UnbindElement();
			BoundId = null;
		}
		
		public BindingContext Use() => new BindingContext(this);

		public void Use(Action f) {
			bool b = BoundId == Id;
			if (!b)
				Bind();
			f();
			if (!b)
				Unbind();
		}

		protected abstract void BindElement();

		protected abstract void UnbindElement();
	}
}