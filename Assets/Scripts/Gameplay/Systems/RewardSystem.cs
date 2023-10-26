using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem
{
	private const int MAX_STARS_COUNT = 3;
	public int starsCount => _starsCount;
	public float levelTime => _timer.currentSeconds;
	
	private readonly Timer _timer;
	private bool _isFistStarLost, _isSecondStarLost, _isThirdStarLost;
	private float _firstStarTime, _secondStarTime, _thirdStarTime;
	private int _starsCount =MAX_STARS_COUNT;
	
	public RewardSystem(HudSystem hudSystem, Timer timer) {
		_timer = timer;
		_timer.TickEvent += OnTimerRefresh;
	}

	public void Initialize(float first, float second, float third) {
		_firstStarTime = first;
		_secondStarTime = second;
		_thirdStarTime = third;

		_isFistStarLost = _isSecondStarLost = _isThirdStarLost = false;
		_timer.Reset();
	}

	public void Start() {
		_timer.Start();
	}

	public void Stop() {
		_timer.Stop();
	}

	private void OnTimerRefresh() {
		if (!_isFistStarLost && _firstStarTime > _timer.currentSeconds) {
			_isFistStarLost = true;
			StarsCountDecrease();
		}
		if(!_isSecondStarLost && _secondStarTime > _timer.currentSeconds) {
			_isSecondStarLost = true;
			StarsCountDecrease();
		}
		if(!_isThirdStarLost && _thirdStarTime > _timer.currentSeconds) {
			_isThirdStarLost = true;
			StarsCountDecrease();
		}
		//upd hud
		Debug.Log($"{_timer.current.Minutes:00}:{_timer.current.Seconds:00}:{_timer.current.Milliseconds:000}");
	}

	private void StarsCountDecrease() =>
		_starsCount--;
}
