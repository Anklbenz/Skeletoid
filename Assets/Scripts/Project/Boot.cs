using UnityEngine;
using Zenject;

public class Boot : MonoBehaviour {
	private SceneLoaderService _service;
	
	[Inject] public void Constructor(SceneLoaderService service) {
		_service = service;
	}
	
	private void Awake() {
		_service.GoToGameplayScene();
	}
}
