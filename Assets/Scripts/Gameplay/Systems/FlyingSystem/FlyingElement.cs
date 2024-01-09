using UnityEngine;

public class FlyingElement : MonoBehaviour
{
	public Vector3 position {
		get => transform.position;
		set => transform.position = value;
	}
}
