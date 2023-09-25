using System;
using UnityEngine;

    public class Ball : Motor, IReflect
    {
        [SerializeField] private int damage;
        [SerializeField] private float permissibleAngle, correctionStep;
        public Action<Vector3> OnCollisionEvent;
        private Vector3 direction { get; set; }
        public float magnitude;

        /*private bool _move;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                direction = transform.forward;
                _move = true;
            }
        }*/

        private void FixedUpdate() {
         //   if (!_move) return;

            Move(direction);
            magnitude = direction.magnitude;
        }

        private void OnCollisionEnter(Collision collision) {
            OnCollisionEvent?.Invoke(collision.contacts[0].point);

            var paddle = collision.transform.GetComponent<Paddle>();
            if (paddle != null) return;
            Reflect(collision.contacts[0].normal);

            TryCauseDamage(collision.gameObject);
        }

        private void TryCauseDamage(GameObject collision) {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.Hit(damage);
        }

        public void Reflect(Vector3 hitNormal) {
            var isOppositeDirection = Vector3.Dot(direction, hitNormal) < 0;
            if (!isOppositeDirection) return;

            var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);

            if (Mathf.Abs(collisionAngle) < permissibleAngle) {
                Debug.Log($"Correct {collisionAngle} ");
                direction = (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);
            }

            var direction2 = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
            direction = new Vector3(direction2.x, 0, direction2.y);
        }
        
        public void SetDirection(Vector3 dir) {
            direction = dir;
        }
    }
