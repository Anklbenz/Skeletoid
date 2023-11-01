using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KeysRecoverySystem : IDisposable
{
   private const int REFRESH_TIMER_DELAY = 1000;
   public event Action LifeIncreasedEvent, TimerTickEvent;

   public TimeSpan timeLeftToNextSpan => _timeLeft;
   public int maxKeysCount { get; }
   public string timeLeftToNextSpawnString => _timeLeft.ToString(@"mm\:ss");
   private bool isKeysCountMax => _progressSystem.keysCount == _gameConfig.maxKeys;
   private long timeLeftToIncrease => nextIncreaseTime - ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
   private long nowSeconds => ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
   private long nextIncreaseTime => _progressSystem.lastSpendTime + _gameConfig.keyIncreaseTime;

   private readonly ProgressSystem _progressSystem;
   private readonly GameConfig _gameConfig;

   private TimeSpan _timeLeft = TimeSpan.Zero;
   private float _time;
   private bool _isRunning;

   public KeysRecoverySystem(ProgressSystem progressSystem, GameConfig gameConfig) {
      _progressSystem = progressSystem;
      _gameConfig = gameConfig;
      maxKeysCount = _gameConfig.maxKeys;
   }

   public void Initialize() {
      _isRunning = true;
      Refresh();
      TickCycle();
   }

   private void Refresh() {
      if (_progressSystem.lastSpendTime == 0)
         _progressSystem.lastSpendTime = nowSeconds;

      var secondsSpend = nowSeconds - _progressSystem.lastSpendTime;
      var keysEarned = (int)(secondsSpend / _gameConfig.keyIncreaseTime);

      if (keysEarned < 1) return;
      _progressSystem.lastSpendTime += keysEarned * _gameConfig.keyIncreaseTime;

      var keysCount = _progressSystem.keysCount;
      var clampedTotal = Mathf.Clamp(keysCount + keysEarned, 0, maxKeysCount);
      _progressSystem.IncreaseKey(clampedTotal - keysCount);

      LifeIncreasedEvent?.Invoke();
   }

   public void KeyDecrease() {
      _progressSystem.lastSpendTime = nowSeconds;
      _progressSystem.SpendKey();
   }

   private async void TickCycle() {
      while (_isRunning) {
         if (isKeysCountMax) return;
         _timeLeft = TimeSpan.FromSeconds(timeLeftToIncrease);

         if (_timeLeft.TotalSeconds <= 0)
            Refresh();
         
         TimerTickEvent?.Invoke();
         await UniTask.Delay(REFRESH_TIMER_DELAY);
      }
   }

   public void Dispose() =>
      _isRunning = false;
}