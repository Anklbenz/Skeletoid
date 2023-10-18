using System;
using UnityEngine;

public class HudSystem : ICoinsTarget
{
	public event Action PauseValueChangedEvent;
	public Transform transform => _view.coinsTransform;
	private readonly ProgressSystem _progress;
	private HudView _view;

	public HudSystem(ProgressSystem progress) {
		_progress = progress;
	}

	public void Initialize(HudView view) {
		_view = view;
		_view.PauseClickedEvent += OnPauseValueChanged;
		Refresh();
	}

	public void SetActive(bool isActive) {
		if (isActive)
			_view.Open();
		else
			_view.Close();
	}

	private void OnPauseValueChanged() =>
		PauseValueChangedEvent?.Invoke();

	public void Refresh() {
		_view.coinsCount = _progress.currentCoinsCount.ToString("D2");
		_view.skullsCount = _progress.livesCount.ToString("D2");
	}
}