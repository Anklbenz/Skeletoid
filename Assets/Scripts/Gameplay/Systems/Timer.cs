using System;
using UnityEngine;
using Zenject;

public class Timer : ITickable
{
   public event Action<float> TickEvent;
   public TimeSpan current => TimeSpan.FromSeconds(_totalMilliseconds);

   private float _totalMilliseconds;
   private bool _isRunning;

   public void Start() =>
      _isRunning = true;

   public void Stop() =>
      _isRunning = false;

   public void Reset() =>
      _totalMilliseconds = 0;
   
   public void Tick() {
      if (!_isRunning) return;
      _totalMilliseconds += Time.deltaTime;
      TickNotify();
   }
   private void TickNotify() =>
      TickEvent?.Invoke(_totalMilliseconds);
}