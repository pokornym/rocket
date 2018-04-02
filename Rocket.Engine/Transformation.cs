using System;
using OpenTK;

namespace Rocket.Engine {
	public sealed class Transformation {
		public Vector2 Position {
			get => _pos;
			set {
				_pos = value;
				ComputeMatrix();
			}
		}
		public float Angle {
			get => _angle;
			set {
				_angle = value % ((float) Math.PI * 2);
				ComputeMatrix();
			}
		}
		public Vector2 Scale {
			get => _scale;
			set {
				_scale = value;
				ComputeMatrix();
			}
		}
		public Matrix4 Matrix { get; private set; }
		private Vector2 _pos = Vector2.Zero;
		private float _angle = 0;
		private Vector2 _scale = Vector2.One;

		public Transformation() {
			ComputeMatrix();
		}

		private void ComputeMatrix() {
			Matrix =
				Matrix4.CreateScale(_scale.X, _scale.Y, 0) *
				Matrix4.CreateRotationZ(_angle) *
				Matrix4.CreateTranslation(_pos.X, _pos.Y, 0);
		}

		public static Matrix4 operator +(Transformation a, Transformation b) {
			Vector2 scale = a._scale * b._scale;
			return
				Matrix4.CreateScale(scale.X, scale.Y, 0) *
				Matrix4.CreateRotationZ(a._angle + b._angle) *
				Matrix4.CreateTranslation(a._pos.X + b._pos.X, a._pos.Y + b._pos.Y, 0);
		}
	}
}