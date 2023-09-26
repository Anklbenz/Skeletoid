using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameObjectsConfig gameObjectsConfig;
	
	public override void InstallBindings() {
		InstallFactory();
	//	InstallInitialGameState();
	//	InstallPlayState();
		InstallGameScenario();
		InstallInitializeForThis();
	}
	private void InstallInitializeForThis() =>
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();

	private void InstallGameScenario() {
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<CurrentGameplayData>().AsSingle();
		Container.Bind<BallSystem>().AsSingle();
		Container.Bind<GameObjectsConfig>().FromInstance(gameObjectsConfig);
		Container.Bind<InitialState>().AsSingle();
		Container.Bind<GameplayState>().AsSingle();
		Container.Bind<GameScenario>().AsSingle();
	}
	
	private void InstallFactory() =>
		Container.Bind<IFactory>().To<Factory>().AsSingle();

	
	public void Initialize() {
		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}