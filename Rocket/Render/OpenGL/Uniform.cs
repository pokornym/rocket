using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Render.OpenGL {
	internal class Uniform {
		private readonly int _location;
		public readonly string Name;
		public readonly ShaderElementType Type;

		internal Uniform(string nm, ShaderElementType t, int loc) {
			if (string.IsNullOrWhiteSpace(nm))
				throw new ArgumentNullException(nameof(nm));
			Name = nm;
			_location = loc;
			Type = t ?? throw new ArgumentNullException(nameof(t));
		}

		public void Set(int a) {
			CheckType(PrimitiveTypes.Int, 1);
			GL.Uniform1(_location, a);
		}

		public void Set(int a, int b) {
			CheckType(PrimitiveTypes.Int, 2);
			GL.Uniform2(_location, a, b);
		}

		public void Set(int a, int b, int c) {
			CheckType(PrimitiveTypes.Int, 3);
			GL.Uniform3(_location, a, b, c);
		}

		public void Set(int a, int b, int c, int d) {
			CheckType(PrimitiveTypes.Int, 4);
			GL.Uniform4(_location, a, b, c, d);
		}

		public void Set(uint a) {
			CheckType(PrimitiveTypes.UnsignedInt, 1);
			GL.Uniform1(_location, a);
		}

		public void Set(uint a, uint b) {
			CheckType(PrimitiveTypes.UnsignedInt, 2);
			GL.Uniform2(_location, a, b);
		}

		public void Set(uint a, uint b, uint c) {
			CheckType(PrimitiveTypes.UnsignedInt, 3);
			GL.Uniform3(_location, a, b, c);
		}

		public void Set(uint a, uint b, uint c, uint d) {
			CheckType(PrimitiveTypes.UnsignedInt, 4);
			GL.Uniform4(_location, a, b, c, d);
		}

		public void Set(float a) {
			CheckType(PrimitiveTypes.Float, 1);
			GL.Uniform1(_location, a);
		}

		public void Set(float a, float b) {
			CheckType(PrimitiveTypes.Float, 2);
			GL.Uniform2(_location, a, b);
		}

		internal void Set(Vector2 vec) {
			CheckType(PrimitiveTypes.Float, 2);
			GL.Uniform2(_location, vec);
		}

		public void Set(float a, float b, float c) {
			CheckType(PrimitiveTypes.Float, 3);
			GL.Uniform3(_location, a, b, c);
		}

		internal void Set(Vector3 vec) {
			CheckType(PrimitiveTypes.Float, 3);
			GL.Uniform3(_location, vec);
		}

		public void Set(float a, float b, float c, float d) {
			CheckType(PrimitiveTypes.Float, 4);
			GL.Uniform4(_location, a, b, c, d);
		}

		internal void Set(Vector4 vec) {
			CheckType(PrimitiveTypes.Float, 4);
			GL.Uniform4(_location, vec);
		}

		public void Set(double a) {
			CheckType(PrimitiveTypes.Double, 1);
			GL.Uniform1(_location, a);
		}

		public void Set(double a, double b) {
			CheckType(PrimitiveTypes.Double, 2);
			GL.Uniform2(_location, a, b);
		}

		public void Set(double a, double b, double c) {
			CheckType(PrimitiveTypes.Double, 3);
			GL.Uniform3(_location, a, b, c);
		}

		public void Set(double a, double b, double c, double d) {
			CheckType(PrimitiveTypes.Double, 4);
			GL.Uniform4(_location, a, b, c, d);
		}

		internal void Set(Matrix2 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 2);
			GL.UniformMatrix2(_location, trans, ref mat);
		}

		internal void Set(Matrix3 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 3);
			GL.UniformMatrix3(_location, trans, ref mat);
		}

		internal void Set(Matrix4 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 4);
			GL.UniformMatrix4(_location, trans, ref mat);
		}

		public void Set(TextureUnit unit) {
			CheckType(PrimitiveTypes.Sampler, 2);
			GL.Uniform1(_location, unit.Slot);
		}

		public override string ToString() {
			return $"{Type} {Name}";
		}

		private void CheckType(PrimitiveTypes type, int dim) {
			if (Type.Type != type || Type.Dimension != dim)
				throw new TypeMismatchException(Type, new ShaderElementType(type, dim));
		}
	}
}