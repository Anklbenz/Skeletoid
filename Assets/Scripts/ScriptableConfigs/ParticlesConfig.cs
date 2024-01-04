using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticlesConfig", menuName = "Configs/ParticlesConfig")]
public class ParticlesConfig : ScriptableObject
{
	[SerializeField] private ParticleSystem sparkParticle;
    [SerializeField] private int sparkPoolAmount;
    [Space]
    [SerializeField] private ParticleSystem dustDarkParticles;
    [SerializeField] private int dustDarkPoolAmount;
    [Space]
    [SerializeField] private ParticleSystem dustBrightParticles;
    [SerializeField] private int dustBrightPoolAmount;
    [Space]
    [SerializeField] private ParticleSystem dustCircleParticles;
    [SerializeField] private int dustCirclePoolAmount;
    [Space]
    [SerializeField] private ParticleSystem waterParticles;
    [SerializeField] private int waterPoolAmount;
    [Space]
    [SerializeField] private ParticleSystem grenadeParticles;
    [SerializeField] private int grenadePoolAmount;
    [Space]
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private int firePoolAmount;
    [Space]
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private int damagePoolAmount;

    public int  damagePoolSize => damagePoolAmount;
    public int sparkPoolSize => sparkPoolAmount;
    public int dustDarkPoolSize => dustDarkPoolAmount;
    public int dustBrightPoolSize => dustBrightPoolAmount;
    public int dustCirclePoolSize => dustCirclePoolAmount;
    public int waterPoolSize => waterPoolAmount;
    public int grenadePoolSize => grenadePoolAmount;
    public int firePoolSize => firePoolAmount;
    
    public ParticleSystem sparkParticlePrefab => sparkParticle;
    public ParticleSystem dustDarkParticlesPrefab => dustDarkParticles;
    public ParticleSystem dustBrightParticlesPrefab => dustBrightParticles;
    public ParticleSystem dustCircleParticlesPrefab => dustCircleParticles;
    public ParticleSystem waterParticlesPrefab => waterParticles;
    public ParticleSystem grenadeParticlesPrefab => grenadeParticles;
    public ParticleSystem fireParticlesPrefab => fireParticles;
    public ParticleSystem damagePrefab => damageParticles;
}
