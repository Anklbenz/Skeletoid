using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

public class AdvSDK {
	public Reward reward;
	public void Initialize(YandexGame yandexGame) {
		reward = new Reward(yandexGame);
	}
}