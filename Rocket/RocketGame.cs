using System.IO;
using OpenTK;
using Rocket.Models;
using Rocket.Render;
using Rocket.Render.Features;
using Rocket.Render.Layers;
using Rocket.Render.OpenGL;
using Rocket.Scenery;

namespace Rocket {
	internal sealed class RocketGame {
		private const int FPS = 30;
		private const int UPS = 20;
		private const int PLANET_TRIANGLES = 8;
		private const string SHADERS_DIR = "shaders";

		private readonly RocketWindow _window;
		private readonly VertexCoder _coder = new VertexCoder();
		private readonly Scene _scene = new Scene();
		private SceneLayer _layer;
		private Model[] _planets;

		public RocketGame(string[] args) {
			_window = new RocketWindow();
			_window.Attach(new BlendingFeature());
			_window.Attach(new TexturesFeature());
			_window.Attach(new DepthFeature());
			_window.OnInitialize += (s, e) => Initialize();
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
			_layer = new SceneLayer(_scene, program, program.GetUniform("uModel"), program.GetUniform("uView"), program.GetUniform("uProjection"));
			_window.Add(_layer);

			_scene.Add(new SceneObject(_planets[0], new CircleCollider(1)){Transformation = Matrix4.CreateScale(256)});
		}
	}
}