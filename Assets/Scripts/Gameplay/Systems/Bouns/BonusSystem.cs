using System;
using UnityEngine;
public class BonusSystem : IDisposable, IComboEvent, IStoneWallEvent, IPauseSensitive {
	public event Action<Vector3, int> ComboEvent;
	public event Action WallActivateEvent; 

	private readonly BonusConfig _config;
	private readonly Combo _combo;
	private Level _level;
	private StoneBackWall backWall => _level.backWall;

	public BonusSystem(BonusConfig config, Combo combo) {
		_config = config;
		_combo = combo;
		_combo.ComboEvent += OnCombo;
	}

	public void Initialize(Level level) {
		_level = level;
		_combo.Start(level);
	}

	private void ActivateBackWall() {
		backWall.Activate(_config.backWallActiveTime);
	}

	public void Dispose() {
		_combo.Stop();
	}

	private void OnCombo(Vector3 position, int comboCount) {
		if (comboCount >= 3) {
			ComboEvent?.Invoke(position, comboCount);
			ActivateBackWall();
		}
	}
	public void SetPause(bool isPaused) {
		backWall.SetPause(isPaused);
	}
}