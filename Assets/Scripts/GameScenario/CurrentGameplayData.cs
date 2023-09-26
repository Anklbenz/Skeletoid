using System.Collections.Generic;
using UnityEngine;

public class CurrentGameplayData {
	public List<Brick> bricks { get; set; }
	public Ball ball{ get; set; }
	public Paddle paddle{ get; set; }
	public Environment environment{ get; set; } 
	public Transform ballOrigin{ get; set; }
	
	public DeadZone deadZone { get; set; }
}
