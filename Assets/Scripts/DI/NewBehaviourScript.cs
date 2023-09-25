using System;
using UnityEngine;
using Zenject;

public class NewBehaviourScript : MonoBehaviour {
	private IResourcesFactory _factory;
	
	[Inject] 
	public void Constructor(IResourcesFactory factory) {
		_factory = factory;
	}

	private void Update() {
		if (Input.GetMouseButton(0))
			_factory.Get<Ball>("Sphere");
	}
}
