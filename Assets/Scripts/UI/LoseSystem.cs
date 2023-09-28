using System;
using UnityEngine;
public class LoseSystem {
	private readonly ResultUiConfig _config;
	private readonly IFactory _factory;
	private readonly ProgressData _progressData;
	private LoseView _loseView;

	public event Action RestartEvent, QuitEvent;

	public LoseSystem(ResultUiConfig config, ProgressData progressData, IFactory factory) {
		_factory = factory;
		_config = config;
		_progressData = progressData;
	}

	public void Initialize() {
		var canvasParent = _factory.Create<Canvas>(_config.canvasPrefab).transform;
		_loseView = _factory.Create(_config.loseViewPrefab, canvasParent);
		_loseView.ForceClose();

		_loseView.RestartEvent += OnRestart;
	}
	private void OnRestart() {
		throw new System.NotImplementedException();
	}

	public void OnLose() {
		var livesCount = --_progressData.livesCount;
		if (livesCount <= 0) {
			_loseView.Set($"Жизней {livesCount} Посмотри ролик");
			_loseView.Open();

			_loseView.Close();
		}
	}
}