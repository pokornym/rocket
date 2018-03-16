using System;
using OpenTK;

namespace Rocket.Scenery {
	public sealed class Camera {
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
		public Matrix4 View { get; private set; }
		private Vector2 _pos;
		private float _angle;
		private float _scale = 1;

		public Camera() {
			ComputeMatrix();
		}

		private void ComputeMatrix() => View = Matrix4.CreateTranslation(_pos.X, _pos.Y, 0) * Matrix4.CreateRotationZ(_angle) * Matrix4.CreateScale(_scale);
	}
}