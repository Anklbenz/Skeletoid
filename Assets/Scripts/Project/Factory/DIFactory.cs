using UnityEngine;

public class Factory : IFactory
{
	public T Get<T>(string path) where T : Object {
		var prefab = Resources.Load<T>(path);
		return Get<T>(prefab);
	}

	public T Get<T>(T prefab) where T : UnityEngine.Object {
		return Object.Instantiate(prefab);
	}
}