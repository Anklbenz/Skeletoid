using UnityEngine;

public class ResourcesFactory : IResourcesFactory{
	private readonly ResourcesLoader _loader;
	private readonly IFactory _factory;
	
	public ResourcesFactory(ResourcesLoader loader, IFactory factory) {
		_loader = loader;
		_factory = factory;
	}

	public T Get<T>(string path) where T : MonoBehaviour {
		var prefab = _loader.Load<T>(path);
		return _factory.Get(prefab);
	}
}