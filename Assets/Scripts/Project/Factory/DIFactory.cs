using UnityEngine;

public class Factory : IFactory {
	public T Get<T>(string path) where T : Object {
		var prefab = Resources.Load<T>(path);
		return Get<T>(prefab);
	}
	public T Get<T>(T prefab, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) where T : Object {
		return Object.Instantiate(prefab, position, rotation, parent);
	}
}