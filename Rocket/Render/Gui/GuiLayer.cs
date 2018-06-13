using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Rocket.Engine;
using Rocket.Engine.Features;
using Rocket.Engine.OpenGL;
using Rocket.Engine.Text;
using TextureUnit = Rocket.Engine.OpenGL.TextureUnit;

namespace Rocket.Render.Gui {
	internal class GuiLayer : ILayer, IFontRenderer {
		public float Width { get; private set; }
		public float Height { get; private set; }
		private readonly List<GuiRenderer> _controls = new List<GuiRenderer>();
		private readonly VertexArray<GuiVertex> _array;
		private readonly IndexBuffer _indices;
		private readonly Font _font;
		private readonly TextureWorkspace _tw = new TextureWorkspace();
		private readonly GlProgram _pgm;
		private readonly Uniform _uColor;
		private readonly Uniform _uTexture;
		private readonly Uniform _uTextureEnabled;
		private readonly Uniform _uTransform;
		private readonly Uniform _uScale;
		private Matrix4 _projection;
		private readonly Window _win;

		public GuiLayer(Window w, GlProgram pgm, IVertexCoder<GuiVertex> coder, Font fnt) {
			_win = w ?? throw new ArgumentNullException(nameof(w));
			_pgm = pgm ?? throw new ArgumentNullException(nameof(pgm));
			_array = new VertexArray<GuiVertex>(coder, 4) {
				[0] = new GuiVertex(new Vector2(0, 0), new Vector2(0, 1)),
				[1] = new GuiVertex(new Vector2(1, 0), new Vector2(1, 1)),
				[2] = new GuiVertex(new Vector2(1, 1), new Vector2(1, 0)),
				[3] = new GuiVertex(new Vector2(0, 1), new Vector2(0, 0))
			};
			_indices = new IndexBuffer(6, _array) {
				[0] = 0,
				[1] = 1,
				[2] = 2,
				[3] = 0,
				[4] = 2,
				[5] = 3,
			};
			_font = fnt ?? throw new ArgumentNullException(nameof(fnt));

			_uColor = pgm.GetUniform("uColor");
			_uColor.EnsureType(new ShaderElementType(PrimitiveTypes.Float, 4));
			_uTexture = pgm.GetUniform("uTexture");
			_uTexture.EnsureType(new ShaderElementType(PrimitiveTypes.Sampler, 2));
			_uTextureEnabled = pgm.GetUniform("uTextureEnabled");
			_uTextureEnabled.EnsureType(new ShaderElementType(PrimitiveTypes.Bool, 1));
			_uTransform = pgm.GetUniform("uTransform");
			_uTransform.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uScale = pgm.GetUniform("uScale");
			_uScale.EnsureType(new ShaderElementType(PrimitiveTypes.Float, 2));
		}

		public void Add(Control ctrl) => _controls.Add(new GuiRenderer(ctrl, this));

		public void Remove(Control ctrl) {
			GuiRenderer ren = _controls.FirstOrDefault(i => i.Control == ctrl);
			if (ren != null)
				_controls.Remove(ren);
		}

		public virtual void Resize(int w, int h) {
			Width = w;
			Height = h;
			_projection = Matrix4.CreateOrthographicOffCenter(0, w, h, 0, -1, 1);
		}

		public virtual void Render() {
			foreach (GuiRenderer ctrl in _controls.Where(i => i.Control.IsVisible))
				ctrl.Render();
		}

		public void Write(string txt, float x, float y, float size, Color cl) {
			_pgm.Use();
			_uColor.Set((float) cl.R / byte.MaxValue, (float) cl.G / byte.MaxValue, (float) cl.B / byte.MaxValue, (float) cl.A / byte.MaxValue);
			_font.Render(this, txt, x, y, size);
		}

		public void Draw(float x, float y, float w, float h, float t, Color cl) {
			_pgm.Use();
			_win.Disable<DepthFeature>();
			_uColor.Set((float) cl.R / byte.MaxValue, (float) cl.G / byte.MaxValue, (float) cl.B / byte.MaxValue, (float) cl.A / byte.MaxValue);
			_uTextureEnabled.Set(false);
			GL.LineWidth(t);
			Transform(x, y, w, h);
			_array.Draw(GeometricPrimitives.LineLoop);
			_win.Enable<DepthFeature>();
		}

		public void Fill(float x, float y, float w, float h, Color cl) {
			_pgm.Use();
			_win.Disable<DepthFeature>();
			_uColor.Set((float) cl.R / byte.MaxValue, (float) cl.G / byte.MaxValue, (float) cl.B / byte.MaxValue, (float) cl.A / byte.MaxValue);
			_uTextureEnabled.Set(false);
			Transform(x, y, w, h);
			_indices.Draw(GeometricPrimitives.Triangles);
			_win.Enable<DepthFeature>();
		}

		public void RenderChar(Texture tex, float x, float y, float w, float h) {
			_pgm.Use();
			_win.Disable<DepthFeature>();
			_uTextureEnabled.Set(true);
			Transform(x, y, w, h);
			using (TextureUnit unit = _tw.Use(tex)) {
				_uTexture.Set(unit);
				_indices.Draw(GeometricPrimitives.Triangles);
			}

			_win.Enable<DepthFeature>();
		}

		private void Transform(float x, float y, float w, float h) {
			_uScale.Set(w, h);
			_uTransform.Set(Matrix4.CreateTranslation(x, y, 0) * _projection);
		}
	}
}