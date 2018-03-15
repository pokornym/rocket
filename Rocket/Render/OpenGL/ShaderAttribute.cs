using System;

namespace Rocket.Render.OpenGL {
	internal class ShaderAttribute {
		private readonly int _location;
		public readonly string Name;
		public readonly ShaderElementType Type;

		internal ShaderAttribute(string name, ShaderElementType type, int loc) {
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));
			Name = name;
			Type = type ?? throw new ArgumentNullException(nameof(type));
			_location = loc;
		}

		public override string ToString() {
			return $"{Type} {Name}";
		}
	}
}