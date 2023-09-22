using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
   [SerializeField] private Swipe swipe;
   [SerializeField] private PressHandler leftPress, rightPress;
   public event Action<Vector3> SwipeEvent, SidePressedEvent;
   public event Action ReleaseEvent;

   private bool _leftPressed, _rightPressed;

   private void Awake() {
      swipe.OnDirectSwipe += OnSwipe;
      leftPress.PressEvent += OnLeftPressed;
      leftPress.ReleaseEvent += OnLeftRelease;

      rightPress.PressEvent += OnRightPressed;
      rightPress.ReleaseEvent += OnRightRelease;
   }

   private void OnLeftPressed() {
      _leftPressed = true;
   }

   private void OnLeftRelease() {
      _leftPressed = false;
   }

   private void OnRightPressed() {
      _rightPressed = true;
   }

   private void OnRightRelease() {
      _rightPressed = false;
   }

   private void OnSwipe(Vector2 direction) {
      SwipeEvent?.Invoke(direction);
   }

   private void Update() {
      if (_leftPressed)
         SidePressedEvent?.Invoke(Vector3.left);
      if (_rightPressed)
         SidePressedEvent?.Invoke(Vector3.right);
   }
}
