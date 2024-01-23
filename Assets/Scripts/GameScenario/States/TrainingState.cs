using Cysharp.Threading.Tasks;

public class TrainingState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly HudSystem _hudSystem;

	public TrainingState(StateSwitcher stateSwitcher, HudSystem hudSystem) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_hudSystem = hudSystem;
	}

	public override async void Enter() =>
			await PlayTraining();

	private async UniTask PlayTraining() {
		await _hudSystem.AwaitPlayTraining();
		_stateSwitcher.SetState<GameState>();
	}
}