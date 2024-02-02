using Cysharp.Threading.Tasks;
using YG;

public class WebServices {
	private YandexGame _yandexGame;
	private UniTaskCompletionSource<bool> _videoRewardCompletionSource;

	public void Initialize(YandexGame yandexGame) {
		_yandexGame = yandexGame;
	}

	public async UniTask<bool> ShowRewardVideo() {
		_videoRewardCompletionSource = new UniTaskCompletionSource<bool>();
		//_yandexGame.CloseVideoAd.AddListener(OnVideoRewardSuccess);
		_yandexGame.RewardVideoAd.AddListener(OnVideoRewardSuccess);
		_yandexGame.ErrorVideoAd.AddListener(OnVideoError);
		_yandexGame._RewardedShow(0);

		var result = await _videoRewardCompletionSource.Task;

		_yandexGame.CloseVideoAd.RemoveListener(OnVideoRewardSuccess);
		_yandexGame.ErrorVideoAd.RemoveListener(OnVideoError);

		return result;
	}
	private void OnVideoRewardSuccess() {
		_videoRewardCompletionSource.TrySetResult(true);
	}
	private void OnVideoError() {
		_videoRewardCompletionSource.TrySetResult(false);
	}
}