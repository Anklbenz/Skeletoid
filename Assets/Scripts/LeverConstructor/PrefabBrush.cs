using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
[CreateAssetMenu (fileName = "GameObjectBrush", menuName = "Brushes/Brush")]
[CustomGridBrush(false,true,false,"GameObject brush")]
public class PrefabBrush : GameObjectBrush
{
}
