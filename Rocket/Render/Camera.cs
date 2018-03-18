using System;
using OpenTK;

namespace Rocket.Render {
	internal sealed class Camera {
		public Vector2 Position {
			get => _pos;
			set {
				_pos = value;
				ComputeMatrix();
			}
		}
		public float Angle {
			get => _angle % ((float) Math.PI * 2);
			set {
				_angle = value;
				ComputeMatrix();
			}
		}
		public float Zoom {
			get => _zoom;
			set {
				_zoom = value;
				ComputeMatrix();
			}
		}
		public Matrix4 Matrix { get; private set; }
		private Vector2 _pos = Vector2.Zero;
		private float _angle = 0;
		private float _zoom = 1;

		public Camera() => ComputeMatrix();

		private void ComputeMatrix() => Matrix = Matrix4.CreateScale(_zoom) * Matrix4.CreateRotationZ(_angle) * Matrix4.CreateTranslation(_zoom * -_pos.X, _zoom * -_pos.Y, 0);
	}
}