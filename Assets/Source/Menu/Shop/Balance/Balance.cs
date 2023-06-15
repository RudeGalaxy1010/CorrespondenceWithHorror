using System;

public class Balance : IInitable, IDeinitable
{
    private const int RewardMoney = 200;
    private const string NegativeValueExceptionMessage = "Value can't be less than zero";

    public event Action<int> Changed;

    private PlayerData _playerData;
    private SaveLoad _saveLoad;
    private BalanceEmitter _balanceEmitter;

    private int _money;

    public Balance(PlayerData playerData, SaveLoad saveLoad, BalanceEmitter emitter)
    {
        _playerData = playerData;
        _saveLoad = saveLoad;
        _balanceEmitter = emitter;
        _money = _playerData.Money;
    }

    public int Money => _money;
    public bool Has(int value) => value <= _money;

    public void Init()
    {
        _balanceEmitter.AddMoneyButton.onClick.AddListener(OnAddMoneyButtonClicked);
    }

    public void Deinit()
    {
        _balanceEmitter.AddMoneyButton.onClick.RemoveListener(OnAddMoneyButtonClicked);
    }

    public void AddMoney(int value)
    {
        if (value < 0)
        {
            throw new Exception(NegativeValueExceptionMessage);
        }

        _money += value;
        _playerData.Money = _money;
        _saveLoad.SavePlayerData(_playerData);
        Changed?.Invoke(_money);
    }

    public bool TrySubtractMoney(int value)
    {
        if (value < 0)
        {
            throw new Exception(NegativeValueExceptionMessage);
        }

        if (value >= _money)
        {
            return false;
        }

        _money -= value;
        _playerData.Money = _money;
        _saveLoad.SavePlayerData(_playerData);
        Changed?.Invoke(_money);
        return true;
    }

    private void OnAddMoneyButtonClicked()
    {
        // TODO: Call Ad
        AddMoney(RewardMoney);
    }
}
