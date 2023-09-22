using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action PressEvent , ReleaseEvent;

    public void OnPointerDown(PointerEventData eventData) {
        PressEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData) {
       ReleaseEvent?.Invoke();
    }
}
