using Cinemachine;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller, IInitializable
{
   [SerializeField] private MapItem[] items;
   [SerializeField] private MainStatsView mainStatsView;
   [SerializeField] private Transform particlesTransform;
   [SerializeField] private MapConfig mapConfig;
   [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

   public override void InstallBindings() {
      InstallMapSystem();
      InstallInitializeForThis();
      InstallMainStat();
      InstallParticlesPlayer();
      InstallMapMove();
   }

   private void InstallMapMove() {
      Container.BindInterfacesAndSelfTo<MapKeyboardInput>().AsSingle();
      Container.Bind<CinemachineVirtualCamera>().FromInstance(cinemachineVirtualCamera);
      Container.Bind<MapCameraSystem>().AsSingle().NonLazy();
   }

   private void InstallParticlesPlayer() {
      Container.Bind<Transform>().FromInstance(particlesTransform);
      Container.Bind<MapConfig>().FromInstance(mapConfig);
      Container.Bind<MapParticlesPlayer>().AsSingle();
   }

   private void InstallMainStat() {
      Container.Bind<MainStatsView>().FromInstance(mainStatsView);
      Container.Bind<MainStats>().AsSingle();
   }

   private void InstallInitializeForThis() {
      Container.Bind<IInitializable>().FromInstance(this).AsSingle();
   }

   private void InstallMapSystem() {
      Container.Bind<WordMapSystem>().AsSingle();
   }

   public void Initialize() {
      
      var mapSystem = Container.Resolve<WordMapSystem>();
      mapSystem.Initialize(items);
      
   }
}