using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class KeySpendView : AnimatedView {
	[SerializeField] private int appearDelay = 550;
	[SerializeField] private int disappearDelay = 1000;
	[SerializeField] private TMP_Text totalCountText;
	[SerializeField] private GameObject spendCountObject;

	public async UniTask Show(int countBefore, int countAfter) {
		Open();

		totalCountText.text = countBefore.ToString("D2");

		await UniTask.Delay(appearDelay);
		totalCountText.text = countAfter.ToString("D2");

		//Because if user close panel, when delay in awaiting, after await give null reference
		if (spendCountObject == null) return;

		spendCountObject.SetActive(true);
		await UniTask.Delay(disappearDelay);

		//Because if user close panel, when delay in awaiting, after await give null reference
		if (spendCountObject == null) return;
		spendCountObject.SetActive(false);
	}
}