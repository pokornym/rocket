using System;
using OpenTK;

namespace Rocket.Render {
	internal sealed class Transformation {
		public Vector2 Position {
			get => _pos;
			set {
				_pos = value;
				ComputeMatrix();
			}
		}
		public float Angle {
			get => _angle % ((float)Math.PI * 2);
			set {
				_angle = value;
				ComputeMatrix();
			}
		}
		public float Scale {
			get => _scale;
			set {
				_scale = value;
				ComputeMatrix();
			}
		}
		public Matrix4 Matrix { get; private set; }
		private Vector2 _pos = Vector2.Zero;
		private float _angle = 0;
		private float _scale = 1;

		public Transformation() {
			ComputeMatrix();
		}

		private void ComputeMatrix() => Matrix = Matrix4.CreateTranslation(_pos.X, _pos.Y, 0) * Matrix4.CreateRotationZ(_angle) * Matrix4.CreateScale(_scale);

		public static implicit operator Matrix4(Transformation t) => t.Matrix;
		
		public static Matrix4 operator *(Transformation a, Transformation b) => a.Matrix * b.Matrix;
	}
}