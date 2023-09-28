using UnityEngine;
using Zenject;

public class DiFactory : IFactory {
	private readonly DiContainer _diContainer;

	public DiFactory(DiContainer diContainer) {
		_diContainer = diContainer;
	}

	public T Create<T>(string path) where T : Object {
		return _diContainer.InstantiatePrefabResourceForComponent<T>(path);
	}
	public T Create<T>(T prefab, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion)) where T : UnityEngine.Object {
		return _diContainer.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
	}
}