using System;
using UnityEngine;

public interface IComboEvent
{
	public event Action<Vector3, int> ComboEvent;
}