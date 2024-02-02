using System;
using UnityEngine;

public class HudSystem : IFlyingTarget {
	public event Action PauseValueChangedEvent;
	public Transform coinsTargetTransform => _view.coinsTransform;

	private readonly ProgressSystem _progress;

	private HudView _view;

	private int _hudCoinsCount;

	public HudSystem(ProgressSystem progress) {
		_progress = progress;
	}

	public void Initialize(HudView view) {
		var isMobile = Application.isMobilePlatform;
		_view = view;
		_view.SetCurrentTrainingAnimation(isMobile);
		_view.PauseClickedEvent += OnPauseValueChanged;
		Refresh();
	}

	public void SetActive(bool isActive) {
		if (isActive)
			_view.Open();
		else
			_view.Close();
	}

	public void ShowTraining() {
		_view.isHudVisible = false;
		_view.isTrainingVisible = true;
	}

	public void HideTraining() {
		_view.isHudVisible = true;
		_view.isTrainingVisible = false;
	}

	public void IncreaseCoinsCount(int count = 1) {
		_hudCoinsCount += count;
		_view.coinsCount = _hudCoinsCount.ToString("D2");
		_view.CoinsAnimationPlay();
	}

	public void Refresh() {
		_view.coinsCount = _progress.currentCoinsCount.ToString("D2");
		_view.skullsCount = _progress.livesCount.ToString("D2");
	}
	private void OnPauseValueChanged() =>
			PauseValueChangedEvent?.Invoke();
}