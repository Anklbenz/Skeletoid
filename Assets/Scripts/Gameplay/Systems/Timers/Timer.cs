using System;
using UnityEngine;
using Zenject;

public class Timer : ITickable {
	public event Action TickEvent, AlarmEvent;
	public TimeSpan current => TimeSpan.FromSeconds(_totalSeconds);
	public float currentSeconds => _totalSeconds;
	public bool isRunning => _isRunning;

	private float _totalSeconds, _alarmSeconds;
	private bool _isRunning, _isAlarmed, _isRepeatEnabled;

	public void StartWithAlarm(float alarmSeconds) {
		SetAlarm(alarmSeconds);
		Start();
	}
	public void Start() {
		Reset();
		Run();
	}
	public void Stop() =>
			_isRunning = false;

	public void Run() =>
			_isRunning = true;

	public void Reset() =>
			_totalSeconds = 0;

	private void SetAlarm(float alarmSeconds) {
		_isAlarmed = true;
		_alarmSeconds = alarmSeconds;
	}

	public void Tick() {
		if (!_isRunning) return;
		_totalSeconds += Time.deltaTime;

		TickNotify();

		var isAlarmHappened = _isAlarmed && _totalSeconds >= _alarmSeconds;
		if (!isAlarmHappened) return;

		AlarmNotify();
		AlarmReset();
	}

	private void AlarmNotify() =>
			AlarmEvent?.Invoke();

	private void AlarmReset() {
		_isAlarmed = false;
		Stop();
	}
	private void TickNotify() =>
			TickEvent?.Invoke();
}