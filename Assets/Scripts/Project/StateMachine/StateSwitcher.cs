using System;
using System.Collections.Generic;

public class StateSwitcher 
{
   private State current { get; set; }
   private Type currentType { get; set; }
   private readonly Dictionary<Type, State> _states = new();
   
   public void AddState(State state) =>
	   _states.Add(state.GetType(), state);
   
   public void SetState<T>() where T : State {
	   var type = typeof(T);

	   if (type == currentType || !_states.TryGetValue(type, out var state))
		   return;

	   currentType = type;
	   current?.Exit();
	   current = state;
	   current.Enter();
   }

   public void Update() =>
	   current?.Update();
}
