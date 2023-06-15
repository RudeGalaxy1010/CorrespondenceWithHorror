using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEmitter : MonoBehaviour
{
    public GameObject ShopPanel;
    public Button ShopOpenButton;
    public Button ShopCloseButton;
    public TMP_Text OpenedAvatarsText;
    public ShopItem ShopItemPrefab;
    public Transform ItemsContainer;
    public MoneyCostView MoneyCostView;
    public GameObject AdCostView;
    public Button PurchaseButton;
}
