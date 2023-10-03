using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameplayConfig gameplayConfig;
	[SerializeField] private UiFactoryConfig uiFactoryConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	[SerializeField] private CoinServiceConfig coinsConfig;

	public override void InstallBindings() {
		InstallProgressData();
		InstallDiFactory();
		InstallUiFactory();
		InstallLevelFactory();
		Container.Bind<PauseUiSystem>().AsSingle();
		Container.Bind<PauseHandler>().AsSingle();
		InstallGameplaySystem();
		InstallLoseSystem();
		InstallWinSystem();
		InstallHudSystem();
		InstallParticlesService();
		InstallFlyingCoinService();
		InstallGameScenario();
		InstallInitializeForThis();
		
	}
	private void InstallHudSystem() =>
			Container.BindInterfacesAndSelfTo<HudSystem>().AsSingle();

	private void InstallLevelFactory() {
		Container.Bind<GameplayConfig>().FromInstance(gameplayConfig).AsSingle();
		Container.Bind<LevelFactory>().AsSingle();
	}
	private void InstallUiFactory() {
		Container.Bind<UiFactoryConfig>().FromInstance(uiFactoryConfig).AsSingle();
		Container.Bind<UiFactory>().AsSingle();
	}

	private void InstallProgressData() =>
			Container.Bind<ProgressData>().AsSingle();

	private void InstallLoseSystem() =>
			Container.Bind<LoseSystem>().AsSingle();

	private void InstallWinSystem() =>
			Container.Bind<WinSystem>().AsSingle();

	private void InstallFlyingCoinService() {
		Container.Bind<CoinServiceConfig>().FromInstance(coinsConfig).AsSingle();
		Container.BindInterfacesAndSelfTo<FlyingCoinService>().AsSingle();
	}

	private void InstallGameplaySystem() =>
			Container.Bind<GameplaySystem>().AsSingle();

	private void InstallParticlesService() {
		Container.Bind<ParticlesConfig>().FromInstance(particlesConfig);
		Container.Bind<ParticlesService>().AsSingle();
	}

	private void InstallInitializeForThis() =>
			Container.Bind<IInitializable>().FromInstance(this).AsSingle();

	private void InstallGameScenario() {
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<InitialState>().AsSingle();
		Container.Bind<InitializeLevelState>().AsSingle();
		Container.Bind<PauseState>().AsSingle();
		Container.Bind<GameState>().AsSingle();
		Container.Bind<LoseState>().AsSingle();
		Container.Bind<WinState>().AsSingle();
		Container.Bind<GameScenario>().AsSingle();
	}

	private void InstallDiFactory() =>
			Container.Bind<IFactory>().To<DiFactory>().AsSingle();

	public void Initialize() {
		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}