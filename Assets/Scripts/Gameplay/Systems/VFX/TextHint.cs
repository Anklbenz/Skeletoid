using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TextHint : AnimatedView {
	[SerializeField] private TMP_Text textField;
	
	public async void Show(string message, int showTime) {
		textField.text = message;
		Open();
		await UniTask.Delay(showTime);
		Close();
	}
}
