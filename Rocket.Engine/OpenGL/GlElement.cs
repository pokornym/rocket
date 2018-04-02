using System;

namespace Rocket.Engine.OpenGL {
	public abstract class GlElement : IDisposable {
		public int? Id { get; private set; }

		public virtual void Dispose() {
			Delete();
		}

		~GlElement() {
			Dispose();
		}

		protected void Create() {
			GlProtection.Protect(() => Id = CreateElement());
		}

		protected abstract int CreateElement();

		protected abstract void DeleteElement();

		public virtual void Delete() {
			if (Id == null)
				return;
			GlProtection.Protect(DeleteElement);
			Id = null;
		}
	}
}