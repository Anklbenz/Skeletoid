using System.Linq;

public class InitialState : State {
	private readonly GameplaySystem _gameplaySystem;
	private readonly ParticlesService _particlesService;
	private readonly CoinService _coinService;
	private readonly LoseSystem _loseSystem;
	
	public InitialState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, LoseSystem loseSystem, ParticlesService particlesService, CoinService coinService) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_particlesService = particlesService;
		_coinService = coinService;
		_loseSystem = loseSystem;
	}

	public override void Enter() {
		Initialize();
		SwitchToGameplay();
	}
	private void Initialize() {
		_gameplaySystem.Initialize();
		_particlesService.Initialize();
		_coinService.Initialize();
		_loseSystem.Initialize();
	}

	private void SwitchToGameplay() {
		switcher.SetState<GameState>();
	}
}