using UnityEngine;

public class Factory : IGameObjectFactoryByPath{
	private readonly ResourcesLoader _loader;
	private readonly IGameObjectFactory _gameObjectFactory;
	
	public Factory(ResourcesLoader loader, IGameObjectFactory gameObjectFactory) {
		_loader = loader;
		_gameObjectFactory = gameObjectFactory;
	}

	public T Get<T>(string path) where T : MonoBehaviour {
		var prefab = _loader.Load<T>(path);
		return _gameObjectFactory.Get(prefab);
	}
}