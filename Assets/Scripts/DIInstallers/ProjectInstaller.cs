using UnityEngine;
using Zenject;
public class ProjectInstaller : MonoInstaller {
	[SerializeField] private SceneIndexes scenesIndexes;
	public override void InstallBindings() {
		Container.Bind<ProjectDataStorage>().AsSingle();
		Container.Bind<SceneIndexes>().FromInstance(scenesIndexes).AsSingle();
		Container.Bind<SceneLoaderService>().AsSingle();
	}
}
