using System;
using UnityEngine;
using Zenject;

public class StarsTimer : ITickable {
	private const int MAX_STARS_COUNT = 3;
	public int starsCount => _starsCount;
	public float levelTime => _timer.currentSeconds;
	public TimeSpan levelTimeSpan => _timer.current;

	private readonly Timer _timer;
	private bool _isFistStarLost, _isSecondStarLost, _isThirdStarLost;
	private float _firstStarTime, _secondStarTime, _thirdStarTime;
	private int _starsCount = MAX_STARS_COUNT;

	public StarsTimer() {
		_timer = new Timer();
		_timer.TickEvent += OnTimerRefresh;
		}

	public void Initialize(Vector3 timeLimits) {
		_firstStarTime = timeLimits.x;
		_secondStarTime = timeLimits.y;
		_thirdStarTime = timeLimits.z;

		_isFistStarLost = _isSecondStarLost = _isThirdStarLost = false;
		_timer.Reset();
	}

	public void Start() =>
			_timer.Run();

	public void Stop() =>
			_timer.Stop();

	private void OnTimerRefresh() {
		if (!_isFistStarLost && _firstStarTime < _timer.currentSeconds) {
			_isFistStarLost = true;
			StarsCountDecrease();
		}
		if (!_isSecondStarLost && _secondStarTime < _timer.currentSeconds) {
			_isSecondStarLost = true;
			StarsCountDecrease();
		}
		if (!_isThirdStarLost && _thirdStarTime < _timer.currentSeconds) {
			_isThirdStarLost = true;
			StarsCountDecrease();
		}
		//upd hud
		//	Debug.Log($"{_timer.current.Minutes:00}:{_timer.current.Seconds:00}:{_timer.current.Milliseconds:000}");
	}

	private void StarsCountDecrease() =>
			_starsCount--;

	public void Tick() =>
			_timer.Tick();
}