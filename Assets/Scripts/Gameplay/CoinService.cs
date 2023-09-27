using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CoinService : IFixedTickable {
	private readonly CoinServiceConfig _coinServiceConfig;
	private PoolObjects<Coin> _coinsPool;
	private List<GameObject> _coins = new();
	
	public CoinService(CoinServiceConfig gameObjectsConfig) {
		_coinServiceConfig = gameObjectsConfig;
	}
	public void Initialize() {
		_coinsPool = new PoolObjects<Coin>(_coinServiceConfig.prefab, 10, true, null);
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
		if(_coins.Count<=0) return;

		foreach (var coin in _coins) {
			//	coin.transform.position
			
		}
	}
}
