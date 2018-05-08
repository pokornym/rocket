using System;
using OpenTK;

namespace Rocket.Engine {
	public sealed class Camera {
		public Vector3 Position {
			get => _pos;
			set {
				_pos = value;
				ComputeMatrix();
			}
		}
		public Vector3 Direction {
			get => _dir;
			set {
				_dir = value;
				ComputeMatrix();
			}
		}
		public Vector3 Up {
			get => _up;
			set {
				_up = value;
				ComputeMatrix();
			}
		}
		public Matrix4 Matrix { get; private set; }
		private Vector3 _pos = new Vector3(-300f, 0, 0);
		private Vector3 _dir = new Vector3(1f, 0, 0);
		private Vector3 _up = new Vector3(0, 0f, 1f);

		public Camera() => ComputeMatrix();

		private void ComputeMatrix() => Matrix = Matrix4.LookAt(_pos, _dir, _up);
	}
}