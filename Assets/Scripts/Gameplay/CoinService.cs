using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class CoinService : IFixedTickable {
	private const string COINS_PARENT = "CoinsParent";
	private readonly CoinServiceConfig _coinServiceConfig;
	private readonly List<GameObject> _coins = new();
	private PoolObjects<Coin> _coinsPool;
	private GameObject _coinsParent;

	public CoinService(CoinServiceConfig gameObjectsConfig) {
		_coinServiceConfig = gameObjectsConfig;
	}
	public void Initialize() {
		_coinsParent = new GameObject(COINS_PARENT);
		_coinsPool = new PoolObjects<Coin>(_coinServiceConfig.prefab, 10, true, _coinsParent.transform);
	}

	public void SpawnCoins(Vector3 position, int count) {
		for (var i = 0; i < count; i++) {
			var coin = _coinsPool.GetFreeElement();
			var randomPointInSphere = Random.insideUnitSphere * _coinServiceConfig.radius + position + _coinServiceConfig.offset;
			coin.transform.position = randomPointInSphere;
			_coins.Add(coin.gameObject);
		}
	}
	public void FixedTick() {
		if (_coins.Count <= 0) return;

		foreach (var coin in _coins) {
			//	coin.transform.position

		}
	}
}