using UnityEngine;

[System.Serializable]
public class SkinSwitcher {
	[SerializeField] private GameObject[] skins;
	private int _currentIndex;
	public void MoveNext() {
		var hasNextElement = _currentIndex + 1 < skins.Length;
		if (!hasNextElement) return;
		HideCurrent();
		_currentIndex++;
		ShowNext();
	}
	private void ShowNext() =>
			skins[_currentIndex].SetActive(true);

	private void HideCurrent() =>
			skins[_currentIndex].SetActive(false);
}