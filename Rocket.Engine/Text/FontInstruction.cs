using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rocket.Engine.Text {
	internal sealed class FontInstruction {
		public readonly string Instruction;
		private readonly Dictionary<string, string> _props = new Dictionary<string, string>();

		public FontInstruction(string ln) {
			string[] spl = ln.Split(new[] { ' ', '\t', '\0' }, StringSplitOptions.RemoveEmptyEntries);

			Instruction = spl[0].ToLower();
			for (int i = 1; i < spl.Length; i++) {
				string[] kvp = spl[i].Split('=');
				_props.Add(kvp[0].Trim().ToLower(), kvp[1].Trim());
			}
		}

		public string this[string k] => _props.ContainsKey(k.ToLower()) ? _props[k.ToLower()] : null;
	}
}