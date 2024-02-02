using UnityEngine;
public class GameMenuState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameMenu _gameMenu;
	private readonly ProgressSystem _progressSystem;

	public GameMenuState(StateSwitcher stateSwitcher, GameMenu gameMenu, ProgressSystem progressSystem) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_gameMenu = gameMenu;
		_progressSystem = progressSystem;

		_gameMenu.ContinueEvent += OnResume;
		_gameMenu.RestartEvent += OnRestart;
		_gameMenu.QuitEvent += OnQuit;
		_gameMenu.VolumeChangedEvent += OnVolumeChanged;
	}


	public override void Enter() {
		_gameMenu.Open();
	}

	private void OnResume() {
		_gameMenu.Close();
		_stateSwitcher.SetState<GameState>();
	}

	private void OnRestart() {
		//reset score
		_stateSwitcher.SetState<InitializeLevelState>();
		_gameMenu.Close();
	}

	private void OnVolumeChanged(float volume) {
		AudioListener.volume = volume;
		_progressSystem.SaveVolumeValue(volume);
	}

	private void OnQuit() {
		_progressSystem.ResetCurrentCoins();
		_stateSwitcher.SetState<FinalizeState>();
	}
}