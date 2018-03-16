using System.Collections;
using System.Collections.Generic;
using OpenTK;

namespace Rocket.Scenery {
	internal sealed class Scene : IEnumerable<SceneObject> {
		public readonly Camera Camera = new Camera();
		private readonly List<SceneObject> _objects = new List<SceneObject>();
		
		public void Add(SceneObject obj) => _objects.Add(obj);

		public void Remove(SceneObject obj) => _objects.Remove(obj);
		
		public IEnumerator<SceneObject> GetEnumerator() => _objects.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}