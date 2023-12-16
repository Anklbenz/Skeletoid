using System;
public class Win
{
	private readonly ProgressSystem _progressData;
	private WinView _view;

	public event Action ContinueEvent;

	public Win(ProgressSystem progressData) {
		_progressData = progressData;
	}

	public void Initialize(WinView view) {
		_view = view;
		_view.ForceClose();
		_view.ContinueEvent += OnContinue;
	}

	public void OnWin(int starsCount, TimeSpan levelTime) {
		_view.ShowStarsCount(starsCount);
		_view.levelTime =  $"{levelTime.Minutes:00}:{levelTime.Seconds:00}.{levelTime.Milliseconds:000}";
		_view.Open();
	}

	private void OnContinue() {
		_view.Close();
		ContinueEvent?.Invoke();
	}
}