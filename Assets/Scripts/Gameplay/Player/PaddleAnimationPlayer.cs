using UnityEngine;

public sealed class PaddleAnimationPlayer : MonoBehaviour
{
    private const string ANIMATOR_KEY = "Direction";
    private static readonly int ANIMATION_HASH = Animator.StringToHash(ANIMATOR_KEY);

    [SerializeField] private Animator animator;

    public void MoveDirection(float speed) =>
        animator.SetFloat(ANIMATION_HASH, speed);
}