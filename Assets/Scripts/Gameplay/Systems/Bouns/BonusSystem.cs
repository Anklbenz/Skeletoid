using System;
using UnityEngine;

public class BonusSystem : IDisposable, ICombo {
	public event Action<Vector3, int> ComboEvent;

	private readonly BonusConfig _config;
	private readonly Combo _combo;
	private Level _level;
	
	public BonusSystem(BonusConfig config, Combo combo) {
		_config = config;
		_combo = combo;
		_combo.ComboEvent += OnCombo;
	}

	public void Initialize(Level level) {
		_combo.Start(level);
	}
	
	private async void ActivateBackWall() {
		
	}
	

	public void Dispose() {
		_combo.Stop();
	}
	
	private void OnCombo(Vector3 position, int comboCount) {
		if(comboCount >= 3)
             ComboEvent?.Invoke(position, comboCount);
	}

	
}