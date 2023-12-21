using UnityEngine;

public class KeyShop {
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly ProgressSystem _progressSystem;
	private readonly KeyShopView _view;
	private readonly int _keyPrice, _maxKeys;
	private bool _isOpened;

	public KeyShop(GameConfig config, KeysRecoverySystem keysRecoverySystem, ProgressSystem progressSystem, KeyShopView view) {
		_keyPrice = config.keyPrice;
		_maxKeys = config.maxKeys;

		_keysRecoverySystem = keysRecoverySystem;
		_progressSystem = progressSystem;
	
		_view = view;
		_view.BuyForAdsEvent += BuyOnAds;
		_view.BuyForGoldEvent += BuyOnGold;
		_view.CloseEvent += Close;
	}

	private void BuyOnGold() {
		_progressSystem.DecreaseTotalCoins(_keyPrice);
		_progressSystem.IncreaseKey(_maxKeys);
		Close();
	}
	private void BuyOnAds() {
		Debug.Log("Key add for ads");
		_keysRecoverySystem.KeyIncrease(_maxKeys);
		Close();
	}

	public void Open() {
		if(_isOpened) return;
		_keysRecoverySystem.LifeIncreasedEvent += OnKeyIncrease;
		_keysRecoverySystem.TimerTickEvent += OnTimeTicked;

		_view.buyForGoldInteractable = _progressSystem.totalCoinsCount >= _keyPrice;
		_view.coinsCount = _progressSystem.totalCoinsCount.ToString("D2");

		OnTimeTicked();
		OnKeyIncrease();

		_view.Open();
		_isOpened = true;
	}

	private void Close() {
		_keysRecoverySystem.LifeIncreasedEvent -= OnKeyIncrease;
		_keysRecoverySystem.TimerTickEvent -= OnTimeTicked;
		
		_view.Close();
		_isOpened = false;
	}

	private void OnTimeTicked() =>
			_view.timeLeftToRecovery = _keysRecoverySystem.timeLeftToNextSpawnString;
	private void OnKeyIncrease() =>
			_view.keysCount = _progressSystem.keysCount.ToString("D2");
}