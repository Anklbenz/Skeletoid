using System;

public class BonusSystem : IDisposable {
	public event Action<int> ComboEvent;

	private readonly Combo _combo;
	private Level _level;
	
	public BonusSystem(Combo combo) {
		_combo = combo;
		_combo.ComboEvent += OnCombo;
	}

	public void Initialize(Level level) {
		_combo.Start(level);
	}

	public void Dispose() {
		_combo.Stop();
	}
	
	private void OnCombo(int comboCount) {
		if(comboCount > 3)
             ComboEvent?.Invoke(comboCount);			
			
	}

	private void ApplyHitCombo() {
		
	}
}