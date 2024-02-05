using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

public class WebServices {
	private YandexGame _yandexGame;
	private UniTaskCompletionSource<bool> _videoRewardCompletionSource;

	public void Initialize(YandexGame yandexGame) {
		_yandexGame = yandexGame;
	}

	public async UniTask<bool> ShowRewardVideo() {
		_videoRewardCompletionSource = new UniTaskCompletionSource<bool>();
		_yandexGame._RewardedShow(0);

		YandexGame.RewardVideoEvent += OnVideoRewardSuccess;
		YandexGame.ErrorVideoEvent += OnVideoError;

		var result = await _videoRewardCompletionSource.Task;

		YandexGame.ErrorVideoEvent -= OnVideoError;
		YandexGame.RewardVideoEvent -= OnVideoRewardSuccess;

		return result;
	}
	private void OnVideoRewardSuccess(int i) {
		Debug.Log("OnReward");
		_videoRewardCompletionSource.TrySetResult(true);
	}
	private void OnVideoError() {
		Debug.Log("OnError");
		_videoRewardCompletionSource.TrySetResult(false);
	}
}