using System;

namespace Rocket.Render.OpenGL {
	internal sealed class TextureUnit : IDisposable {
		private readonly TextureWorkspace _ws;
		internal readonly int Slot;
		public bool IsDisposed { get; private set; }
		
		internal TextureUnit(TextureWorkspace ws, int slot) {
			_ws = ws ?? throw new ArgumentNullException(nameof(ws));
			Slot = slot;
		}

		~TextureUnit() {
			if (!IsDisposed)
				Dispose();
		}

		public void Dispose() {
			if (IsDisposed)
				throw new ObjectDisposedException(GetType().FullName);
			_ws.Release(Slot);
			IsDisposed = true;
		}
	}
}