using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller {
	[SerializeField] private StorageConfig storageConfig;
	[SerializeField] private KeyboardConfig keyboardConfig; 

	public override void InstallBindings() {
		InstallFactory();
		InstallResourcesFactory();
		InstallInitialGameState();
		InstallPlayState();
		InstallGameScenario();
		InstallControls();
		Container.Bind<Paddle>().AsSingle();
	}
	private void InstallPlayState() {

		Container.Bind<PlayState>().AsSingle();
	}

	private void InstallControls() {
		Container.Bind<KeyboardConfig>().FromInstance(keyboardConfig).AsSingle();
		Container.Bind<IInput>().To<KeyboardInput>().AsSingle();
	}
	private void InstallGameScenario() {
		Container.Bind<GameScenario>().AsSingle().NonLazy();
	}

	private void InstallFactory() {
		Container.Bind<ResourcesLoader>().To<ResourcesLoader>().AsSingle();
		Container.Bind<IFactory>().To<Factory>().AsSingle();
	}
	
	private void InstallResourcesFactory() {
		Container.Bind<IResourcesFactory>().To<ResourcesFactory>().AsSingle();
	}
	
	private void InstallInitialGameState() {
		Container.Bind<StorageConfig>().FromInstance(storageConfig);
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<InitialState>().AsSingle();
	}
	
	public override void Start() {
		
	}
}