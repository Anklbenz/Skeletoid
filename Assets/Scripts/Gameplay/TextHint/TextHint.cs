using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextHint : AnimatedView {
	[SerializeField] private TMP_Text textField;
	[SerializeField] private int showTimeMilliseconds;
	
	public string text {
		set => textField.text = value;
	}

	public async void Show(string message) {
		textField.text = message;
		Open();
		await UniTask.Delay(showTimeMilliseconds);
		Close();
	}
}
