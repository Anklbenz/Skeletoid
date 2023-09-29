public class Wallet : IWallet
{
    private int _count;
    public int count => _count;

    public Wallet(int startCount) {
        _count = startCount;
    }

    public void Increase(int quantity = 1) =>
        _count += quantity;

    public bool Decrease(int quantity = 1) {
        var balance = _count - quantity;
        if (balance < 0) return false;
        _count = balance;
        return true;
    }
}