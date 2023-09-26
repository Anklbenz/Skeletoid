using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	public Transform paddleOrigin;
	public List<Brick> bricks = new();
	// public Vector3 paddlePosition => paddleOrigin.position;

	/*public event Action AllBricksDestroyedEvent;
//   public event Action<Vector3> BrickDestroyedEvent;
	
	public void Initialize() {
	  foreach (var brick in bricks)
	      brick.DestroyedEvent += OnBrickDestroy;
	}
	     
	private void OnBrickDestroy(Vector3 sender) {
		//BrickDestroyedEvent?.Invoke(sender.transform.position);
	         
	   if(bricks.Count <=0)
	      AllBricksDestroyedEvent?.Invoke();
	}*/
}
   