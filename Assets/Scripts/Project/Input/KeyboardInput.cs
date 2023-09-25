using System;
using Zenject;

public class KeyboardInput : IInput, ITickable
{
	public event Action Left;
	public event Action Right;
	public event Action Shoot;

			//private 
	public KeyboardInput() {
		
	}
	
	public void Tick() {
		
	}
}
