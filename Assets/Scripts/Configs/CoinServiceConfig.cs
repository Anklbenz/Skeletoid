using UnityEngine;


[CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/CoinServiceConfig")]
public class CoinServiceConfig: ScriptableObject {
	[SerializeField] private float sphereRadius;
	[SerializeField] private Vector3 sphereOffset;
	[SerializeField] private Vector2Int delayBeforeStartMovingMinMax;
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private float speed;

	public int delayBeforeStartMoving => Random.Range(delayBeforeStartMovingMinMax.x, delayBeforeStartMovingMinMax.y);
	public float coinSpeed => speed;
	public float radius => sphereRadius;
	public Vector3 offset => sphereOffset;
	public Coin prefab => coinPrefab;
}
