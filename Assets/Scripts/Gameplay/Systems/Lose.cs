using System;
using UnityEngine;

public class Lose {
	private const string ADD_LIFE_TEXT = "+ 1";
	private const string SPEND_LIFE_TEXT = "- 1";

	public event Action RestartEvent, QuitEvent;

	private readonly GameplayConfig _gameplayConfig;
	private readonly ProgressSystem _progressSystem;
	//private readonly CameraSystem _cameraSystem;
	private LoseView _view;

	public Lose(GameplayConfig gameplayConfig, ProgressSystem progressSystem) {
		_gameplayConfig = gameplayConfig;
		_progressSystem = progressSystem;
	}

	public void Initialize(LoseView view) {
		_view = view;
		_view.ForceClose();

		_view.ContinueClickedEvent += OnContinueClicked;
		_view.QuitClickedEvent += OnQuitClicked;
		_view.ShowAdsClickedEvent += OnShowAdsClicked;
	}

	public void OnLose() {
		var livesCount = _progressSystem.SpendLife();
		SetContinueButtonEnabled(livesCount > 0);
		_view.SetLivesWithAnimation(livesCount + 1, livesCount, SPEND_LIFE_TEXT);
		_view.Open();
	}
	
	private void OnShowAdsClicked() {
		Debug.Log("Show Ads");
		var livesCount = _progressSystem.AddLife();
		SetContinueButtonEnabled(livesCount > 0);
		_view.SetLivesWithAnimation(livesCount - 1, livesCount, ADD_LIFE_TEXT);
	}

	private void SetContinueButtonEnabled(bool hasLives) {
		_view.continueVisible = hasLives;
		_view.continueWithAdsButtonVisible = !hasLives;
	}

	private void OnContinueClicked() {
		_view.Close();
		RestartEvent?.Invoke();
	}

	private void OnQuitClicked() {
		_view.Close();
		_progressSystem.ResetCurrentCoins();
		QuitEvent?.Invoke();
	}
}