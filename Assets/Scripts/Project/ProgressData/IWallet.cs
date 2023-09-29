public interface IWallet
{
    void Increase(int quantity = 1);
    bool Decrease(int quantity = 1);
}