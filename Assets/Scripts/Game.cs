using UnityEngine;

public class Game {
	private ParticlesPlayer particlesPlayer;
	private Level levelPrefab;
	private Paddle paddlePrefab;
	private Ball ballPrefab;
	private Controller controller;

	private Paddle _paddle;
	private Level _level;
	private Ball _ball;

	private void Awake() {
		/*_level = Instantiate(levelPrefab);
		_level.Initialize();

		_paddle = Instantiate(paddlePrefab, _level.paddleOrigin);
		_paddle.Initialize(controller);

		_ball = Instantiate(ballPrefab, _paddle.ballTransform);
		_level.BrickDestroyedEvent += OnBrickDestroyed;
		_level.AllBricksDestroyedEvent += OnAllBricksDestroyed;*/

		particlesPlayer.Initialize();
	}

	private void OnBrickDestroyed(Vector3 destroyPosition) {
		particlesPlayer.PlayDestroy(destroyPosition);
	}

	private void OnAllBricksDestroyed() {
		Debug.Log("Is Over");
	}
}
