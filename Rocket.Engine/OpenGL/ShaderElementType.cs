using System;
using OpenTK.Graphics.OpenGL4;

namespace Rocket.Engine.OpenGL {
	public sealed class ShaderElementType : IEquatable<ShaderElementType> {
		public readonly int Dimension;
		public readonly PrimitiveTypes Type;

		public ShaderElementType(PrimitiveTypes t, int d) {
			Type = t;
			Dimension = d;
		}

		public override string ToString() {
			return $"{Type:G}[{Dimension}]";
		}

		public bool Equals(ShaderElementType other) {
			if (ReferenceEquals(other, null))
				return false;
			return Type == other.Type && Dimension == other.Dimension;
		}

		public static bool operator ==(ShaderElementType left, ShaderElementType right) {
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return true;
			return ReferenceEquals(left, null) ? right.Equals(left) : left.Equals(right);
		}

		public static bool operator !=(ShaderElementType left, ShaderElementType right) {
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return false;
			return !(ReferenceEquals(left, null) ? right.Equals(left) : left.Equals(right));
		}

		internal static ShaderElementType FromGl(ActiveAttribType t) {
			return FromGl((ActiveUniformType) t);
		}

		internal static ShaderElementType FromGl(ActiveUniformType t) {
			if (ScalarVertex(PrimitiveTypes.Int, ActiveUniformType.Int, ActiveUniformType.IntVec2, ActiveUniformType.IntVec4, 1, out ShaderElementType tInt))
				return tInt;

			if (ScalarVertex(PrimitiveTypes.UnsignedInt, ActiveUniformType.UnsignedInt, ActiveUniformType.UnsignedIntVec2, ActiveUniformType.UnsignedIntVec4, 1, out ShaderElementType tUint))
				return tUint;

			if (ScalarVertex(PrimitiveTypes.Float, ActiveUniformType.Float, ActiveUniformType.FloatVec2, ActiveUniformType.FloatVec4, 1, out ShaderElementType tFloat))
				return tFloat;

			if (ScalarVertex(PrimitiveTypes.Double, ActiveUniformType.Double, ActiveUniformType.DoubleVec2, ActiveUniformType.DoubleVec4, 1, out ShaderElementType tDouble))
				return tDouble;

			if (ScalarVertex(PrimitiveTypes.Bool, ActiveUniformType.Bool, ActiveUniformType.BoolVec2, ActiveUniformType.BoolVec4, 1, out ShaderElementType tBool))
				return tBool;

			if (Vertex(PrimitiveTypes.Matrix, ActiveUniformType.FloatMat2, ActiveUniformType.FloatMat4, 2, out ShaderElementType tMatrtix))
				return tMatrtix;

			if (Vertex(PrimitiveTypes.Sampler, ActiveUniformType.Sampler1D, ActiveUniformType.Sampler3D, 1, out ShaderElementType tSampler))
				return tSampler;

			if (Vertex(PrimitiveTypes.Image, ActiveUniformType.Image1D, ActiveUniformType.Image3D, 1, out ShaderElementType tImage))
				return tImage;

			throw new NotSupportedException($"Type {t:G} is not supported!");

			bool ScalarVertex(PrimitiveTypes pt, ActiveUniformType b, ActiveUniformType bottom, ActiveUniformType top, int start, out ShaderElementType set) {
				if (t == b) {
					set = new ShaderElementType(pt, start);
					return true;
				}

				if (t >= bottom && t <= top) {
					set = new ShaderElementType(pt, t - bottom + start + 1);
					return true;
				}

				set = null;
				return false;
			}

			bool Vertex(PrimitiveTypes pt, ActiveUniformType bottom, ActiveUniformType top, int start, out ShaderElementType set) {
				if (t >= bottom && t <= top) {
					set = new ShaderElementType(pt, t - bottom + start);
					return true;
				}

				set = null;
				return false;
			}


		}
	}
}