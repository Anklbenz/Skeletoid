using UnityEngine;
using UnityEngine.Animations.Rigging;

public sealed class AnimationPlayer : MonoBehaviour {
	private const string ANIMATOR_KEY = "Direction";
	private static readonly int ANIMATION_HASH = Animator.StringToHash(ANIMATOR_KEY);

	[SerializeField] private Animator animator;
	[Header("Rig Aim Settings")]
	[SerializeField] private RigBuilder rigBuilder;
	[SerializeField] private MultiAimConstraint headMultiAimConstraint;
	[SerializeField] private MultiAimConstraint spineMultiAimConstraint;

	public void MoveDirection(float speed) =>
			animator.SetFloat(ANIMATION_HASH, speed);

	public void SetAimTarget(Transform target) {
		SetAimTargetTransform(headMultiAimConstraint,target);
		SetAimTargetTransform(spineMultiAimConstraint,target);
		rigBuilder.Build();
	}

	private void SetAimTargetTransform(MultiAimConstraint multiAimConstraint, Transform target) {
		var sourcesArray = multiAimConstraint.data.sourceObjects;
		sourcesArray.SetTransform(0, target);
		multiAimConstraint.data.sourceObjects = sourcesArray;
	}


}