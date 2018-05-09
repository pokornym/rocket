using System;
using OpenTK;
using Rocket.Engine;

namespace Rocket {
	public sealed class OrbitalCamera {
		public float Distance {
			get => _dist;
			set {
				_dist = value;
				Configure();
			}
		}
		public Vector3 Pivot {
			get => _pivot;
			set {
				_pivot = value;
				Configure();
			}
		}
		public Vector2 Rotation {
			get => _rot;
			set {
				_rot = value;
				Configure();
			}
		}
		private readonly Camera _cam;
		private float _dist=1;
		private Vector3 _pivot;
		private Vector2 _rot;

		public OrbitalCamera(Camera cam) {
			_cam = cam ?? throw new ArgumentNullException(nameof(cam));
			Configure();
		}

		private void Configure() {
			_cam.Direction = _pivot;
			_cam.Position = _pivot + Distance * new Vector3((float) Math.Cos(_rot.Y) * (float) Math.Sin(_rot.X), (float) Math.Cos(_rot.Y) * (float) Math.Cos(_rot.X), (float) Math.Sin(_rot.Y));
		}
	}
}