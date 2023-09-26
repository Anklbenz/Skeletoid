using UnityEngine;
using Zenject;

public class DIFactory : IFactory {
	private readonly DiContainer _diContainer;

	public DIFactory(DiContainer diContainer) {
		_diContainer = diContainer;
	}

	public T Get<T>(string path) where T : Object {
		return _diContainer.InstantiatePrefabResourceForComponent<T>(path);
	}
	public T Get<T>(T prefab) where T : UnityEngine.Object {
		return _diContainer.InstantiatePrefabForComponent<T>(prefab); 
	}
}