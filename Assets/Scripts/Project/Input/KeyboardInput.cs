using System;
using UnityEngine;

public class KeyboardInput : IInput/*, ITickable*/ {
	public event Action<float> HorizontalAxisChangedEvent;
	public event Action ShotEvent, AnyPressedEvent;
	public bool enabled { get; set; } = true;
	private readonly InputConfig _config;

	private bool isShotPressed => Input.GetKeyDown(_config.keyShot) || Input.GetKeyDown(_config.keyShotExtra);
	private bool isLeftPressed => Input.GetKey(_config.keyLeft) || Input.GetKey(_config.keyLeftExtra);
	private bool isRightPressed => Input.GetKey(_config.keyRight) || Input.GetKey(_config.keyRightExtra);
	private bool isNothingPressed => !isLeftPressed && !isRightPressed;

	private bool isAnyPressed => Input.anyKey;

	public KeyboardInput(InputConfig config) {
		_config = config;
	}

	public void Tick() {
		if (!enabled) return;

		if (isAnyPressed)
			AnyPressedEvent?.Invoke();

		if (isLeftPressed)
			HorizontalAxisChangedEvent?.Invoke(-1);
		if (isRightPressed)
			HorizontalAxisChangedEvent?.Invoke(1);
		if (isNothingPressed)
			HorizontalAxisChangedEvent?.Invoke(0);

		if (isShotPressed)
			ShotEvent?.Invoke();
	}
}