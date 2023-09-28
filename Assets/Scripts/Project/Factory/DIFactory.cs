using UnityEngine;

public class Factory : IFactory {
	public T Create<T>(string path) where T : Object {
		var prefab = Resources.Load<T>(path);
		return Create<T>(prefab);
	}
	public T Create<T>(T prefab, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) where T : Object {
		return Object.Instantiate(prefab, position, rotation, parent);
	}
}