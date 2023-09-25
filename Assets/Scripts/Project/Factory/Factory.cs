using UnityEngine;
public class Factory : IFactory
{
	public T Get<T>(T prefab) where T : Object {
		return Object.Instantiate(prefab);
	}
}
