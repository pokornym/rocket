using System;
using OpenTK;

namespace Rocket.Render {
	public static class Vector2Extension {
		public static Vector2 Absolute(this Vector2 vec) {
			return new Vector2(Math.Abs(vec.X), Math.Abs(vec.Y));
		}
	}
}