using System;
using OpenTK;

namespace Rocket.Engine {
	public sealed class Transformation {
		public Vector3 Position {
			get => _pos;
			set {
				_pos = value;
				ComputeMatrix();
			}
		}
		public Vector3 Rotation {
			get => _rotation;
			set {
				_rotation = new Vector3(value.X % ((float) Math.PI * 2), value.Y % ((float) Math.PI * 2), value.Z % ((float) Math.PI * 2));
				ComputeMatrix();
			}
		}
		public Vector3 Scale {
			get => _scale;
			set {
				_scale = value;
				ComputeMatrix();
			}
		}
		public Matrix4 Matrix { get; private set; }
		private Vector3 _pos = Vector3.Zero;
		private Vector3 _rotation = Vector3.Zero;
		private Vector3 _scale = Vector3.One;

		public Transformation() {
			ComputeMatrix();
		}

		private void ComputeMatrix() {
			Matrix =
				Matrix4.CreateScale(_scale.X, _scale.Y, 0) *
				Matrix4.CreateRotationX(_rotation.X) *
				Matrix4.CreateRotationY(_rotation.Y) *
				Matrix4.CreateRotationZ(_rotation.Z) *
				Matrix4.CreateTranslation(_pos.X, _pos.Y, 0);
		}

		public static Matrix4 operator +(Transformation a, Transformation b) {
			Vector3 scale = a._scale * b._scale;
			return
				Matrix4.CreateScale(scale.X, scale.Y, scale.Z) *
				Matrix4.CreateRotationX(a._rotation.X + b._rotation.X) *
				Matrix4.CreateRotationY(a._rotation.Y + b._rotation.Y) *
				Matrix4.CreateRotationZ(a._rotation.Z + b._rotation.Z) *
				Matrix4.CreateTranslation(a._pos.X + b._pos.X, a._pos.Y + b._pos.Y, a._pos.Z + b._pos.Z);
		}
	}
}