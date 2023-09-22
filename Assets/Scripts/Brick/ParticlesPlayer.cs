using UnityEngine;

[System.Serializable]
    public class ParticlesPlayer 
    {
        [SerializeField] private ParticleSystem destroyPrefab, onCollisionPrefab;
        [SerializeField] private Transform parent;
        private ParticleSystem _boxDestroyParticle, _onBallCollisionParticle;
        public void Initialize() {
            _boxDestroyParticle = Object.Instantiate(destroyPrefab, parent);
            _onBallCollisionParticle = Object.Instantiate(onCollisionPrefab, parent);
        }
  
        public void BallCollisionPlay(Vector3 position) {
            PlayOnPosition(_onBallCollisionParticle, position);
        }
        
        public void PlayDestroy(Vector3 position) {
            PlayOnPosition(_boxDestroyParticle, position);
        }
        
        public void PlayOnPosition(ParticleSystem particle, Vector3 position) {
            particle.transform.position = position;
            particle.Play();
        }
    }
