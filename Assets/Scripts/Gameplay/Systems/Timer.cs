using System;
using UnityEngine;
using Zenject;

public class Timer : ITickable {
	public event Action TickEvent, AlarmEvent;
	public TimeSpan current => TimeSpan.FromSeconds(_totalSeconds);
	public float currentSeconds => _totalSeconds;
	public bool isRunning => _isRunning;

	private float _totalSeconds, _alarmSeconds;
	private bool _isRunning, _isAlarmed;

	public void StartWithAlarm(float alarmSeconds) {
		SetAlarm(alarmSeconds);
		Start();
	}
	public void Start() {
		Reset();
		Run();
	}

	public void Run() =>
			_isRunning = true;

	public void Stop() =>
			_isRunning = false;

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

		if (_isAlarmed && _totalSeconds >= _alarmSeconds)
			Alarm();
	}

	private void Alarm() {
		AlarmEvent?.Invoke();
		_isAlarmed = false;
	}
	private void TickNotify() =>
			TickEvent?.Invoke();
}