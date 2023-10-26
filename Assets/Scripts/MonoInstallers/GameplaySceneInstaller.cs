using Cinemachine;
using UnityEngine;
using Zenject;

public sealed class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameplayConfig gameplayConfig;
	[SerializeField] private CameraConfig cameraConfig;
	[SerializeField] private UiFactoryConfig uiFactoryConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	[SerializeField] private FlyingCoinsConfig coinsConfig;

	public override void InstallBindings() {
		//InstallProgressDataSystem();
		InstallDiFactory();
		InstallUiFactory();
		InstallLevelFactory();
		InstallPause();
		InstallGameplaySystem();
		InstallLoseSystem();
		InstallWinSystem();
		InstallHudSystem();
		InstallParticlesService();
		InstallFlyingCoinService();
		InstallGameScenario();
		InstallInitializeForThis();
		InstallGameCameraSystem();
		InstallTimer();
	}

	private void InstallTimer() {
		Container.BindInterfacesAndSelfTo<Timer>().AsSingle();
		Container.Bind<RewardSystem>().AsSingle();
	}

	private void InstallGameCameraSystem() {
		Container.Bind<CameraConfig>().FromInstance(cameraConfig);
		Container.Bind<CameraSystem>().AsSingle();
	}

	private void InstallPause() {
		Container.Bind<PauseUiSystem>().AsSingle();
		Container.Bind<PauseHandler>().AsSingle();
	}

	private void InstallHudSystem() =>
			Container.BindInterfacesAndSelfTo<HudSystem>().AsSingle();

	private void InstallLevelFactory() {
	//	Container.Bind<WorldsConfig>().FromInstance(worldsConfig).AsSingle();
		Container.Bind<GameplayConfig>().FromInstance(gameplayConfig).AsSingle();
		Container.Bind<LevelFactory>().AsSingle();
	}
	private void InstallUiFactory() {
		Container.Bind<UiFactoryConfig>().FromInstance(uiFactoryConfig).AsSingle();
		Container.Bind<UiFactory>().AsSingle();
	}

	private void InstallLoseSystem() =>
			Container.Bind<LoseSystem>().AsSingle();

	private void InstallWinSystem() =>
			Container.Bind<WinSystem>().AsSingle();

	private void InstallFlyingCoinService() {
		Container.Bind<FlyingCoinsConfig>().FromInstance(coinsConfig).AsSingle();
		Container.BindInterfacesAndSelfTo<FlyingCoinService>().AsSingle();
	}

	private void InstallGameplaySystem() {
		Container.Bind<BallLaunchSystem>().AsSingle();
		Container.BindInterfacesAndSelfTo<GameplaySystem>().AsSingle();
	}

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