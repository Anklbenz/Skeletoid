using UnityEngine;
public class MapHud : ICoinsTarget {
	public Transform coinsTargetTransform => _view.coinsTargetTransform;
	private readonly ProgressSystem _progressSystem;
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly MapHudView _view;
	private int _coinsCount;


	public MapHud(ProgressSystem progressSystem, KeysRecoverySystem keysRecoverySystem, MapHudView view) {
		_progressSystem = progressSystem;
		_keysRecoverySystem = keysRecoverySystem;
		_keysRecoverySystem.LifeIncreasedEvent += LivesRefresh;
		_keysRecoverySystem.TimerTickEvent += RefreshTimer;
		_view = view;
	}

	public void AddCoin() =>
			_view.coinsCount = ShowWithSymbol(++_coinsCount);
	public void Refresh() {
		_coinsCount = _progressSystem.totalCoinsCount;
		_view.coinsCount = ShowWithSymbol(_progressSystem.totalCoinsCount);
		_view.starsCount = $"{_progressSystem.starsCount:D2}";
		RefreshTimer();
		LivesRefresh();
	}

	private void LivesRefresh() =>
			_view.keysCount = $"{_progressSystem.keysCount:D2}";

	private void RefreshTimer() {
		_view.isTimerActive = _keysRecoverySystem.maxKeysCount != _progressSystem.keysCount;
		_view.timeLeft = _keysRecoverySystem.timeLeftToNextSpawnString;
	}

	private string ShowWithSymbol(int count) =>
			count < 1000 ? $"{count:D2}" : $"{(count / 1000).ToString()}k";
}