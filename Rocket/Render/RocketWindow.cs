using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Rocket.Render.OpenGL;

namespace Rocket.Render {
	internal sealed class RocketWindow {
		public int Width {
			get => _window.Width;
			set => _window.Width = value;
		}
		public int Height {
			get => _window.Height;
			set => _window.Height = value;
		}
		public string Title {
			get => _window.Title;
			set => _window.Title = value;
		}
		public event EventHandler OnUpdate;
		private readonly List<IFeature> _features = new List<IFeature>();
		private readonly GameWindow _window;

		public RocketWindow(int w = 1024, int h = 720) {
			_window = new GameWindow(w, h, new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, GraphicsMode.Default.Stencil, 8), "Game");
			_window.Load += (s, e) => Initialize();
			_window.RenderFrame += (s, e) => {
				Tesselate();
				GlProtection.FailIfError();
			};
			_window.UpdateFrame += (s, e) => OnUpdate?.Invoke(this, null);
			_window.Unload += (s, e) => Uninitialize();
		}

		public void Attach(IFeature f) {
			_features.Add(f);
			f.Attach();
		}

		public void Detach(IFeature f) {
			_features.Remove(f);
			f.Detach();
		}

		public void Start(int fps, int ups) {
			_window.Run(ups, fps);
		}

		private void Initialize() { }

		private void Frame() { }

		private void Tesselate() {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			foreach (IFeature f in _features)
				f.Before();

			Frame();

			foreach (IFeature f in _features)
				f.After();

			_window.SwapBuffers();
		}

		private void Uninitialize() { }
	}
}