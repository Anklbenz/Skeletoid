using UnityEngine;
public class GameObjectFactory : IGameObjectFactory
{
	public T Get<T>(T prefab) where T : MonoBehaviour {
		return Object.Instantiate(prefab);
	}
}
