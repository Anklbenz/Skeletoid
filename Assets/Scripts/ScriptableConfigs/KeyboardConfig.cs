using UnityEngine;


[CreateAssetMenu(fileName = "keyboardConfig", menuName = "Configs/keyboardConfig")]
public class KeyboardConfig : ScriptableObject {
	[SerializeField] private KeyCode left,
			leftExtra,
			right,
			rightExtra,
			shot,
			shotExtra;

	public KeyCode keyLeft => left;
	public KeyCode keyLeftExtra => leftExtra;
	public KeyCode keyRight => right;
	public KeyCode keyRightExtra => rightExtra;
	public KeyCode keyShot => shot;
	public KeyCode keyShotExtra => shotExtra;
}