using System;
using UnityEngine;

public class LoseSystem {
	public event Action RestartEvent, QuitEvent;

	private readonly UiFactoryConfig _config;
	private readonly ProgressSystem _progressSystem;
	private LoseView _view;

	public LoseSystem(ProgressSystem progressSystem) {
		_progressSystem = progressSystem;
	}

	public void Initialize(LoseView view) {
		_view = view;
		_view.ForceClose();

		_view.RestartEvent += OnRestart;
		_view.QuitEvent += OnQuit;
		_view.ShowAdsEvent += ShowAds;
	}

	public void OnLose() {
		var hasLives = _progressSystem.hasLives;
		_view.restartInteractable = hasLives;
		_view.showAdsButtonVisible = !hasLives;
		_view.SetSkullsCount(_progressSystem.livesCount);
		_progressSystem.SpendLife();
		_view.Open();
	}

	private void ShowAds() {
		Debug.Log("Show Ads");
		_progressSystem.AddLife();
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