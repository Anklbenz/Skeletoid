using UnityEngine;
public class HudSystem {
	private readonly ProgressData _progress;
	private HudView _view;

	
	public HudSystem(ProgressData progress) {
		_progress = progress;
	}
	public void Initialize(HudView view) {
		_view = view;
		_view.PauseClickedEvent += OnPauseClicked;
		Refresh();
	}

	public void SetActive(bool isActive) {
		if(isActive)
			_view.Open();
		else 
			_view.Close();
	}
	
	private void OnPauseClicked() {
		Debug.Log("PauseClicked");
	}

	public void Refresh() {
		_view.coinsCount = _progress.currentCoins.count.ToString("D2");
		_view.skullsCount = _progress.lives.count.ToString("D2");
	}
}