using System;
using UnityEngine;

public class GameplayHint : IDisposable {
	private readonly IInput _input;
	private readonly Timer _timer;
	private readonly float _hintDelay;

	public GameplayHint(GameplayConfig config, Timer timer, IInput input) {
		_hintDelay = config.gameplayHintDelay;
		_timer = timer;
		_input = input;
		_input.AnyPressedEvent += OnAnyKeyPressed;
	}

	public void SetActive(bool isActive) {
		_timer.StartWithAlarm(_hintDelay);
		_timer.AlarmEvent += OnShot;
	}
	private void OnAnyKeyPressed() {
		//throw new NotImplementedException();
	}


	private void OnShot() {
		Debug.Log("Alarm");
	}
	//private void OnInputAction()=>
	//      _timer.StartWithAlarm()

	public void Dispose() =>
			_input.AnyPressedEvent -= OnAnyKeyPressed;
}