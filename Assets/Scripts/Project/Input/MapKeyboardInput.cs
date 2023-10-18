using System;
using UnityEngine;
using Zenject;

public class MapKeyboardInput : IMapInput, ILateTickable
{
	public event Action<Vector3> DraggingEvent;
	public event Action StartDragEvent;
	public event Action StopDragEvent;

	private readonly Camera _camera;
	private bool _drag;
	private Vector3 _dragStartPoint;

	public MapKeyboardInput() {
		_camera = Camera.main;
	}

	public void LateTick() {
		if (Input.GetMouseButtonDown(0))
			OnDragStart();

		if (Input.GetMouseButtonUp(0))
			OnDragStop();

		if (_drag)
			OnDragging();
	}

	private void OnDragging() {
		var offset = _dragStartPoint - _camera.ScreenToWorldPoint(Input.mousePosition);
		DraggingEvent?.Invoke(offset);
	}

	private void OnDragStop() {
		_drag = false;
		StopDragEvent?.Invoke();
	}

	private void OnDragStart() {
		_drag = true;
		_dragStartPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
		StartDragEvent?.Invoke();
	}
}