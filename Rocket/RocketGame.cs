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
		private const int TARGET_FPS = 120;
		private const int TARGET_UPS = 30;
		private const int SPHERE_BREAKUPS = 1;
		private const string SHADERS_DIR = "shaders";
		private const float ROTATION_TORQUE = 1f;
		private const float STABILIZATION_TORQUE = 5f;
		private const float BOOSTER_FORCE = 10f;

		private readonly VertexCoder _coder = new VertexCoder();
		private readonly Universe _universe = new Universe();
		private RocketObject _rocket;
		private OrbitalCamera _cam;
		private SceneLayer _layer;

		private Model[] _planets;
		private Model[] _atmospheres;
		private int _tick = Environment.TickCount;

		public RocketGame(string[] args) {
			Attach(new BlendingFeature());
			Attach(new DepthFeature());
			//Attach(new CullFaceFeature());
			Title = "Rocket";
		}

		public void Start() {
			Start(TARGET_FPS, TARGET_UPS);
		}

		protected override void OnInitialize() {
			_planets = new Model[] {
				new SphereModel(_coder, SPHERE_BREAKUPS, Color.Green),
				new SphereModel(_coder, SPHERE_BREAKUPS, Color.Red)
			};
			_atmospheres = new Model[] {
				new SphereModel(_coder, SPHERE_BREAKUPS, new Color(0, 165, 255, byte.MaxValue / 4), true),
			};

			GlProgram program = new GlProgram(
				new Shader(ShaderTypes.Vertex, File.ReadAllText(Path.Combine(SHADERS_DIR, "vertex.glsl"))),
				new Shader(ShaderTypes.Fragment, File.ReadAllText(Path.Combine(SHADERS_DIR, "fragment.glsl")))
			);

			_universe.Add(new SpaceObject(2, 100000, _planets[0], _atmospheres[0]) { Position = new Vector3(0, 0, 0) });
			_universe.Add(new SpaceObject(8, 50, _planets[1], _atmospheres[0]) { Position = new Vector3(-500, -500, 0), Velocity = new Vector3(30f, 0, -5f) });
			_universe.Add(new SpaceObject(0, 250, _planets[0], _atmospheres[0]) { Position = new Vector3(500, -500, 0), Velocity = new Vector3(30f, 0, 5f) });
			_universe.Add(_rocket = new RocketObject(55, _planets[0]) { Position = new Vector3(500, 500, 500), Velocity = new Vector3(-30f, 0f, 0f) });

			RenderHandle handle = new RenderHandle(this, program);
			_layer = new SceneLayer(_universe, handle);
			AddLayer(_layer);
			_cam = new OrbitalCamera(_layer.Camera) { Distance = 250 };
			//AddLayer(new FuelLayer(_rocket, handle, new RectangleModel(_coder, Color.Red)));
			Background = new Color(50, 50, 50, 255);
			base.OnInitialize();
		}

		protected override void OnFrame() {
			_cam.Pivot = _rocket.Position;
			base.OnFrame();
		}

		protected override void OnUpdate() {
			_rocket.Force = Vector3.Zero;
			_rocket.Torque = Vector3.Zero;
			if (IsKey(Key.W))
				_rocket.Torque += Vector3.UnitX * ROTATION_TORQUE;
			if (IsKey(Key.S))
				_rocket.Torque -= Vector3.UnitX * ROTATION_TORQUE;
			if (IsKey(Key.D))
				_rocket.Torque += Vector3.UnitY * ROTATION_TORQUE;
			if (IsKey(Key.A))
				_rocket.Torque -= Vector3.UnitY * ROTATION_TORQUE;
			if (IsKey(Key.E))
				_rocket.Torque += Vector3.UnitZ * ROTATION_TORQUE;
			if (IsKey(Key.Q))
				_rocket.Torque -= Vector3.UnitZ * ROTATION_TORQUE;
			if (IsKey(Key.X) && _rocket.AngularMomentum.Length > 0)
				_rocket.Torque -= _rocket.AngularMomentum.Length > 1 ? _rocket.AngularMomentum.Normalized() * STABILIZATION_TORQUE : _rocket.AngularMomentum * STABILIZATION_TORQUE;
			if (IsKey(Key.Space))
				_rocket.Force = Rotate(Vector3.UnitZ, _rocket.Rotation) * BOOSTER_FORCE;

			if (IsKey(Key.Left))
				_cam.Rotation = new Vector2(_cam.Rotation.X + (float) Math.PI / 180, _cam.Rotation.Y);
			if (IsKey(Key.Right))
				_cam.Rotation = new Vector2(_cam.Rotation.X - (float) Math.PI / 180, _cam.Rotation.Y);
			if (IsKey(Key.Up))
				_cam.Rotation = new Vector2(_cam.Rotation.X, _cam.Rotation.Y + (float) Math.PI / 180);
			if (IsKey(Key.Down))
				_cam.Rotation = new Vector2(_cam.Rotation.X, _cam.Rotation.Y - (float) Math.PI / 180);

			if (IsKey(Key.KeypadPlus))
				_universe.TimeWrap++;
			else if (IsKey(Key.KeypadMinus))
				_universe.TimeWrap--;

			_universe.Tick();

			if (_tick + 1000 <= Environment.TickCount) {
				Debug.WriteLine($"{FPS:F2} FPS, {UPS:F2} UPS");
				_tick = Environment.TickCount;
			}

			base.OnUpdate();
		}

		protected override void OnWheel(float delta) {
			_cam.Distance -= delta * 25;
			base.OnWheel(delta);
		}

		private static Vector3 Rotate(Vector3 vec, Vector3 rot) => RotateX(RotateY(RotateZ(vec, rot.Z), rot.Y), rot.X);

		private static Vector3 RotateX(Vector3 vec, float ang) {
			float sin = (float) Math.Sin(ang);
			float cos = (float) Math.Cos(ang);
			return new Vector3(vec.X, vec.Y * cos + vec.Z * sin, vec.Z * cos - vec.Y * sin);
		}

		private static Vector3 RotateY(Vector3 vec, float ang) {
			float sin = (float) Math.Sin(ang);
			float cos = (float) Math.Cos(ang);
			return new Vector3(vec.X * cos - vec.Z * sin, vec.Y, vec.X * sin + vec.Z * cos);
		}

		private static Vector3 RotateZ(Vector3 vec, float ang) {
			float sin = (float) Math.Sin(ang);
			float cos = (float) Math.Cos(ang);
			return new Vector3(vec.X * cos + vec.Y * sin, vec.Y * cos - vec.X * sin, vec.Z);
		}
	}
}