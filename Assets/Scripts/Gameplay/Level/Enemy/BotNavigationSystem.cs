using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotNavigationSystem : IPauseSensitive {
	private const float PATH_REFRESH_RATE = 0.5f;
	private readonly Timer _timer;
	private List<Enemy> _enemies;
	private Bounds _navSurfaceBounds;

	public BotNavigationSystem(Timer timer) {
		_timer = timer;
	}

	public void Initialize(IEnumerable<Enemy> enemies, Bounds navSurfaceBounds) {
		_navSurfaceBounds = navSurfaceBounds;
		_enemies = enemies.ToList();
	}

	public void Start() {
		_timer.StartWithAlarm(PATH_REFRESH_RATE);
		_timer.AlarmEvent += RefreshPaths;
	}

	public void Stop() {
		_timer.Stop();
		_timer.AlarmEvent -= RefreshPaths;
	}

	private void RefreshPaths() {
		foreach (var enemy in _enemies) {
			if (!enemy.isPathComplete)
				continue;

			var target = GetRandomPointInBounds();
			enemy.SetNewDestination(target);
		}
	}

	private Vector3 GetRandomPointInBounds() {
		var x = Random.Range(_navSurfaceBounds.min.x, _navSurfaceBounds.max.x);
		var z = Random.Range(_navSurfaceBounds.min.z, _navSurfaceBounds.max.z);
		var y = _navSurfaceBounds.max.y;
		return new Vector3(x, y, z);
	}
	public void SetPause(bool isPaused) {
		if (isPaused)
			Start();
		else
			Stop();
	}
}