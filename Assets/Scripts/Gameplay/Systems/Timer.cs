using System;
using UnityEngine;
using Zenject;

public class Timer : ITickable
{
   public event Action TickEvent;
   public TimeSpan current => TimeSpan.FromSeconds(_totalSeconds);
   public float currentSeconds => _totalSeconds;

   private float _totalSeconds;
   private bool _isRunning;

   public void Start() =>
      _isRunning = true;

   public void Stop() =>
      _isRunning = false;

   public void Reset() =>
      _totalSeconds = 0;
   
   public void Tick() {
      if (!_isRunning) return;
      _totalSeconds += Time.deltaTime;
      TickNotify();
   }
   private void TickNotify() =>
      TickEvent?.Invoke();
}