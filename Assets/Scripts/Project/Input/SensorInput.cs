using System;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SensorInput : IInput, ITickable {
	private const float SWIPE_MAX_DELAY = 0.5f;
	
	public event Action<float> HorizontalAxisChangedEvent;
	public event Action ShotEvent;
	public bool enabled { get; set; } = true;
	private bool touched => Input.touchCount > 0/* && (touchOne.phase == TouchPhase.Stationary || touchOne.phase == TouchPhase.Moved)*/&&!TryHitHud(touchOne.position);
	private Touch touchOne => Input.touches[0];
	private Vector2 _swipeStartPosition;
	private float _upSwipePermitAngle, _swipeRequiredLength;
	private Timer _timer;

	public SensorInput(InputConfig config) {
		_upSwipePermitAngle = config.swipePermitAngle;
		_swipeRequiredLength = config.swipeRequiredLength;
		_timer = new Timer();
	}

	public void Tick() {
		if (!enabled) return;
		
		_timer.Tick();

		var swipeHappened = TryDetectSwipe();
		if (swipeHappened) return;

		if (touched) {
			var horizontalCenter = Screen.width / 2;

			if (horizontalCenter > touchOne.position.x)
				HorizontalAxisChangedEvent?.Invoke(-1);
			else
				HorizontalAxisChangedEvent?.Invoke(1);
		}
		else {
			HorizontalAxisChangedEvent?.Invoke(0);
		}
	}

	private bool TryDetectSwipe() {
		if (Input.touchCount != 1) return false;

		switch (touchOne.phase) {
			case TouchPhase.Began:
				_swipeStartPosition = touchOne.position;
				_timer.Start();
				break;
			case TouchPhase.Ended: {
				_timer.Stop();
				if (_timer.currentSeconds > SWIPE_MAX_DELAY)
					return false;
				
				var swipeEndPosition = touchOne.position;

				var swipeDirection = swipeEndPosition - _swipeStartPosition;
				var swipeDirectionNormalized = swipeDirection.normalized;

				var swipeLength = swipeDirection.magnitude;

				if (swipeLength < _swipeRequiredLength)
					return false;

				var swipeAngle = Vector2.Angle(Vector2.up, swipeDirectionNormalized);
				if (swipeAngle < _upSwipePermitAngle) {
					ShotEvent?.Invoke();
					return true;
				}
				break;
			}
		}
		return false;
	}

	protected bool TryHitHud(Vector3 mousePosition) {
		var pointerData = new PointerEventData(EventSystem.current) {
				position = mousePosition
		};

		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);
		var distanceToUiElement = results.Count > 0 ? results[0].distance : -1;

		return distanceToUiElement == 0;
	}
}