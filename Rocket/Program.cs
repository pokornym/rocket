using OpenTK;
using OpenTK.Graphics;
using Rocket.Render;
using System;

namespace Rocket {
	internal static class Program {
		private static void Main(string[] args) {
			new RocketGame(args).Start();
		}
	}
}