using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticlesConfig", menuName = "Configs/ParticlesConfig")]
public class ParticlesConfig : ScriptableObject
{
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem destroyParticles;
    
    public ParticleSystem hitParticlesPrefab => hitParticles;
    public ParticleSystem destroyParticlesPrefab => destroyParticles;
}
