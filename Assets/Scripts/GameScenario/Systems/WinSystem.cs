using System;
using UnityEngine;

public class WinSystem
{
	private readonly IFactory _factory;
	private readonly LoseConfig _config; //winPrebaf
	private readonly ProgressData _progressData;
	private WinView _winView;
	private WinView _winViewPrefab;

	public event Action ContinueEvent;

	public WinSystem(LoseConfig config, WinView prefab, ProgressData progressData, IFactory factory) {
		_factory = factory;
		_config = config;
		_winViewPrefab = prefab;
		_progressData = progressData;
	}

	public void Initialize() {
		var canvasParent = _factory.Create<Canvas>(_config.canvasPrefab).transform;
		_winView = _factory.Create(_winViewPrefab, canvasParent);
		var rectTransform = ((RectTransform)_winView.transform);
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		_winView.ForceClose();
		_winView.ContinueEvent += OnContinue;
	}

	public void OnWin() {
		_winView.SetCoinsCount(_progressData.currentCoins.count);
		_winView.Open();
	}

	private void OnContinue() {
		_winView.Close();
		ContinueEvent?.Invoke();
	}
}
