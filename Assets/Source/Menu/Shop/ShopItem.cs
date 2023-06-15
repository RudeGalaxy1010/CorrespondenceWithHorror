using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public event Action<ShopItem> Selected;

    [SerializeField] private Image _previewImage;
    [SerializeField] private Button _selectButton;

    private int _cost;
    private bool _isPurchased;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
    }

    public void Construct(Sprite preview, int cost, bool isPurchased)
    {
        _previewImage.sprite = preview;
        _cost = cost;
        _isPurchased = isPurchased;

        if (isPurchased == true)
        {
            _previewImage.gameObject.SetActive(true);
        }
    }

    public bool IsPurchased => _isPurchased;
    public int Cost => _cost;
    public int AvatarId => int.Parse(_previewImage.sprite.name);

    private void OnSelectButtonClicked()
    {
        Selected?.Invoke(this);
    }
}
