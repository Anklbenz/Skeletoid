using UnityEngine;

public sealed class PaddleAnimationPlayer : MonoBehaviour
{
    private const string ANIMATOR_KEY = "Direction";
    private static readonly int ANIMATION_HASH = Animator.StringToHash(ANIMATOR_KEY);

    [SerializeField] private Animator animator;
    [SerializeField] private Paddle paddle;

    private void Awake() {
        paddle.OnMoveEvent += OnMove;
    }
    private void OnMove(float speed) =>
        animator.SetFloat(ANIMATION_HASH, speed);
}