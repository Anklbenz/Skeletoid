using UnityEngine;

[CreateAssetMenu(menuName = "Configs/TextDrawerConfig", fileName = "TextDrawerConfig", order = 0)]
public class TextDrawerConfig : ScriptableObject
{
	[SerializeField] private TextHint textHint;
	[SerializeField] private int poolSize = 3;
	[SerializeField] private int showTime;
	public TextHint textHintPrefab => textHint;
	public int textHintPoolSize => poolSize;
	public int showTimeMilliseconds => showTime;

}
