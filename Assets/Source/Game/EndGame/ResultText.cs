using TMPro;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    private const string VictoryText = "Победа";
    private const string DefeatText = "Провал";
    private const string VictoryColor = "FFEB60";
    private const string DefeatColor = "FF6100";

    [SerializeField] private TMP_Text _resultText;

    public void SetFromResult(GameResult result)
    {
        SetText(result == GameResult.Victory ? VictoryText : DefeatText);
        SetColor(result == GameResult.Victory ? GetColorFromString(VictoryColor) : GetColorFromString(DefeatColor));
    }

    private void SetText(string text)
    {
        _resultText.text = text;
    }

    private void SetColor(Color color)
    {
        _resultText.color = color;
    }

    private Color GetColorFromString(string hexadecimalString)
    {
        if (ColorUtility.TryParseHtmlString(hexadecimalString, out Color newColor))
        {
            return newColor;
        }
        else
        {
            return default;
        }
    }
}
