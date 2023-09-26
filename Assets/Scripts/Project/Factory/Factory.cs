using UnityEngine;
using Zenject;

public class Factory : IFactory {
	private readonly DiContainer _diContainer;

	public Factory(DiContainer diContainer) {
		_diContainer = diContainer;
	}

	public T Get<T>(string path) where T : Object {
		return _diContainer.InstantiatePrefabResourceForComponent<T>(path);
	}
	public T Get<T>(T prefab) where T : UnityEngine.Object {
		//var type = typeof(T); 
		//if(type.IsInterface() || type.DerivesFrom<Component>())
		return _diContainer.InstantiatePrefabForComponent<T>(prefab); //)Instantiate(prefab, Vector3.zero, Quaternion.identity, null);}
	}
}