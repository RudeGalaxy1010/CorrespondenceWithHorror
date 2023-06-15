public class BalanceDisplayer : IInitable, IDeinitable
{
    private Balance _balance;
    private BalanceDisplayerEmitter _emitter;

    public BalanceDisplayer(Balance balance, BalanceDisplayerEmitter emitter)
    {
        _balance = balance;
        _emitter = emitter;
        UpdateBalance(_balance.Money);
    }

    public void Init()
    {
        _balance.Changed += OnBalanceChanged;
    }

    public void Deinit()
    {
        _balance.Changed -= OnBalanceChanged;
    }

    private void OnBalanceChanged(int money)
    {
        UpdateBalance(money);
    }

    private void UpdateBalance(int money)
    {
        _emitter.BalanceText.text = money.ToString();
    }
}
