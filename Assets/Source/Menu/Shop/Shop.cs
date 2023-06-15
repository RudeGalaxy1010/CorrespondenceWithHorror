using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class Shop : IInitable, IDeinitable
{
    private const int CostMultiplier = 100;
    private const int AdItemsOrderNumber = 3;
    private const string OpenedAvatarsText = "Открыто";

    public event Action AvatarUpdated;

    private PlayerData _playerData;
    private SaveLoad _saveLoad;
    private Balance _balance;
    private Sprite[] _avatars;
    private ShopEmitter _emitter;

    private List<ShopItem> _items;
    private ShopItem _selectedItem;

    public Shop(PlayerData playerData, SaveLoad saveLoad, Balance balance, Sprite[] avatars, ShopEmitter emitter)
    {
        _saveLoad = saveLoad;
        _playerData = playerData;
        _balance = balance;
        _avatars = avatars;
        _emitter = emitter;

        ResetPurchaseInfo();
        CreateOrUpdateItems();
    }

    public void Init()
    {
        _emitter.ShopOpenButton.onClick.AddListener(OnShopOpenButtonClicked);
        _emitter.ShopCloseButton.onClick.AddListener(OnShopCloseButtonClicked);
        _emitter.PurchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    public void Deinit()
    {
        _emitter.ShopOpenButton.onClick.RemoveListener(OnShopOpenButtonClicked);
        _emitter.ShopCloseButton.onClick.RemoveListener(OnShopCloseButtonClicked);
        _emitter.PurchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
    }

    private void OnShopOpenButtonClicked()
    {
        _emitter.ShopPanel.SetActive(true);
    }

    private void OnShopCloseButtonClicked()
    {
        ResetPurchaseInfo();
        _emitter.ShopPanel.SetActive(false);
    }

    private void CreateOrUpdateItems()
    {
        ClearItems();
        CreateItems();
        _emitter.OpenedAvatarsText.text = $"{OpenedAvatarsText} {_playerData.OpenedAvatarIds.Length}/{_avatars.Length}";
    }

    private void ClearItems()
    {
        if (_items == null || _items.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            Object.Destroy(_items[i].gameObject);
        }

        _items.Clear();
    }

    private void CreateItems()
    {
        _items = new List<ShopItem>();

        for (int i = 0; i < _avatars.Length; i++)
        {
            ShopItem item = Object.Instantiate(_emitter.ShopItemPrefab, _emitter.ItemsContainer);
            int avatarId = int.Parse(_avatars[i].name);
            bool isAvatarOpened = _playerData.OpenedAvatarIds.Contains(avatarId);
            int cost = i * CostMultiplier;
            item.Construct(_avatars[i], cost, isAvatarOpened);
            item.Selected += OnItemSelected;
            _items.Add(item);
        }
    }

    private void OnItemSelected(ShopItem item)
    {
        if (item.IsPurchased == true)
        {
            ApplyAvatar(item.AvatarId);
            _saveLoad.SavePlayerData(_playerData);
            return;
        }

        _selectedItem = item;
        CostType costType = GetCostType(_selectedItem);

        if (costType == CostType.Money)
        {
            ResetPurchaseInfo();
            _emitter.MoneyCostView.gameObject.SetActive(true);
            _emitter.MoneyCostView.SetCost(item.Cost);
            _emitter.PurchaseButton.interactable = _balance.Has(item.Cost);
        }
        else if (costType == CostType.Ad)
        {
            ResetPurchaseInfo();
            _emitter.AdCostView.gameObject.SetActive(true);
            _emitter.PurchaseButton.interactable = true;
        }
    }

    private void OnPurchaseButtonClicked()
    {
        CostType costType = GetCostType(_selectedItem);

        if (costType == CostType.Money)
        {
            // TOOD: subtract money
        }
        else if (costType == CostType.Ad)
        {
            // TODO: show Ad
        }

        _playerData.OpenedAvatarIds = _playerData.OpenedAvatarIds.Append(_selectedItem.AvatarId).ToArray();
        ApplyAvatar(_selectedItem.AvatarId);
        _saveLoad.SavePlayerData(_playerData);
        CreateOrUpdateItems();
        ResetPurchaseInfo();
    }

    private void ResetPurchaseInfo()
    {
        _emitter.AdCostView.gameObject.SetActive(false);
        _emitter.MoneyCostView.gameObject.SetActive(false);
        _emitter.PurchaseButton.interactable = false;
    }

    private CostType GetCostType(ShopItem shopItem)
    {
        // Every AdItemsOrderNumber item for Ad
        return (_items.IndexOf(shopItem) + 1) % AdItemsOrderNumber == 0 ? CostType.Ad : CostType.Money;
    }

    private void ApplyAvatar(int avatarId)
    {
        _playerData.CurrentAvatarId = avatarId;
        AvatarUpdated?.Invoke();
    }
}
