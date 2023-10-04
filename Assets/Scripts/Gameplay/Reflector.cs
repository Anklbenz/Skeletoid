using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class Reflector : MonoBehaviour
{
   private const float permissibleAngle = 20;
   private const float correctionStep = 3;


   private void OnCollisionStay(Collision collision) {
      Debug.Log(collision.gameObject.name);
      if (!collision.gameObject.TryGetComponent<IReflectObject>(out var reflectObject)) return;
      reflectObject.direction = GetReflectDirection(reflectObject.direction, collision.contacts[0].normal);
   }

   private Vector3 GetReflectDirection(Vector3 direction, Vector3 hitNormal) {
    var isOppositeDirection = Vector3.Dot(direction, hitNormal) > 0;
      if (!isOppositeDirection) return direction;

      var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);

      if (Mathf.Abs(collisionAngle) < permissibleAngle)
         direction = GetCorrectedDirection(direction, collisionAngle);

      var reflectDirection2D = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
      return new Vector3(reflectDirection2D.x, 0, reflectDirection2D.y);
   }

   private static Vector3 GetCorrectedDirection(Vector3 direction, float collisionAngle) {
//			Debug.Log($"CollisionAngle {collisionAngle} Correct {collisionAngle} ");
      return (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);
   }
}