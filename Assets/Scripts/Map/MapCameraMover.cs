using Cinemachine;
using UnityEngine;

public class MapCameraMover
{
   private const float speedMultipier = 0.3f;
   private readonly CinemachineVirtualCamera _camera;
   private Vector3 _startDragPosition;

   public MapCameraMover(CinemachineVirtualCamera camera, IMapInput mapInput) {
      _camera = camera;

      mapInput.DraggingEvent += OnDragging;
      mapInput.StopDragEvent += OnStopDrag;
   }

   private void OnStopDrag() =>
      _camera.transform.position = _camera.State.CorrectedPosition;

   private void OnDragging(Vector3 offset) {
      _camera.transform.position = _camera.State.CorrectedPosition;
      _camera.transform.position += offset*speedMultipier;
   }
}
