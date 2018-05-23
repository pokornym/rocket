using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Rocket.Engine.OpenGL;

namespace Rocket.Engine {
	public class Window {
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
		public event EventHandler Initialize;
		public event EventHandler Frame;
		public event EventHandler Update;
		public event EventHandler Uninitialize;
		public event EventHandler<Key> KeyDown;
		public event EventHandler<Key> KeyUp;
		public event EventHandler<float> Wheel;
		protected Color Background = Color.Black;
		private readonly List<Key> _keys = new List<Key>();
		private readonly List<FeatureHandle> _features = new List<FeatureHandle>();
		private readonly List<ILayer> _layers = new List<ILayer>();
		private readonly GameWindow _window;

		public Window(int w = 1024, int h = 720) {
			_window = new GameWindow(w, h, new GraphicsMode(GraphicsMode.Default.ColorFormat, GraphicsMode.Default.Depth, GraphicsMode.Default.Stencil, 16), "Game");
			_window.VSync = VSyncMode.Adaptive;
			_window.Load += (s, e) => OnInitialize();
			_window.RenderFrame += (s, e) => {
				OnFrame();
				Tesselate();
				GlProtection.FailIfError();
			};
			_window.UpdateFrame += (s, e) => OnUpdate();
			_window.Unload += (s, e) => OnUnitialize();
			_window.Resize += (s, e) => {
				foreach (ILayer layer in _layers) {
					layer.Resize(_window.Width, _window.Height);
					GL.Viewport(0, 0, _window.Width, _window.Height);
				}
			};
			_window.KeyDown += (s, e) => {
				if (!_keys.Contains(e.Key))
					_keys.Add(e.Key);
				OnKeyDown(e.Key);
			};
			_window.KeyUp += (s, e) => {
				if (_keys.Contains(e.Key))
					_keys.Remove(e.Key);
				OnKeyUp(e.Key);
			};
			_window.MouseWheel += (s, e) => OnWheel(e.DeltaPrecise);
			_window.FocusedChanged += (s, e) => {
				if (_window.Focused)
					return;

				foreach (Key k in _keys)
					OnKeyUp(k);
				_keys.Clear();
			};
		}

		public void Attach(IFeature f) {
			FeatureHandle h = new FeatureHandle(f);
			_features.Add(h);
			h.Attach();
		}

		public void Enable<T>() where T : IFeature {
			foreach (FeatureHandle h in _features.Where(i => i.Feature is T && !i.Enabled))
				h.Attach();
		}

		public void Detach(IFeature f) {
			FeatureHandle h = _features.FirstOrDefault(i => i.Feature == f);
			if (h == null)
				return;
			_features.Remove(h);
			h.Detach();
		}

		public void Disable<T>() where T : IFeature {
			foreach (FeatureHandle h in _features.Where(i => i.Feature is T && i.Enabled))
				h.Detach();
		}
		
		public bool IsKey(Key k) => _keys.Contains(k);

		public void AddLayer(ILayer l) {
			l.Resize(_window.Width, _window.Height);
			_layers.Add(l);
		}

		public void RemoveLayer(ILayer l) => _layers.Remove(l);

		public void Start(int fps, int ups) {
			_window.Run(ups, fps);
		}

		protected virtual void OnInitialize() => Initialize?.Invoke(this, null);

		protected virtual void OnFrame() => Frame?.Invoke(this, null);
		
		protected virtual void OnUpdate() => Update?.Invoke(this, null);

		protected virtual void OnUnitialize() => Uninitialize?.Invoke(this, null);

		protected virtual void OnKeyDown(Key k) => KeyDown?.Invoke(this, k);

		protected virtual void OnKeyUp(Key k) => KeyUp?.Invoke(this, k);

		protected virtual void OnWheel(float delta) => Wheel?.Invoke(this, delta);

		private void Tesselate() {
			GL.ClearColor(Background);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			foreach (FeatureHandle f in _features)
				f.Before();

			foreach (ILayer l in _layers)
				l.Render();

			foreach (FeatureHandle f in _features)
				f.After();

			_window.SwapBuffers();
		}

		private sealed class FeatureHandle : IFeature{
			public readonly IFeature Feature;
			public bool Enabled { get; private set; }

			public FeatureHandle(IFeature f) => Feature = f ?? throw new ArgumentNullException(nameof(f));
			
			public void Attach() {
				if (Enabled)
					return;
				Feature.Attach();
				Enabled = true;
			}

			public void Detach() {
				if (!Enabled)
					return;
				Feature.Detach();
				Enabled = false;
			}

			public void Before() {
				if (Enabled)
					Feature.Before();
			}

			public void After() {
				if (Enabled)
					Feature.Before();
			}
		}
	}
}