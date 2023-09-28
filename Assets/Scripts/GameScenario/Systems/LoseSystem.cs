using System;
using UnityEngine;

public class LoseSystem
{
	private readonly LoseConfig _config;
	private readonly IFactory _factory;
	private readonly ProgressData _progressData;
	private LoseView _loseView;

	public event Action RestartEvent, QuitEvent;

	public LoseSystem(LoseConfig config, ProgressData progressData, IFactory factory) {
		_factory = factory;
		_config = config;
		_progressData = progressData;
	}

	public void Initialize() {
		var canvasParent = _factory.Create<Canvas>(_config.canvasPrefab).transform;
		_loseView = _factory.Create(_config.loseViewPrefab, canvasParent);
		var rectTransform = ((RectTransform)_loseView.transform);
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		_loseView.ForceClose();

		_loseView.RestartEvent += OnRestart;
		_loseView.QuitEvent += OnQuit;
		_loseView.ShowAdsEvent += ShowAds;
	}

	public void OnLose() {
		var hasLives = _progressData.lives.count>0;
		_loseView.restartInteractable = hasLives;
		_loseView.showAdsButtonVisible = !hasLives;
		_loseView.SetSkullsCount(_progressData.lives.count);
		_progressData.lives.Decrease();
		_loseView.Open();
	}

	private void ShowAds() {
		Debug.Log("Show Ads");
		_progressData.lives.Increase();
		OnLose();
	}

	private void OnRestart() {
		_loseView.Close();
		RestartEvent?.Invoke();
	}

	private void OnQuit() {
		_loseView.Close();
		QuitEvent?.Invoke();
	}
}
