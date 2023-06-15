using TMPro;
using UnityEngine;

public class MoneyCostView : MonoBehaviour
{
    [SerializeField] private TMP_Text _costText;

    public void SetCost(int cost)
    {
        _costText.text = cost.ToString();
    }
}
