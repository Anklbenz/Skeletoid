using System;
using UnityEngine;

public class GameMenu {
	public event Action<float> VolumeChangedEvent; 
	public event Action ContinueEvent, RestartEvent, QuitEvent;
	private PauseView _view;

	public void Initialize(PauseView view) {
		_view = view;
		_view.ContinueEvent += OnContinueSelected;
		_view.RestartEvent += OnRestartSelected;
		_view.QuitEvent += OnQuitSelected;
		_view.VolumeChangedEvent += OnVolumeChanged;
	}

	public void Open() =>
			_view.Open();

	public void Close() =>
			_view.Close();

	private void OnContinueSelected() =>
			ContinueEvent?.Invoke();

	private void OnQuitSelected() =>
			QuitEvent?.Invoke();

	private void OnRestartSelected() =>
			RestartEvent?.Invoke();
	private void OnVolumeChanged(float volume) =>
			VolumeChangedEvent?.Invoke(volume);
}