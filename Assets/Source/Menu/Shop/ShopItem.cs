using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public event Action<ShopItem> Selected;

    private const string AdsRequiredText = "За рекламу";
    private const string ChosenText = "Выбрана";

    [SerializeField] private Image _previewImage;
    [SerializeField] private TMP_Text _costText;
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

    public void Construct(Sprite preview, int cost, bool isPurchased, bool isChosen)
    {
        _previewImage.sprite = preview;
        _cost = cost;
        _isPurchased = isPurchased;
        _previewImage.gameObject.SetActive(isPurchased);

        if (_isPurchased == true && isChosen == true)
        {
            _costText.text = ChosenText;
            return;
        }

        if (_isPurchased == true)
        {
            _costText.text = string.Empty;
            return;
        }

        _costText.text = cost > 0 ? cost.ToString() : AdsRequiredText;
    }

    public bool IsPurchased => _isPurchased;
    public int Cost => _cost;
    public int AvatarId => int.Parse(_previewImage.sprite.name);

    private void OnSelectButtonClicked()
    {
        Selected?.Invoke(this);
    }
}
