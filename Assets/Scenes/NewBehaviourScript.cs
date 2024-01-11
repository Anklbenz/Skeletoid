using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour {
	[SerializeField] private NavMeshSurface navMeshSurface;
	[SerializeField] private NavMeshAgent navMeshAgent;

	private void Start() {
		navMeshSurface.BuildNavMesh();
		Debug.Log(navMeshSurface.size);
		Debug.Log("Center "+navMeshSurface.center);
		//NavMesh.
	}
}
