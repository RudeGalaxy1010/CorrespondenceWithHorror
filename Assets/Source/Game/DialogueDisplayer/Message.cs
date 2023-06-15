using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _avatarImage;

    public void Display(Sprite avatar, string text)
    {
        _avatarImage.sprite = avatar;
        _text.text = text;
    }
}
