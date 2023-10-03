using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class FlyingCoinService : IFixedTickable
{
	public event Action CollectedEvent;
	
	private const string COINS_PARENT_NAME = "CoinsParent";
	private const int POOL_AMOUNT = 10;

	private Vector3 targetPosition => _cameraMain.ScreenToWorldPoint(new Vector3(_coinsTarget.transform.position.x, _coinsTarget.transform.position.y, _cameraMain.nearClipPlane));
	

	private readonly CoinServiceConfig _config;
	private readonly List<Transform> _movingCoins = new();
	private readonly ICoinsTarget _coinsTarget;
	private PoolObjects<Coin> _coinsPool;
	private GameObject _coinsParent;
	private Camera _cameraMain;


	public FlyingCoinService(CoinServiceConfig config, ICoinsTarget coinsTarget) {
		_config = config;
		_coinsTarget = coinsTarget;

	}

	public void Initialize() {
		_coinsParent = new GameObject(COINS_PARENT_NAME);
		_coinsPool = new PoolObjects<Coin>(_config.prefab, POOL_AMOUNT, true, _coinsParent.transform);
		_cameraMain = Camera.main;
	}

	public void SpawnCoins(Vector3 position, int count) {
		for (var i = 0; i < count; i++) {
			var coin = _coinsPool.GetFreeElement();
			var randomPointInSphere = Random.insideUnitSphere * _config.radius + position + _config.offset;
			coin.transform.position = randomPointInSphere;
			AddToMovingList(coin.transform);
		}
	}

	private async void AddToMovingList(Transform coinTransform) {
		await UniTask.Delay(_config.delayBeforeStartMoving);
		_movingCoins.Add(coinTransform);
	}

	public void FixedTick() {
		if (_movingCoins.Count <= 0) return;

		for (var i = _movingCoins.Count - 1; i >= 0; i--) {
			_movingCoins[i].transform.position = Vector3.MoveTowards(_movingCoins[i].transform.position, targetPosition, _config.coinSpeed);

			if (_movingCoins[i].transform.position != targetPosition) continue;
			
			_movingCoins[i].gameObject.SetActive(false);
			_movingCoins.RemoveAt(i);
			CollectedEvent?.Invoke();
		}
	}
}