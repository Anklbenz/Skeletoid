using UnityEngine;


[CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/CoinServiceConfig")]
public class FlyingCoinsConfig : ScriptableObject {
	[Header("Sphere")]
	[SerializeField] private float sphereRadius;
	[SerializeField] private Vector3 sphereOffset;
	
	[Header("Circle")]
	[SerializeField] private Vector2 circleOffset;
	[SerializeField] private Vector2Int delayBeforeStartMovingMinMax;
	[SerializeField] private Vector2Int delayBeforeAppearMinMax = new Vector2Int(0, 300);

	[Header("Common")]
	[SerializeField] private FlyingElement flyingElementPrefab;
	[SerializeField] private float speed;
//	[SerializeField] private Vector3 prefabScaleValue = Vector3.one;
	[SerializeField] private int poolAmount = 5;
	[SerializeField] private string itemsParentGameObjectName = "CoinsParent";

	public string itemsParentName => itemsParentGameObjectName;
	public int itemsPoolAmount => poolAmount;
//	public Vector3 prefabScale => prefabScaleValue;
	public int delayBeforeStartMoving => Random.Range(delayBeforeStartMovingMinMax.x, delayBeforeStartMovingMinMax.y);
	public int delayBeforeAppear => Random.Range(delayBeforeAppearMinMax.x, delayBeforeAppearMinMax.y);
	public float coinSpeed => speed;
	public float radius => sphereRadius;
	public Vector3 offsetInSphere => sphereOffset;
	public Vector2 offsetInCircle => circleOffset;
	public FlyingElement prefab => flyingElementPrefab;
}