using System;
using System.Collections.Generic;
using OpenTK;
using Rocket.Engine;
using Rocket.Engine.OpenGL;
using Rocket.World;

namespace Rocket.Render {
	public sealed class RenderHandle {
		public const string UNIFORM_MODEL = "uModel";
		public const string UNIFORM_VIEW = "uView";
		public const string UNIFORM_PROJECTION = "uProjection";
		public const string UNIFORM_LIGHT = "uLight";
		public const string UNIFORM_LIGHT_COUNT = "uLightCount";
		public const string UNIFORM_SHADE = "uShade";
		public const string UNIFORM_MATERIAL = "uMaterial";
		public const uint LIGHT_LIMIT = 8;
		public readonly Window Window;
		public readonly GlProgram Program;
		private readonly Uniform _uModel;
		private readonly Uniform _uView;
		private readonly Uniform _uProjection;
		private readonly Uniform _uShade;
		private readonly Uniform _uLightCount;
		private readonly MaterialUniform _uMaterial;
		private readonly LightUniform[] _uLight = new LightUniform[LIGHT_LIMIT];

		public RenderHandle(Window win, GlProgram program) {
			Window = win ?? throw new ArgumentNullException(nameof(win));
			Program = program;
			_uModel = program.GetUniform(UNIFORM_MODEL);
			_uView = program.GetUniform(UNIFORM_VIEW);
			_uProjection = program.GetUniform(UNIFORM_PROJECTION);
			_uShade = program.GetUniform(UNIFORM_SHADE);
			_uMaterial = new MaterialUniform(program.GetUniform($"{UNIFORM_MATERIAL}.ambient"), program.GetUniform($"{UNIFORM_MATERIAL}.diffusion"), program.GetUniform($"{UNIFORM_MATERIAL}.color"));
			_uLightCount = program.GetUniform(UNIFORM_LIGHT_COUNT);

			for (uint i = 0; i < LIGHT_LIMIT; i++)
				_uLight[i] = new LightUniform(program.GetUniform($"{UNIFORM_LIGHT}[{i}].position"), program.GetUniform($"{UNIFORM_LIGHT}[{i}].color"), program.GetUniform($"{UNIFORM_LIGHT}[{i}].intensity"));

			_uModel.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uView.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uProjection.EnsureType(new ShaderElementType(PrimitiveTypes.Matrix, 4));
			_uShade.EnsureType(new ShaderElementType(PrimitiveTypes.Bool, 1));
		}

		public void RenderModel(ModelHandle m) {
			SetMaterial(m.Material);
			m.Model.Draw();
		}

		public void SetLights(IEnumerable<LightSource> ls) {
			int i = 0;
			foreach (LightSource l in ls) {
				if (i >= LIGHT_LIMIT)
					throw new OutOfMemoryException("Too many lights!");
				_uLight[i++].Set(l);
			}

			_uLightCount.Set(i);
		}

		public void SetMaterial(Material mat) => _uMaterial.Set(mat);

		public void SetShade(bool b) => _uShade.Set(b);

		public void SetModel(Matrix4 mtx) => _uModel.Set(mtx);

		public void SetView(Matrix4 mtx) => _uView.Set(mtx);

		public void SetProjection(Matrix4 mtx) => _uProjection.Set(mtx);

		private sealed class LightUniform {
			private static readonly ShaderElementType Float = new ShaderElementType(PrimitiveTypes.Float, 1);
			private static readonly ShaderElementType Vec4 = new ShaderElementType(PrimitiveTypes.Float, 4);
			private static readonly ShaderElementType Vec3 = new ShaderElementType(PrimitiveTypes.Float, 3);
			private readonly Uniform _position;
			private readonly Uniform _color;
			private readonly Uniform _intensity;

			public LightUniform(Uniform pos, Uniform col, Uniform its) {
				_position = pos ?? throw new ArgumentNullException(nameof(pos));
				_color = col ?? throw new ArgumentNullException(nameof(col));
				_intensity = its ?? throw new ArgumentNullException(nameof(its));

				_position.EnsureType(Vec4);
				_color.EnsureType(Vec3);
				_intensity.EnsureType(Float);
			}

			public void Set(LightSource l) {
				_position.Set(l.Position.X, l.Position.Y, l.Position.Z, 1f);
				_color.Set(l.Light.Color);
				_intensity.Set(l.Light.Intensity);
			}
		}

		private sealed class MaterialUniform {
			private static readonly ShaderElementType Float = new ShaderElementType(PrimitiveTypes.Float, 1);
			private static readonly ShaderElementType Vec4 = new ShaderElementType(PrimitiveTypes.Float, 4);
			private readonly Uniform _ambient;
			private readonly Uniform _diffusion;
			private readonly Uniform _color;

			public MaterialUniform(Uniform amb, Uniform diff, Uniform col) {
				_ambient = amb ?? throw new ArgumentNullException(nameof(amb));
				_diffusion = diff ?? throw new ArgumentNullException(nameof(diff));
				_color = col ?? throw new ArgumentNullException(nameof(col));

				_ambient.EnsureType(Float);
				_diffusion.EnsureType(Float);
				_color.EnsureType(Vec4);
			}

			public void Set(Material mat) {
				_ambient.Set(mat.Ambient);
				_diffusion.Set(mat.Diffusion);
				_color.Set(mat.Color);
			}
		}
	}
}