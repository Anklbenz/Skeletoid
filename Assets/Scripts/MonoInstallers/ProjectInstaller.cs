using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller, IInitializable 
{
	[SerializeField] private SceneIndexes scenesIndexes;
	[SerializeField] private KeyboardConfig keyboardConfig;
	[SerializeField] private WorldsConfig worldsConfig;
	[SerializeField] private GameConfig gameConfig;
	[SerializeField] private Canvas projectCanvasPrefab;
	[SerializeField] private AnimatedView sceneTransitionsView;

	public override void InstallBindings() {
		InstallSceneLoader();
		InstallProjectStorage();
		InstallControls();
		InstallProgressDataSystem();
		InstallLivesRecoveryTimer();
		InstallInitializableForThis();
	}
	private void InstallInitializableForThis() {
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
		Container.Bind<SceneLoader>().AsSingle();
	}

	private void InstallProgressDataSystem() =>
		Container.Bind<ProgressSystem>().AsSingle().NonLazy();

	private void InstallLivesRecoveryTimer() {
		Container.Bind<GameConfig>().FromInstance(gameConfig);
		Container.BindInterfacesAndSelfTo<KeysRecoverySystem>().AsSingle();
	}
	public void Initialize() {
		//	var popupCanvas = Container.InstantiatePrefabForComponent<Canvas>(projectCanvasPrefab, this.transform);
	//	var 
	}
}