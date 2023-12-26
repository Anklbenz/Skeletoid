using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BonusConfig", fileName = "BonusConfig", order = 0)]
public class BonusConfig : ScriptableObject {
	[SerializeField] private int threeHitCombo = 3;
	[SerializeField] private int backWallActiveTimeMilliseconds = 3000;

	public int backWallActiveTime => backWallActiveTimeMilliseconds;
}
