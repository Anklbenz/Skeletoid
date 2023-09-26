using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public event Action<Vector3> DeadZoneReachedEvent; 
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponentInParent<Ball>())
            DeadZoneReachedEvent?.Invoke(Vector3.zero);
    }
}
