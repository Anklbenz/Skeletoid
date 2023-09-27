using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/CoinServiceConfig")]
public class CoinServiceConfig: ScriptableObject {
	[SerializeField] private float sphereRadius;
	[SerializeField] private Vector3 sphereOffset;
	[SerializeField] private Coin coinPrefab;

	public float radius => sphereRadius;
	public Vector3 offset => sphereOffset;
	public Coin prefab => coinPrefab;
}
