using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WordListConfig", fileName = "WordListConfig")]
public class WordListConfig : ScriptableObject
{
   [SerializeField] private WordMapItem items;
   
}
