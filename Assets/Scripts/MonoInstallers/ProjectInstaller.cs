using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller, IInitializable
{
	[SerializeField] private SceneIndexes scenesIndexes;
	[SerializeField] private KeyboardConfig keyboardConfig;
	[SerializeField] private WorldsConfig worldsConfig;

	public override void InstallBindings() {
		InstallSceneLoader();
		InstallProjectStorage();
		InstallControls();
		InstallInitializeForThis();
		InstallProgressDataSystem();
	}

	private void InstallInitializeForThis() {
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();
	}

	private void InstallControls() {
		Container.Bind<KeyboardConfig>().FromInstance(keyboardConfig).AsSingle();
		Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
	}

	private void InstallProjectStorage() {
		Container.Bind<WorldsConfig>().FromInstance(worldsConfig).AsSingle();
		Container.Bind<ProgressData>().AsSingle();
	}

	private void InstallSceneLoader() {
		Container.Bind<SceneIndexes>().FromInstance(scenesIndexes).AsSingle();
		Container.Bind<SceneLoaderService>().AsSingle();
	}

	private void InstallProgressDataSystem() =>
		Container.Bind<ProgressDataInitializeSystem>().AsSingle();

	public void Initialize() {
		var progressDataInitializeSystem = Container.Resolve<ProgressDataInitializeSystem>();
		progressDataInitializeSystem.Initialize();
	}
}