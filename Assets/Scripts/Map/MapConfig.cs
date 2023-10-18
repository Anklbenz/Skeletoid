using UnityEngine;

[CreateAssetMenu(menuName = "Configs/MapConfig", fileName = "MapConfig")]
public class MapConfig : ScriptableObject
{
   
    [SerializeField] private ParticleSystem levelUnlockedParticles;

    public ParticleSystem unlockParticles => levelUnlockedParticles;

}
