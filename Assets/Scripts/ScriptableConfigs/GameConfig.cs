using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
	[SerializeField] private int maxKeysCount = 5;
   [SerializeField] private int startsCoinsCount = 500;
   [SerializeField] private int timeToIncreaseSeconds = 50;
   [SerializeField] private int keyPriceInGold = 300;
 //  [SerializeField] private int targetFrameRate;

   public int maxKeys => maxKeysCount;
   public int startCoins => startsCoinsCount;

   public int keyPrice => keyPriceInGold;
   public int keyIncreaseTime => timeToIncreaseSeconds;

}
