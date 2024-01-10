using Cinemachine;
using UnityEngine;
using Zenject;

public sealed class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameplayConfig gameplayConfig;
	[SerializeField] private BonusConfig bonusConfig;
	[SerializeField] private CameraZoomConfig cameraZoomConfig;
	[SerializeField] private CameraShakeConfig cameraShakeConfig;
	[SerializeField] private UiFactoryConfig uiFactoryConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	[SerializeField] private FlyingCoinsConfig coinsConfig;
	[SerializeField] private TextDrawerConfig textDrawerConfig;
	[SerializeField] private InputConfig inputConfig;
	[SerializeField] private CinemachineVirtualCamera mainCamera;
	[SerializeField] private CinemachineVirtualCamera zoomedCamera;

	public override void InstallBindings() {
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
		InstallStarsTimer();
		InstallBonus();
		InstallControls();
	}
	private void InstallControls() {
		Container.Bind<InputConfig>().FromInstance(inputConfig).AsSingle();
		Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
		//Container.BindInterfacesAndSelfTo<SensorInput>().AsSingle();
	}
	private void InstallTimer() {
		//
		var tickableTracker = new TickableTracker();
		//Container.BindInterfacesAndSelfTo<TickableTracker>().FromInstance(tickableTracker).AsSingle();
		Container.BindInterfacesAndSelfTo<TickableTracker>().FromInstance(tickableTracker).AsSingle();
		Container.BindInterfacesAndSelfTo<Timer>().AsTransient().OnInstantiated<Timer>((ctx, instance) => tickableTracker.Track(instance));
	}

	private void InstallStarsTimer() {
		Container.BindInterfacesAndSelfTo<StarsTimer>().AsSingle();
	}

	private void InstallGameCameraSystem() {
		Container.Bind<CameraZoomConfig>().FromInstance(cameraZoomConfig);
		Container.Bind<CameraZoom>().AsSingle();

		Container.Bind<CameraShakeConfig>().FromInstance(cameraShakeConfig);
		Container.Bind<CameraShaker>().AsSingle();
	}

	private void InstallPause() {
		Container.Bind<Pause>().AsSingle();
		Container.Bind<PauseHandler>().AsSingle();
	}

	private void InstallHudSystem() {
		Container.BindInterfacesAndSelfTo<GameplayHint>().AsSingle();
		Container.BindInterfacesAndSelfTo<HudSystem>().AsSingle();
	}

	private void InstallLevelFactory() {
		Container.Bind<GameplayConfig>().FromInstance(gameplayConfig).AsSingle();
		Container.Bind<LevelFactory>().AsSingle();
	}
	private void InstallUiFactory() {
		Container.Bind<UiFactoryConfig>().FromInstance(uiFactoryConfig).AsSingle();
		Container.Bind<UiFactory>().AsSingle();
	}

	private void InstallLoseSystem() =>
			Container.Bind<Lose>().AsSingle();

	private void InstallWinSystem() =>
			Container.Bind<Win>().AsSingle();

	private void InstallFlyingCoinService() {
		Container.Bind<FlyingCoinsConfig>().FromInstance(coinsConfig).AsSingle();
		Container.BindInterfacesAndSelfTo<FlyingService>().AsSingle();
	}

	private void InstallGameplaySystem() {
		Container.Bind<BallLaunch>().AsSingle();
		Container.Bind<BallSpeed>().AsSingle();
		Container.BindInterfacesAndSelfTo<Gameplay>().AsSingle();
	}

	private void InstallParticlesService() {
		Container.Bind<ParticlesConfig>().FromInstance(particlesConfig);
		Container.Bind<ParticlesPlayer>().AsSingle();
		Container.Bind<TextDrawer>().AsSingle();
		Container.Bind<TextDrawerConfig>().FromInstance(textDrawerConfig);
		Container.Bind<LevelVfx>().AsSingle();
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
		Container.Bind<FinalizeState>().AsSingle();

		Container.Bind<GameScenario>().AsSingle();
	}

	private void InstallBonus() {
		Container.BindInterfacesAndSelfTo<BonusSystem>().AsSingle();
		Container.Bind<BonusConfig>().FromInstance(bonusConfig);
		Container.BindInterfacesAndSelfTo<LevelEventsHandler>().AsSingle();
	}

	private void InstallDiFactory() =>
			Container.Bind<IFactory>().To<DiFactory>().AsSingle();

	public void Initialize() {
		var cameraShaker = Container.Resolve<CameraShaker>();
		cameraShaker.InitializeCamera(mainCamera);

		var cameraZoom = Container.Resolve<CameraZoom>();
		cameraZoom.InitializeCameras(mainCamera, zoomedCamera);

		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}