using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BonusConfig", fileName = "BonusConfig", order = 0)]
public class BonusConfig : ScriptableObject {
	[SerializeField] private int threeHitCombo = 3;
}
