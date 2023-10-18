public class MainStats
{
	private readonly ProgressSystem _progressSystem;
	private MainStatsView _view;

	public MainStats(ProgressSystem progressSystem, MainStatsView view) {
		_progressSystem = progressSystem;
		_view = view;
	}
	
	public void Refresh() {
		_view.coinsCount = $"{_progressSystem.totalCoinsCount:D2}";
		_view.livesCount = $"{_progressSystem.livesCount:D2}";
		_view.starsCount = $"{_progressSystem.starsCount:D2}";
	}
}
