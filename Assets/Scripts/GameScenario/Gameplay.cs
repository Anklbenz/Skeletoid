using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gameplay 
{
  //ParticlesPlayer 
  private List<Brick> _bricks;
  private IInput _input;
  public Gameplay(Brick[] bricks, IInput input, Paddle paddle, IFactory ballFactory) {
    _bricks = bricks.ToList();
    
    
  }
   
}
