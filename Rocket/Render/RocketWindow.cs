using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
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
		public float FPS => (float) _window.RenderFrequency;
		public float UPS => (float) _window.UpdateFrequency;
		public event EventHandler OnUpdate;
		public event EventHandler OnInitialize;
		public event EventHandler OnUninitialize;
		public event EventHandler<Key> OnKeyDown;
		public event EventHandler<Key> OnKeyUp;
		public event EventHandler<float> OnWheel;
		private readonly List<Key> _keys = new List<Key>();
		private readonly List<IFeature> _features = new List<IFeature>();
		private readonly List<ILayer> _layers = new List<ILayer>();
		private readonly GameWindow _window;

		public RocketWindow(int w = 1024, int h = 720) {
			_window = new GameWindow(w, h, new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, GraphicsMode.Default.Stencil, 16), "Game");
			_window.VSync = VSyncMode.Adaptive;
			_window.Load += (s, e) => OnInitialize?.Invoke(this, null);
			_window.RenderFrame += (s, e) => {
				Tesselate();
				GlProtection.FailIfError();
			};
			_window.UpdateFrame += (s, e) => OnUpdate?.Invoke(this, null);
			_window.Unload += (s, e) => OnUninitialize?.Invoke(this, null);
			_window.Resize += (s, e) => {
				foreach (ILayer layer in _layers) {
					layer.Resize(_window.Width, _window.Height);
					GL.Viewport(0, 0, _window.Width, _window.Height);
				}
			};
			_window.KeyDown += (s, e) => {
				if (!_keys.Contains(e.Key))
					_keys.Add(e.Key);
				OnKeyDown?.Invoke(this, e.Key);
			};
			_window.KeyUp += (s, e) => {
				if (_keys.Contains(e.Key))
					_keys.Remove(e.Key);
				OnKeyUp?.Invoke(this, e.Key);
			};
			_window.MouseWheel += (s, e) => OnWheel?.Invoke(this, e.DeltaPrecise);
			_window.FocusedChanged += (s, e) => {
				if (!_window.Focused)
					_keys.Clear();
			};
		}

		public void Attach(IFeature f) {
			_features.Add(f);
			f.Attach();
		}

		public void Detach(IFeature f) {
			_features.Remove(f);
			f.Detach();
		}

		public bool IsKey(Key k) => _keys.Contains(k);

		public void Add(ILayer l) {
			l.Resize(_window.Width, _window.Height);
			_layers.Add(l);
		}

		public void Remove(ILayer l) => _layers.Remove(l);

		public void Start(int fps, int ups) {
			_window.Run(ups, fps);
		}

		private void Tesselate() {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			foreach (IFeature f in _features)
				f.Before();

			foreach (ILayer l in _layers)
				l.Render();

			foreach (IFeature f in _features)
				f.After();

			_window.SwapBuffers();
		}
	}
}