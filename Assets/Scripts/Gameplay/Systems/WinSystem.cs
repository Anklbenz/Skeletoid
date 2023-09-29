using System;
using UnityEngine;

public class WinSystem {
	private readonly ProgressData _progressData;
	private WinView _view;

	public event Action ContinueEvent;

	public WinSystem(ProgressData progressData) {
		_progressData = progressData;
	}

	public void Initialize(WinView view) {
		_view = view;
		_view.ForceClose();
		_view.ContinueEvent += OnContinue;
	}

	public void OnWin() {
		_view.SetCoinsCount(_progressData.currentCoins.count);
		_view.Open();
	}

	private void OnContinue() {
		_view.Close();
		ContinueEvent?.Invoke();
	}
}