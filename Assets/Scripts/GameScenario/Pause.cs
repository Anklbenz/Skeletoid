using System;

public class Pause {
	public event Action ContinueEvent, RestartEvent, QuitEvent;
	private PauseView _view;
	public void Initialize(PauseView view) {
		_view = view;
		_view.ContinueEvent += OnContinueSelected;
		_view.RestartEvent += OnRestartSelected;
		_view.QuitEvent += OnQuitSelected;
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
}