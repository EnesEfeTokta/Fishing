using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyOptionDetailsCell : MonoBehaviour
{
    [SerializeField] private Image moneyOptionDetailsImage; // UI image to display the feature's icon.
    [SerializeField] private TMP_Text moneyOptionDetailsName; // UI text to display the feature's name.

    // Sets the details for the money option feature in the cell.
    public void SetMoneyOptionDetails(string moneyOptionDetailsName, Sprite moneyOptionDetailsImage)
    {
        // Update the UI with the provided feature name and icon.
        this.moneyOptionDetailsName.text = moneyOptionDetailsName;
        this.moneyOptionDetailsImage.sprite = moneyOptionDetailsImage;
    }
}