using System;using UnityEngine;public interface IBackWall {	public event Action<Vector3> StoneWallActivateEvent, StoneWallDeactivateEvent; }