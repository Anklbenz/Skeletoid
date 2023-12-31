using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class FlyingService : IFixedTickable {
	public event Action CollectedEvent, AllCollectedEvent;
	public Vector3 destination { get; set; }

	private readonly FlyingCoinsConfig _config;
	private readonly List<Transform> _flyingList = new();
	private PoolObjects<FlyingElement> _flyingItemsPool;

	public FlyingService(FlyingCoinsConfig config) {
		_config = config;
		//	_config.prefab.transform.localScale = _config.prefabScale;
	}
	public void Initialize(Transform parent = null) {
		if (parent == null)
			parent = new GameObject(_config.itemsParentName).transform;

		_flyingItemsPool = new PoolObjects<FlyingElement>(_config.prefab, _config.itemsPoolAmount, true, parent);
	}

	public void SpawnInSphere(Vector3 sphereCenter, int count) {
		for (var i = 0; i < count; i++) {
			var randomPointInSphere = Random.insideUnitSphere * _config.radius + sphereCenter + _config.offsetInSphere;
			CreateWithDelays(randomPointInSphere);
		}
	}

	public void SpawnInCircle(Vector2 circleCenter, int count) {
		for (var i = 0; i < count; i++) {
			var randomPointInCircle = Random.insideUnitCircle * _config.radius + circleCenter + _config.offsetInCircle;
			CreateWithDelays(randomPointInCircle);
		}
	}

	private async void CreateWithDelays(Vector3 position) {
		await UniTask.Delay(_config.delayBeforeAppear);
		var coin = _flyingItemsPool.GetFreeElement();
		coin.position = position;

		AddToFlyingListWithDelay(coin.transform);
	}

	private async void AddToFlyingListWithDelay(Transform coinTransform) {
		await UniTask.Delay(_config.delayBeforeStartMoving);
		_flyingList.Add(coinTransform);
	}

	public void FixedTick() {
		if (_flyingList.Count <= 0) return;

		for (var i = _flyingList.Count - 1; i >= 0; i--) {
			_flyingList[i].transform.position = Vector3.MoveTowards(_flyingList[i].transform.position, destination, _config.coinSpeed);

			if (_flyingList[i].transform.position != destination) continue;

			_flyingList[i].gameObject.SetActive(false);
			_flyingList.RemoveAt(i);

			CollectedEvent?.Invoke();

			if (_flyingList.Count == 0)
				AllCollectedEvent?.Invoke();
		}
	}
}