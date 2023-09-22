using UnityEngine;

[System.Serializable]
public class BallManager
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private ParticlesPlayer particlesPlayerPlayer;
    [SerializeField] private Transform activeTransform;
    private Ball _ball;

    public void Initialize(Transform idleTransform) {
        _ball = Object.Instantiate(ballPrefab, idleTransform);
        _ball.OnCollisionEvent += OnBallCollision;
        particlesPlayerPlayer.Initialize();
    }

    public void Throw(Vector3 direction) {
        _ball.transform.SetParent(activeTransform);
        _ball.SetDirection(direction);
        _ball.enabled = true;
    }

    private void OnBallCollision(Vector3 position) {
        particlesPlayerPlayer.BallCollisionPlay(position);
    }
}