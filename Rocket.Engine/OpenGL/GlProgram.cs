using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public class GlProgram : BoundElement {
		private static int? _boundId;
		private readonly Shader[] _shaders;
		protected override int? BoundId {
			get => _boundId;
			set => _boundId = value;
		}
		public readonly ShaderAttribute[] Attributes;
		public readonly Uniform[] Uniforms;

		public GlProgram(params Shader[] shaders) {
			if (shaders == null)
				throw new ArgumentNullException(nameof(shaders));
			foreach (Shader sh in shaders)
				if (shaders.Any(i => i != sh && i.Type == sh.Type))
					throw new ArgumentException($"Every shader type can be present only once! <type: {sh.Type}>", nameof(shaders));
			_shaders = shaders;

			Create();

			GL.GetProgram(Id.Value, GetProgramParameterName.ActiveUniforms, out int uniforms);
			Uniforms = new Uniform[uniforms];
			for (int i = 0; i < uniforms; i++) {
				string name = GL.GetActiveUniform(Id.Value, i, out int size, out ActiveUniformType type);
				Uniforms[i] = new Uniform(name, ShaderElementType.FromGl(type), GL.GetUniformLocation(Id.Value, name));
			}

			GL.GetProgram(Id.Value, GetProgramParameterName.ActiveAttributes, out int attribs);
			Attributes = new ShaderAttribute[attribs];
			for (int i = 0; i < attribs; i++) {
				string name = GL.GetActiveAttrib(Id.Value, i, out int size, out ActiveAttribType type);
				Attributes[i] = new ShaderAttribute(name, ShaderElementType.FromGl(type), GL.GetAttribLocation(Id.Value, name));
			}
		}

		public Uniform GetUniform(string name) {
			return Uniforms.First(i => i.Name == name);
		}

		public ShaderAttribute GetAttribute(string name) {
			return Attributes.First(i => i.Name == name);
		}

		protected override void BindElement() {
			GL.UseProgram(Id.Value);
		}

		protected override void UnbindElement() {
			GL.UseProgram(0);
		}

		protected override int CreateElement() {
			int id = GL.CreateProgram();
			foreach (Shader sh in _shaders)
				GL.AttachShader(id, sh.Id.Value);
			GL.LinkProgram(id);
			GL.GetProgram(id, GetProgramParameterName.LinkStatus, out int link);
			if (link == 0) {
				string lderr = GL.GetProgramInfoLog(id);
				throw new GlProgramException(this, $"Linkage failed!: {lderr}");
			}
			GL.ValidateProgram(id);
			string cerr = GL.GetProgramInfoLog(id);
			if (!string.IsNullOrWhiteSpace(cerr))
				throw new GlProgramException(this, $"Compilation error!: {cerr}");
			return id;
		}

		protected override void DeleteElement() {
			foreach (Shader sh in _shaders) {
				GL.DetachShader(Id.Value, sh.Id.Value);
				sh.Delete();
			}

			GL.DeleteProgram(Id.Value);
		}
	}
}