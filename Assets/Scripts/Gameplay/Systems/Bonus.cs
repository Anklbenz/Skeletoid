using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus {
	private Level _level;

	public void Start(Level level) {
		
	}
	public void Stop() {
		
	}
	
	private void Subscribe() {
		foreach (var brick in _level.bricks) {
//			brick.NoLivesLeft += OnBrickDestroy;
		}
//		_level.player.HitOnPaddleEvent += OnPaddleHit;

	}

	private void UnSubscribe() {
		foreach (var brick in _level.bricks) {
	//		brick.NoLivesLeft -= OnBrickDestroy;
		}
	//	_level.player.HitOnPaddleEvent -= OnPaddleHit;
	}
}
