using System;
using UnityEngine;
public class BonusSystem : IDisposable, IBonusEvents, IPauseSensitive {
	public event Action<Vector3, int> ComboEvent;
	public event Action WallActivateEvent;

	private readonly BonusConfig _config;
	private readonly ILevelEvents _levelEvents;
	private Level _level;
	private StoneBackWall backWall => _level.backWall;

	public BonusSystem(BonusConfig config, ILevelEvents levelEvents) {
		_config = config;
		_levelEvents = levelEvents;
		_levelEvents.ComboEvent += OnCombo;
	}

	public void Initialize(Level level) {
		_level = level;
	}

	private void ActivateBackWall() {
		backWall.Activate(_config.backWallActiveTime);
	}

	public void Dispose() =>
			_levelEvents.ComboEvent -= OnCombo;

	private void OnCombo(Vector3 position, int comboCount) {
		if (comboCount >= 3) {
			ComboEvent?.Invoke(position, comboCount);
			ActivateBackWall();
		}
	}
	public void SetPause(bool isPaused) =>
			backWall.SetPause(isPaused);
}