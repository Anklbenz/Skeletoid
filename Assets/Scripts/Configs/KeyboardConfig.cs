using UnityEngine;


[CreateAssetMenu(fileName = "keyboardConfig", menuName = "Configs/keyboardConfig")]
public class KeyboardConfig : ScriptableObject {
	[SerializeField] private KeyCode left,
			leftAdditional,
			right,
			rightAdditional,
			shot,
			shotAdditional;
}
