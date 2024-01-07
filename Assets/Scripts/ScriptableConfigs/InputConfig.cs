using UnityEngine;


[CreateAssetMenu(fileName = "keyboardConfig", menuName = "Configs/keyboardConfig")]
public class InputConfig : ScriptableObject {
	[Header("Keyboard")]
	[SerializeField] private KeyCode left;
	[SerializeField] private KeyCode leftExtra,
			right,
			rightExtra,
			shot,
			shotExtra;
	
	[Header("Touches")]
	[SerializeField] private float swipeUpMagnitude = 10;
	[SerializeField] private float swipeUpPermitAngle = 45;

	public float swipeRequiredLength => swipeUpMagnitude;
	public float swipePermitAngle =>  swipeUpPermitAngle;
	public KeyCode keyLeft => left;
	public KeyCode keyLeftExtra => leftExtra;
	public KeyCode keyRight => right;
	public KeyCode keyRightExtra => rightExtra;
	public KeyCode keyShot => shot;
	public KeyCode keyShotExtra => shotExtra;
}