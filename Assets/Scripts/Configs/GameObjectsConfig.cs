using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class GameObjectsConfig : ScriptableObject {
	[SerializeField] private Map[] map;
	[SerializeField] private Ball ball;
	[SerializeField] private Paddle paddle;
	[Header("Particles")]
	[SerializeField] private ParticleSystem hitParticles;
	[SerializeField] private ParticleSystem destroyParticles;

	public ParticleSystem hitParticlesPrefab => hitParticles;
	public ParticleSystem destroyParticlesPrefab => destroyParticles;
	public Ball ballPrefab => ball;
	public Paddle paddlePrefab => paddle;
	public Map GetMapPrefab(int index) =>
			map[index];
}
