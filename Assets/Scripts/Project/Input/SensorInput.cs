using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;

public class SensorInput : IInput, ITickable {
	public event Action<float> HorizontalAxisChangedEvent;
	public event Action ShotEvent;
	public bool enabled { get; set; } = true;
	private readonly InputConfig _config;

	private bool shotPressed => Input.GetKeyDown(_config.keyShot) || Input.GetKeyDown(_config.keyShotExtra);
	private bool leftPressed => Input.GetKey(_config.keyLeft) || Input.GetKey(_config.keyLeftExtra);
	private bool rightPressed => Input.GetKey(_config.keyRight) || Input.GetKey(_config.keyRightExtra);

	private bool touched => Input.touchCount > 0 /*&& touchOne.phase == TouchPhase.Began*//* && !TryHitHud(touchOne.position*/;
	
	
	private bool nothingPressed => !leftPressed && !rightPressed;
	
	private Touch touchOne => Input.touches[0];
	private Touch touchTwo => Input.touches[1];

	public SensorInput(InputConfig config) {
		_config = config;
	}

	/*public void Tick() {
		if (!enabled) return;
		var touch = Input.GetTouch(0);
		
		if()
		
		if (leftPressed)
			HorizontalAxisChangedEvent?.Invoke(-1);
		if (rightPressed)
			HorizontalAxisChangedEvent?.Invoke(1);
		if (nothingPressed)
			HorizontalAxisChangedEvent?.Invoke(0);

		if (shotPressed)
			ShotEvent?.Invoke();
	}*/

	public void Tick() {
		if (!enabled) return;

		CheckSwipe();
		
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

	private void CheckSwipe() {
		Vector2 swipeStartPosition = Vector2.zero;
		if (Input.touchCount == 1) {
			if (touchOne.phase == TouchPhase.Began)
				swipeStartPosition = touchOne.position;
			
			if (touchOne.phase == TouchPhase.Ended) {
				 var swipeEndPosition = touchOne.position;
				 var swipeDirection = swipeEndPosition - swipeStartPosition; 
				 var swipeDirectionNormalized = swipeDirection.normalized; 
				 
				 var swipeLength = swipeDirection.magnitude;
				 Debug.Log("swipeLength "+swipeLength);
				 if(swipeLength < 10)
					 return;
				 
				 var swipeAngle = Vector2.Angle(Vector2.up, swipeDirectionNormalized);
				 Debug.Log("Angle "+swipeAngle);
				 if(swipeAngle<45)
					 ShotEvent?.Invoke();
			}
		}
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