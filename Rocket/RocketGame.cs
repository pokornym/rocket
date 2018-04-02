using System;
using System.Diagnostics;
using System.IO;
using OpenTK;
using OpenTK.Input;
using Rocket.Engine;
using Rocket.Engine.Features;
using Rocket.Engine.OpenGL;
using Rocket.Models;
using Rocket.Render;
using Rocket.Render.Layers;
using Rocket.World;
using Rocket.World.Objects;

namespace Rocket {
	internal sealed class RocketGame : Window {
		private const int TARGET_FPS = 30;
		private const int TARGET_UPS = 30;
		private const int PLANET_TRIANGLES = 16;
		private const string SHADERS_DIR = "shaders";

		private readonly VertexCoder _coder = new VertexCoder();
		private readonly Universe _universe = new Universe();
		private SceneLayer _layer;
		private Model[] _planets;
		private Model[] _atmospheres;
		private int _tick = Environment.TickCount;
		private RocketObject _rocket;

		public RocketGame(string[] args) {
			Attach(new BlendingFeature());
			Title = "Rocket";
		}

		public void Start() {
			Start(TARGET_FPS, TARGET_UPS);
		}

		protected override void OnInitialize() {
			_planets = new Model[] {
				new CircleModel(_coder, PLANET_TRIANGLES, Color.Green),
				new CircleModel(_coder, PLANET_TRIANGLES, Color.Red)
			};
			_atmospheres = new Model[] {
				new CircleModel(_coder, PLANET_TRIANGLES, new Color(0, 165, 255, byte.MaxValue / 2)),
			};

			GlProgram program = new GlProgram(
				new Shader(ShaderTypes.Vertex, File.ReadAllText(Path.Combine(SHADERS_DIR, "vertex.glsl"))),
				new Shader(ShaderTypes.Fragment, File.ReadAllText(Path.Combine(SHADERS_DIR, "fragment.glsl")))
			);

			_universe.Add(new SpaceObject(2, 100000, _planets[0], _atmospheres[0]) { Position = new Vector2(0, 0) });
			_universe.Add(new SpaceObject(8, 50, _planets[1], _atmospheres[0]) { Position = new Vector2(-500, -500), Velocity = new Vector2(30f, 0) });
			_universe.Add(new SpaceObject(0, 250, _planets[0], _atmospheres[0]) { Position = new Vector2(500, -500), Velocity = new Vector2(30f, 0) });
			_universe.Add(_rocket = new RocketObject(30, 50, 1000, new RocketModel(_coder)) { Position = new Vector2(500, 500), Velocity = new Vector2(-30f, 0f) });

			RenderHandle handle = new RenderHandle(program);
			_layer = new SceneLayer(_universe, handle);
			AddLayer(_layer);
			AddLayer(new FuelLayer(_rocket, handle, new RectangleModel(_coder, Color.Red)));
			
			base.OnInitialize();
		}

		protected override void OnUpdate() {
			if (IsKey(Key.A))
				_rocket.MoveCCW(IsKey(Key.W) ? 2f : 1f);
			else if (IsKey(Key.D))
				_rocket.MoveCW(IsKey(Key.W) ? 2f : 1f);
			else if (IsKey(Key.W))
				_rocket.MoveForward(1f);
			else
				_rocket.StopMovement();

			if (IsKey(Key.KeypadPlus))
				_universe.TimeWrap++;
			else if (IsKey(Key.KeypadMinus))
				_universe.TimeWrap--;

			_universe.Tick();

			_layer.Camera.Position = _rocket.Position;

			if (_tick + 1000 <= Environment.TickCount) {
				Debug.WriteLine($"{FPS:F2} FPS, {UPS:F2} UPS");
				_tick = Environment.TickCount;
			}
			
			base.OnUpdate();
		}
		
		protected override void OnWheel(float delta) {
			_layer.Camera.Zoom = Math.Max(Math.Min(_layer.Camera.Zoom + delta * 0.1f, 3f), 0.2f);
			base.OnWheel(delta);
		}
	}
}