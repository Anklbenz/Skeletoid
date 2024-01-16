using UnityEngine;

public class NavMap : MonoBehaviour
{
	public MeshRenderer meshRenderer;
	public bool isMeshRenderEnabled {
		set => meshRenderer.enabled = value;
	}
	public Bounds floorBounds => meshRenderer.bounds;
}
