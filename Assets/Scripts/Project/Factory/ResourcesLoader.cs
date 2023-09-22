using UnityEngine;

public class ResourcesLoader {
	public T Load<T>(string path) where T : MonoBehaviour {
		return Resources.Load<T>(path);
	}
}