public class MapHud
{
	private readonly ProgressSystem _progressSystem;
	private MapHudView _view;

	public MapHud(ProgressSystem progressSystem, MapHudView view) {
		_progressSystem = progressSystem;
		_view = view;
	}
	
	public void Refresh() {
		_view.coinsCount = ShowWithSymbol(_progressSystem.totalCoinsCount);
		_view.livesCount = $"{_progressSystem.livesCount:D2}";
		_view.starsCount = $"{_progressSystem.starsCount:D2}";
	}

	private string ShowWithSymbol(int count) =>
		 count < 1000 ? $"{count:D2}" : $"{ (count / 1000).ToString()}k";
	
}
