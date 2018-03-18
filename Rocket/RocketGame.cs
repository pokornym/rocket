﻿using System;
using System.Diagnostics;
using System.IO;
using OpenTK;
using OpenTK.Input;
using Rocket.Models;
using Rocket.Render;
using Rocket.Render.Features;
using Rocket.Render.Layers;
using Rocket.Render.OpenGL;
using Rocket.World;
using Rocket.World.Objects;

namespace Rocket {
	internal sealed class RocketGame {
		private const int FPS = 30;
		private const int UPS = 30;
		private const int PLANET_TRIANGLES = 8;
		private const string SHADERS_DIR = "shaders";

		private readonly RocketWindow _window;
		private readonly VertexCoder _coder = new VertexCoder();
		private readonly Universe _universe = new Universe();
		private SceneLayer _layer;
		private Model[] _planets;
		private Model[] _atmospheres;
		private int _tick = Environment.TickCount;
		private RocketObject _rocket;

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
			_layer = new SceneLayer(_universe, program, program.GetUniform("uModel"), program.GetUniform("uView"), program.GetUniform("uProjection"));
			_window.Add(_layer);

			_universe.Add(new SpaceObject(15, 20, 100000, _planets[0], _atmospheres[0]) { Position = new Vector2(0, 0) });
			_universe.Add(new SpaceObject(5, 8, 50, _planets[1], _atmospheres[0]) { Position = new Vector2(-500, -500), Velocity = new Vector2(20f, 0) });
			_universe.Add(_rocket = new RocketObject(30, 50, new RocketModel(_coder)) { Position = new Vector2(500, 500), Velocity = new Vector2(-25f, 0f) });
		}

		private void Update() {
			_universe.Tick();

			 if (_window.IsKey(Key.A))
				_rocket.MoveCCW(_window.IsKey(Key.W) ? 2f : 1f);
			else if (_window.IsKey(Key.D))
				_rocket.MoveCW(_window.IsKey(Key.W) ? 2f : 1f);
			else if (_window.IsKey(Key.W))
				_rocket.MoveForward(1f);
			else
				_rocket.StopMovement();

			if (_tick + 1000 <= Environment.TickCount) {
				Debug.WriteLine($"{_window.FPS:F2} FPS, {_window.UPS:F2} UPS");
				_tick = Environment.TickCount;
			}
		}
	}
}