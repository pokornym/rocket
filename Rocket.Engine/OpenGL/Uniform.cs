using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public class Uniform {
		private const byte TRUE = 1;
		private const byte FALSE = 0;

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

		public void Set(Vector2 vec) {
			CheckType(PrimitiveTypes.Float, 2);
			GL.Uniform2(_location, vec);
		}

		public void Set(float a, float b, float c) {
			CheckType(PrimitiveTypes.Float, 3);
			GL.Uniform3(_location, a, b, c);
		}

		public void Set(Vector3 vec) {
			CheckType(PrimitiveTypes.Float, 3);
			GL.Uniform3(_location, vec);
		}

		public void Set(float a, float b, float c, float d) {
			CheckType(PrimitiveTypes.Float, 4);
			GL.Uniform4(_location, a, b, c, d);
		}

		public void Set(Vector4 vec) {
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

		public void Set(bool a) {
			CheckType(PrimitiveTypes.Bool, 1);
			GL.Uniform1(_location, a ? TRUE : FALSE);
		}

		public void Set(bool a, bool b) {
			CheckType(PrimitiveTypes.Bool, 2);
			GL.Uniform2(_location, a ? TRUE : FALSE, b ? TRUE : FALSE);
		}

		public void Set(bool a, bool b, bool c) {
			CheckType(PrimitiveTypes.Bool, 3);
			GL.Uniform3(_location, a ? TRUE : FALSE, b ? TRUE : FALSE, c ? TRUE : FALSE);
		}

		public void Set(bool a, bool b, bool c, bool d) {
			CheckType(PrimitiveTypes.Bool, 4);
			GL.Uniform4(_location, a ? TRUE : FALSE, b ? TRUE : FALSE, c ? TRUE : FALSE, d ? TRUE : FALSE);
		}

		public void Set(Matrix2 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 2);
			GL.UniformMatrix2(_location, trans, ref mat);
		}

		public void Set(Matrix3 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 3);
			GL.UniformMatrix3(_location, trans, ref mat);
		}

		public void Set(Matrix4 mat, bool trans = false) {
			CheckType(PrimitiveTypes.Matrix, 4);
			GL.UniformMatrix4(_location, trans, ref mat);
		}

		public void Set(TextureUnit unit) {
			CheckType(PrimitiveTypes.Sampler, 2);
			GL.Uniform1(_location, unit.Slot);
		}

		public void EnsureType(ShaderElementType expect) {
			if (Type != expect)
				throw new TypeMismatchException(expect, Type);
		}

		public override string ToString() {
			return $"{Name} : {Type}";
		}

		private void CheckType(PrimitiveTypes type, int dim) {
			EnsureType(new ShaderElementType(type, dim));
		}
	}
}