using System;
using System.Diagnostics;
using System.IO;
using OpenTK;
using Rocket.Models;
using Rocket.Render;
using Rocket.Render.Features;
using Rocket.Render.Layers;
using Rocket.Render.OpenGL;
using Rocket.World;

namespace Rocket {
	internal sealed class RocketGame {
		private const int FPS = 60;
		private const int UPS = 60;
		private const int PLANET_TRIANGLES = 8;
		private const string SHADERS_DIR = "shaders";

		private readonly RocketWindow _window;
		private readonly VertexCoder _coder = new VertexCoder();
		private readonly Universe _universe = new Universe();
		private SceneLayer _layer;
		private Model[] _planets;
		private int _tick = Environment.TickCount;

		public RocketGame(string[] args) {
			_window = new RocketWindow();
			_window.Attach(new BlendingFeature());
			_window.Attach(new TexturesFeature());
			_window.Attach(new DepthFeature());
			_window.OnInitialize += (s, e) => Initialize();
			_window.OnUpdate += (s, e) => Update();
		}

		public void Start() {
			_window.Start(FPS, UPS);
		}

		private void Initialize() {
			_planets = new Model[] {
				new CircleModel(_coder, PLANET_TRIANGLES, Color.Red)
			};

			GlProgram program = new GlProgram(
				new Shader(ShaderTypes.Vertex, File.ReadAllText(Path.Combine(SHADERS_DIR, "vertex.glsl"))),
				new Shader(ShaderTypes.Fragment, File.ReadAllText(Path.Combine(SHADERS_DIR, "fragment.glsl")))
			);
			_layer = new SceneLayer(_universe, program, program.GetUniform("uModel"), program.GetUniform("uView"), program.GetUniform("uProjection"));
			_window.Add(_layer);

			_universe.Add(new WorldObject(_planets[0], new CircleCollider(15)) { Transformation = Matrix4.CreateScale(15), Position = new Vector2(50, 100), Mass = 1000 });
			_universe.Add(new WorldObject(_planets[0], new CircleCollider(5)) { Transformation = Matrix4.CreateScale(5), Position = new Vector2(-50, -250), Mass = 5, Velocity = new Vector2(4f, 0) });
			_universe.Add(new WorldObject(_planets[0], new CircleCollider(15)) { Transformation = Matrix4.CreateScale(15), Position = new Vector2(-100, -200), Mass = 15, Velocity = new Vector2(4f, 0) });
			_universe.Add(new WorldObject(_planets[0], new CircleCollider(30)) { Transformation = Matrix4.CreateScale(30), Position = new Vector2(-150, -100), Mass = 30, Velocity = new Vector2(4f, 0) });
		}

		private void Update() {
			_universe.Tick();
			if (_tick + 1000 <= Environment.TickCount) {
				Debug.WriteLine($"{_window.FPS:F2} FPS, {_window.UPS:F2} UPS");
				_tick = Environment.TickCount;
			}
		}
	}
}