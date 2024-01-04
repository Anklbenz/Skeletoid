using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BonusConfig", fileName = "BonusConfig", order = 0)]
public class BonusConfig : ScriptableObject {
//	[SerializeField] private int threeHitCombo = 3;
	[SerializeField] private float backWallTime = 10;

	public float backWallActiveTime => backWallTime;
}
