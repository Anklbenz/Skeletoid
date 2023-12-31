using System;
using Zenject;
using UnityEngine;

public class KeyboardInput : IInput, ITickable {
	public event Action<float> HorizontalAxisChangedEvent;
	public event Action ShotEvent;
	public bool enabled { get; set; } = true;
	private readonly InputConfig _config;

	private bool shotPressed => Input.GetKeyDown(_config.keyShot) || Input.GetKeyDown(_config.keyShotExtra);
	private bool leftPressed => Input.GetKey(_config.keyLeft) || Input.GetKey(_config.keyLeftExtra);
	private bool rightPressed => Input.GetKey(_config.keyRight) || Input.GetKey(_config.keyRightExtra);
	private bool nothingPressed => !leftPressed && !rightPressed;

	public KeyboardInput(InputConfig config) {
		_config = config;
	}

	public void Tick() {
		if (!enabled) return;

		if (leftPressed)
			HorizontalAxisChangedEvent?.Invoke(-1);
		if (rightPressed)
			HorizontalAxisChangedEvent?.Invoke(1);
		if (nothingPressed)
			HorizontalAxisChangedEvent?.Invoke(0);

		if (shotPressed)
			ShotEvent?.Invoke();
	}
}