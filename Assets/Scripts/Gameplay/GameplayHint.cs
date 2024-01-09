using UnityEngine;

public class GameplayHint 
{
	private readonly GameplayConfig _config;

	public GameplayHint(GameplayConfig config, IInput input) {
		_config = config;
		input.ShotEvent += OnInputEvent;
	//	input.HorizontalAxisChangedEvent += OnInputEvent;
	}
	private void OnInputEvent() {
		throw new System.NotImplementedException();
	}

}
