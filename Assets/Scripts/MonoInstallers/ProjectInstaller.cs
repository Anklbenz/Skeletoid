using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
	[SerializeField] private SceneIndexes scenesIndexes;
	[SerializeField] private KeyboardConfig keyboardConfig;
	[SerializeField] private WorldsConfig worldsConfig;

	public override void InstallBindings() {
		InstallSceneLoader();
		InstallProjectStorage();
		InstallControls();
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
}