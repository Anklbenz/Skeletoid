using UnityEngine;

public sealed class Paddle : MonoBehaviour, IPaddle
{
    [SerializeField] private BoxCollider boxCollider;
    private const float MAX_REFLECT_ANGLE = 70;
    
    // Hit in opposite direction
    public bool CouldSpecialReflectionBePerformed(Vector3 hitPoint, Vector3 hitNormal) {
        return Vector3.Dot(transform.forward, hitNormal) < 0;
    }

    public Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
        var colliderMinX = boxCollider.center.x - boxCollider.size.x / 2;
        var colliderMaxX = boxCollider.center.x + boxCollider.size.x / 2;

        var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
        var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
        var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

        return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
    }
}