using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	internal sealed class Texture : BoundElement {
		protected override int? BoundId {
			get => _boundId;
			set => _boundId = value;
		}
		private static int? _boundId;

		public Texture(IPixelReader pixels, TextureWrapModes wrap, TextureFilterModes filter, TextureMipmapModes mipmap) {
			Create();
			Bind();

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) wrap);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) wrap);
			if (mipmap == TextureMipmapModes.Disabled) {
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) filter);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 0);
			} else {
				if (filter == TextureFilterModes.Linear)
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, mipmap == TextureMipmapModes.Linear ? (int) TextureMinFilter.LinearMipmapLinear : (int) TextureMinFilter.LinearMipmapNearest);
				else
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, mipmap == TextureMipmapModes.Linear ? (int) TextureMinFilter.NearestMipmapLinear : (int) TextureMinFilter.NearestMipmapNearest);
			}

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) filter);

			int r = pixels.Width * pixels.Height;
			byte[] buffer = new byte[r * 4];

			pixels.Rewind();
			for (int i = 0; i < r; i++) {
				Pixel p = pixels.Read();
				int offset = (p.X + p.Y * pixels.Width) * 4;
				buffer[offset] = p.R;
				buffer[offset + 1] = p.G;
				buffer[offset + 2] = p.B;
				buffer[offset + 3] = p.A;
			}

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, pixels.Width, pixels.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, buffer);

			if (mipmap != TextureMipmapModes.Disabled)
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			Unbind();
		}

		protected override int CreateElement() {
			return GL.GenTexture();
		}

		protected override void DeleteElement() {
			GL.DeleteTexture(Id.Value);
		}

		protected override void BindElement() {
			GL.BindTexture(TextureTarget.Texture2D, Id.Value);
		}

		protected override void UnbindElement() {
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
	}
}