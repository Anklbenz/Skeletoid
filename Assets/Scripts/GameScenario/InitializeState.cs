using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeState : State {
	private readonly IGameObjectFactoryByPath _levelFactory;
	
    public InitializeState(StateSwitcher stateSwitcher, IGameObjectFactory  factory) : base(stateSwitcher) {
    }

    public override void Enter() {
	    
    }
    
    
}
