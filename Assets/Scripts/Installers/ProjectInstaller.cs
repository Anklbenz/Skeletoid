using UnityEngine;
using Zenject;
public class ProjectInstaller : MonoInstaller {
	[SerializeField] private SceneIndexesStorage scenesIndexesStorage;
	public override void InstallBindings() {
		Container.Bind<ProjectDataStorage>().AsSingle();
		Container.Bind<SceneIndexesStorage>().FromInstance(scenesIndexesStorage).AsSingle();
		Container.Bind<SceneLoaderService>().AsSingle();
	}
}
