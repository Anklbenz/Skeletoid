using System;
using UnityEngine;

public class LoseSystem {
	public event Action RestartEvent, QuitEvent;

	private readonly UiFactoryConfig _config;
	private readonly ProgressData _progressData;
	private LoseView _view;

	public LoseSystem(ProgressData progressData) {
		_progressData = progressData;
	}

	public void Initialize(LoseView view) {
		_view = view;
		_view.ForceClose();

		_view.RestartEvent += OnRestart;
		_view.QuitEvent += OnQuit;
		_view.ShowAdsEvent += ShowAds;
	}

	public void OnLose() {
		var hasLives = _progressData.lives.count > 0;
		_view.restartInteractable = hasLives;
		_view.showAdsButtonVisible = !hasLives;
		_view.SetSkullsCount(_progressData.lives.count);
		_progressData.lives.Decrease();
		_view.Open();
	}

	private void ShowAds() {
		Debug.Log("Show Ads");
		_progressData.lives.Increase();
		OnLose();
	}

	private void OnRestart() {
		_view.Close();
		RestartEvent?.Invoke();
	}

	private void OnQuit() {
		_view.Close();
		QuitEvent?.Invoke();
	}
}