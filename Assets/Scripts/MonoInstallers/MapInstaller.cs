using Cinemachine;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller, IInitializable
{
   [SerializeField] private MapItem[] items;
   [SerializeField] private MapHudView mapHudView;
   [SerializeField] private Transform particlesTransform;
   [SerializeField] private MapConfig mapConfig;
   [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
   [SerializeField] private CameraConfig cameraConfig;

   public override void InstallBindings() {
      InstallMapSystem();
      InstallInitializeForThis();
      InstallMainStat();
      InstallParticlesPlayer();
      InstallMapMove();
      InstallGameCameraSystem();
      InstallKeyRecovery();
   }
   private void InstallGameCameraSystem() {
      Container.Bind<CameraConfig>().FromInstance(cameraConfig);
      Container.Bind<CinemachineVirtualCamera>().FromInstance(cinemachineVirtualCamera);
      Container.Bind<CameraSystem>().AsSingle();
   }

   private void InstallMapMove() {
      Container.BindInterfacesAndSelfTo<MapKeyboardInput>().AsSingle();
      Container.Bind<MapCameraSystem>().AsSingle().NonLazy();
   }

   private void InstallParticlesPlayer() {
      Container.Bind<Transform>().FromInstance(particlesTransform);
      Container.Bind<MapConfig>().FromInstance(mapConfig);
      Container.Bind<MapParticlesPlayer>().AsSingle();
   }

   private void InstallMainStat() {
      Container.Bind<MapHudView>().FromInstance(mapHudView);
      Container.Bind<MapHud>().AsSingle();
   }

   private void InstallInitializeForThis() {
      Container.Bind<IInitializable>().FromInstance(this).AsSingle();
   }

   private void InstallMapSystem() {
      Container.Bind<WordMapSystem>().AsSingle();
   }

   private void InstallKeyRecovery() {
      Container.BindInterfacesAndSelfTo<KeysRecoverySystem>().AsSingle();
   }

   public void Initialize() {
      var cameraSystem = Container.Resolve<CameraSystem>();
      cameraSystem.Initialize(cinemachineVirtualCamera);
      
      var mapSystem = Container.Resolve<WordMapSystem>();
      mapSystem.Initialize(items);

     
   }
}