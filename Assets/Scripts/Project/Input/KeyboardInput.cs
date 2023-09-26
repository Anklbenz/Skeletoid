using System;
using Zenject;
using UnityEngine;

public class KeyboardInput : IInput, ITickable {
	public event Action Left;
	public event Action Right;
	public event Action Shot;
	public bool Enabled { get; set; }
	private readonly KeyboardConfig _config;

	public KeyboardInput(KeyboardConfig config) {
		_config = config;
	}

	public void Tick() {
		if (!Enabled) return;

		if (Input.GetKey(_config.keyLeft) || Input.GetKey(_config.keyLeftExtra))
			Left?.Invoke();
		if (Input.GetKey(_config.keyRight) || Input.GetKey(_config.keyRightExtra))
			Right?.Invoke();
		if (Input.GetKeyDown(_config.keyShot) || Input.GetKeyDown(_config.keyShotExtra))
			Shot?.Invoke();
	}
}