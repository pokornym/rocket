using OpenTK;
using OpenTK.Graphics;
using Rocket.Render;
using System;

namespace Rocket {
	internal static class Program {
		private static void Main(string[] args) {
			RocketWindow window = new RocketWindow();
			window.Start(30, 20);
		}
	}
}
