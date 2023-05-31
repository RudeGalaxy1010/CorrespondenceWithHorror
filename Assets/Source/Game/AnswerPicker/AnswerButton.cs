using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    public event Action<Answer> Clicked;

    [SerializeField] private TMP_Text _text;

    private Button _button;
    private Answer _answer;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetAnswer(Answer answer)
    {
        _answer = answer;
        _text.text = _answer.Text;
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke(_answer);
    }
}
