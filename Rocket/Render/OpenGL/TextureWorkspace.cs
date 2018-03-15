using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal sealed class TextureWorkspace {
		private readonly Texture[] _slots;
		public readonly int Slots;

		public TextureWorkspace() {
			Slots = GL.GetInteger(GetPName.MaxTextureImageUnits);
			_slots = new Texture[Slots];
		}

		public TextureUnit Use(Texture tex) {
			int? slot = null;
			for (int i = 0; i < Slots; i++)
				if (_slots[i] == tex || _slots[i] == null) {
					slot = i;
					break;
				}

			if (slot == null)
				throw new OutOfMemoryException("No free GPU texture unit!");
			_slots[slot.Value] = tex;
			GL.ActiveTexture(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0 + slot.Value);
			tex.Bind();
			return new TextureUnit(this, slot.Value);
		}

		internal void Release(int slot) {
			if (_slots[slot] == null)
				throw new ArgumentException("Slot is not allocated!", nameof(slot));
			GL.ActiveTexture(OpenTK.Graphics.OpenGL4.TextureUnit.Texture0 + slot);
			_slots[slot].Unbind();
			_slots[slot] = null;
		}
	}
}