using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public event Action<Vector2> OnDirectSwipe;
    private Vector2 _dragStartPosition;
    
    public void OnBeginDrag(PointerEventData eventData) {
        _dragStartPosition = eventData.position;
    }
    
    public void OnDrag(PointerEventData eventData) {
        var direction = eventData.position - _dragStartPosition;

        var dot =Vector2.Dot(direction, Vector2.up);
        var swipeMagnitude = (eventData.position - _dragStartPosition).magnitude;
     
        if (swipeMagnitude > 100) {
            Debug.Log("Dot " + dot);
            Debug.Log("Magnitude " + swipeMagnitude);
            
            OnDirectSwipe?.Invoke(direction.normalized);
        }
    }
}
