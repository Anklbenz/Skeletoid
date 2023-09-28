using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WinView : AnimatedView
{
	[SerializeField] private Button continueButton;
	[SerializeField] private TMP_Text someStatText;
	public event Action ContinueEvent;
   
	public void Set(string info) {
		someStatText.text = info;
	}

	private void Awake() {
		continueButton.onClick.AddListener(OnContinueClick);
	}
	
	private void OnContinueClick() =>
			ContinueEvent?.Invoke();
}
