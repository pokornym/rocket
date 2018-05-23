using System;
using System.Collections;
using System.Collections.Generic;

namespace Rocket {
	internal sealed class OrderedList<T> : IEnumerable<T> {
		private ListItem _first;
		private readonly Func<T, IComparable> _f;

		public OrderedList(Func<T, IComparable> f) => _f = f ?? throw new ArgumentNullException(nameof(f));

		public IEnumerator<T> GetEnumerator() {
			return new ListEnumerator(_first);
		}

		public void Add(T val) {
			if (_first == null)
				_first = new ListItem(val, _f);
			else if (_first.CompareTo(val) < 0)
				_first = new ListItem(val, _f) { Next = _first };
			else
				_first.Add(val);
		}

		public void Remove(T val) {
			if (_first == null)
				return;
			if (_first.Item.Equals(val))
				_first = _first.Next;
			else
				_first.Remove(val);
		}

		public void Clear() => _first = null;

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private sealed class ListEnumerator : IEnumerator<T> {
			public T Current => _current.Item;
			object IEnumerator.Current => Current;
			private readonly ListItem _first;
			private ListItem _current;
			private bool _skip;

			public ListEnumerator(ListItem first) {
				_first = first;
			}

			public void Dispose() {

			}

			public bool MoveNext() {
				if (_skip) {
					_current = _first;
					_skip = false;
				} else if (_current != null)
					_current = _current.Next;
				if (_current == null)
					return false;
				return true;
			}

			public void Reset() {
				_current = _first;
				_skip = true;
			}
		}

		private sealed class ListItem : IComparable<T> {
			public ListItem Next;
			public readonly T Item;
			private readonly Func<T, IComparable> _f;

			public ListItem(T itm, Func<T, IComparable> f) {
				Item = itm;
				_f = f ?? throw new ArgumentNullException(nameof(f));
			}

			public void Add(T val) {
				if (Next != null) {
					if (Next.CompareTo(val) < 0)
						Next.Add(val);
					else
						Next = new ListItem(val, _f) { Next = Next };
				} else
					Next = new ListItem(val, _f);
			}

			public void Remove(T val) {
				if (Next.Item == null)
					return;
				if (Next.Item.Equals(val))
					Next = Next.Next;
				else
					Next.Remove(val);
			}

			public int CompareTo(T other) {
				return _f(Item).CompareTo(_f(other));
			}
		}
	}
}
