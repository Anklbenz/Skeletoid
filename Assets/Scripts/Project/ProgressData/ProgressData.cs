public class ProgressData
{
	public readonly Wallet lives = new(0);
	public readonly Wallet totalCoinsWallet = new(100);
	public readonly Wallet currentCoinsWallet = new(0);
	public readonly Wallet statsWallet = new(0);
	public readonly Wallet keysWallet = new (2);
	public WorldData[] worldsInfo { get; set; }
	public long lastKeySpendTimeSeconds;
}