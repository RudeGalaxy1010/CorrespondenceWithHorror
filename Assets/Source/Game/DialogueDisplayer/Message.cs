using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Display(string text)
    {
        _text.text = text;
    }
}
