using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    private const string Text = "Умножить";
    private const int DefaultMultiplier = 2;

    public event Action Clicked;

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    public int Multiplier => DefaultMultiplier;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void Start()
    {
        int multiplier = Multiplier;
        _text.text = $"{Text} x{multiplier}";
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke();
    }
}
