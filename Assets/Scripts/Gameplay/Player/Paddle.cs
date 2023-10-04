using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Paddle : MonoBehaviour, IPaddle
{
    private const float MAX_REFLECT_ANGLE = 70;
    private BoxCollider _boxCollider;
    private void Awake() {
        _boxCollider=GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.transform.TryGetComponent<IReflectObject>(out var reflectInstance)) return;

        var hitPoint = collision.contacts[0].point;
        var normal = collision.contacts[0].normal;

        var isFrontSideHit = Vector3.Dot(transform.forward, normal) < 0;

        if (!isFrontSideHit) {
            reflectInstance.Reflect(-normal);
            return;
        }

        var dir = GetDirectionDependsOnLocalPaddleHitPoint(hitPoint);
        reflectInstance.direction = dir;
    }

    private Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
        var colliderMinX = _boxCollider.center.x - _boxCollider.size.x / 2;
        var colliderMaxX = _boxCollider.center.x + _boxCollider.size.x / 2;

        var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
        var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
        var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

        return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
    }
}
