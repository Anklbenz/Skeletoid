using System;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private const float MAX_REFLECT_ANGLE = 70;
    
    [SerializeField] private BoxCollider boxCollider;
    private Vector3 _collisionPoint;
    private bool _onBoard;
    private IThrowObject _throwObjectObject;

    public void Throw() {
        if(!_onBoard) return;
        var throwDirection = GetDirectionDependsOnLocalPaddleHitPoint(_collisionPoint);
           _throwObjectObject.Throw(throwDirection * 1000);
            
    }
    
    private void OnCollisionStay(Collision collisionInfo) {
        if(!collisionInfo.gameObject.TryGetComponent<IThrowObject>(out var throwObject)) return;
        _onBoard = true;
        _throwObjectObject = throwObject;
        _collisionPoint = collisionInfo.contacts[0].point;
    }
    
    private void OnCollisionExit(Collision collisionInfo) {
        if(!collisionInfo.gameObject.TryGetComponent<IThrowObject>(out var throwObject)) return;
        _onBoard = false;
    }

    private Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
        var colliderMinX = boxCollider.center.x - boxCollider.size.x / 2;
        var colliderMaxX = boxCollider.center.x + boxCollider.size.x / 2;

        var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
        var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
        var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

        return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
    }
}
