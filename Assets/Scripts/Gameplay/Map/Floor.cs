using UnityEngine;

public class Floor : MonoBehaviour {
	//needs only for build navMeshSurface
	public MeshRenderer meshRenderer;
	public bool isMeshRenderEnabled {
		set => meshRenderer.enabled = value;
	}
	public Bounds floorBounds => meshRenderer.bounds;
}