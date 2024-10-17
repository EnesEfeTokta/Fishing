using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyOptionCell : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image moneyOptionImage;
    [SerializeField] private TMP_Text moneyOptionName;
    [SerializeField] private TMP_Text moneyOptionPrice;

    public void ShowMoneyOption(MoneyOption moneyOption)
    {
        moneyOptionImage.sprite = moneyOption.moneyOptionImage;
        moneyOptionName.text = moneyOption.moneyOptionName;
        moneyOptionPrice.text = moneyOption.moneyOptionPrice.ToString();
    }
}
