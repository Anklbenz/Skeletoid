using System;
using UnityEngine;

public interface ICombo
{
	public event Action<Vector3, int> ComboEvent;
}