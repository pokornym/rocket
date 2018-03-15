using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Render.OpenGL {
	internal class VertexLayout : IEnumerable<LayoutElement> {
		private readonly Dictionary<string, LayoutElement> _elements = new Dictionary<string, LayoutElement>();
		private int _index;
		public int VertexSize { get; private set; }

		public IEnumerator<LayoutElement> GetEnumerator() {
			return _elements.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void AddFloat(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.Float, norm);
		}

		public void AddDouble(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.Double, norm);
		}

		public void AddUInt(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.UInt, norm);
		}

		public void AddInt(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.Int, norm);
		}

		public void AddUShort(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.UShort, norm);
		}

		public void AddShort(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.Short, norm);
		}

		public void AddByte(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.Byte, norm);
		}

		public void AddSByte(string nm, int count = 1, bool norm = false) {
			Add(nm, count, DataTypes.SByte, norm);
		}

		public void Add(string nm, int c, DataTypes t, bool norm = false) {
			if (string.IsNullOrWhiteSpace(nm))
				throw new ArgumentNullException(nameof(nm));
			if (c < 1 || c > 4)
				throw new ArgumentOutOfRangeException(nameof(c), c, "Expected value 1, 2, 3 or 4!");
			LayoutElement ele = new LayoutElement(_index++, VertexSize, nm, c, t, norm);
			_elements.Add(nm, ele);
			VertexSize += ele.TotalSize;
		}

		public void SetPointers(bool e = true) {
			foreach (LayoutElement ele in _elements.OrderBy(i => i.Value.Index).Select(i => i.Value)) {
				if (e)
					ele.Enable();
				ele.SetPointer(VertexSize);
			}
		}

		public void Enable() {
			foreach (LayoutElement ele in _elements.OrderBy(i => i.Value.Index).Select(i => i.Value))
				ele.Enable();
		}

		public void Disable() {
			foreach (LayoutElement ele in _elements.OrderBy(i => i.Value.Index).Select(i => i.Value))
				ele.Disable();
		}
	}
}