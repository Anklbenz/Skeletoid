using System;
using UnityEngine;

public class WinSystem
{
	private readonly ProgressSystem _progressData;
	private WinView _view;

	public event Action ContinueEvent;

	public WinSystem(ProgressSystem progressData) {
		_progressData = progressData;
	}

	public void Initialize(WinView view) {
		_view = view;
		_view.ForceClose();
		_view.ContinueEvent += OnContinue;
	}

	public void OnWin() {
		_view.SetCurrentLevelNumber(_progressData.currentLevelIndex, _progressData.currentWorldLevelsCount);
		_view.SetCoinsCount(_progressData.currentCoinsCount);
		_view.Open();
	}

	private void OnContinue() {
		_view.Close();
		ContinueEvent?.Invoke();
	}
}