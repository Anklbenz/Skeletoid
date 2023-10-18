using System;
using UnityEngine;

public interface IMapInput
{
	event Action<Vector3> DraggingEvent;
	event Action StartDragEvent, StopDragEvent;
}