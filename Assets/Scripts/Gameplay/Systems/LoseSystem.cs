using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoseSystem
{
	public event Action RestartEvent, QuitEvent;

	private readonly GameplayConfig _gameplayConfig;
	private readonly ProgressSystem _progressSystem;
	private readonly CameraSystem _cameraSystem;
	private LoseView _view;
	private int _numbersAdsLeft;

	public LoseSystem(GameplayConfig gameplayConfig, ProgressSystem progressSystem) {
		_gameplayConfig = gameplayConfig;
		_progressSystem = progressSystem;
	
	}

	public void Initialize(LoseView view) {
		_view = view;
		_view.ForceClose();

		_view.RestartEvent += OnRestart;
		_view.QuitEvent += OnQuit;
		_view.ShowAdsEvent += ShowAds;
		_numbersAdsLeft = _gameplayConfig.showingAdsNumber;
	}

	public  void OnLose() {
		
		var hasLives = _progressSystem.hasLives;
		_progressSystem.SpendLife();
		_view.showAdsButtonInteractable = _numbersAdsLeft > 0;
		_view.restartInteractable = hasLives;
		_view.showAdsButtonVisible = !hasLives;
		_view.SetSkullsCount(_progressSystem.livesCount);
		_view.Open();
	}



	private void ShowAds() {
		Debug.Log("Show Ads");
		_numbersAdsLeft--;
		_progressSystem.AddLife();
		OnLose();
	}

	private void OnRestart() {
		_view.Close();
		RestartEvent?.Invoke();
	}

	private void OnQuit() {
		_view.Close();
		_progressSystem.ResetCurrentCoins();
		QuitEvent?.Invoke();
	}
}