using System;

namespace Rocket.Render.OpenGL {
	internal sealed class BindingContext : IDisposable {
		public readonly IBindable Element;

		public BindingContext(IBindable bind) {
			Element = bind ?? throw new ArgumentNullException(nameof(bind));
			Element.Bind();
		}

		public void Dispose() {
			Element.Unbind();
		}
	}
}