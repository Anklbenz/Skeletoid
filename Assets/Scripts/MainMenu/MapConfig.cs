using UnityEngine;

[CreateAssetMenu(menuName = "Configs/MapConfig", fileName = "MapConfig")]
public class MapConfig : ScriptableObject
{
    public MapItem[] items;
}
