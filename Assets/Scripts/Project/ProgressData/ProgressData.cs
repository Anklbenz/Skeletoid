public class ProgressData
{
	public Wallet lives = new(2);
	public Wallet totalCoinsWallet = new(100);
	public Wallet currentCoinsWallet = new(0);
	public Wallet statsWallet = new(0);
	public Wallet keysWallet = new Wallet(2);
	public WorldData[] worldsInfo { get; set; }
	public LevelsHolder levelsHolder { get; set; }

	public long lastKeySpendTimeSeconds;
}