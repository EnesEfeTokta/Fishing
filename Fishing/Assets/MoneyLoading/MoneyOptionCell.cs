using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyOptionCell : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image moneyOptionImage; // Displays the image of the money option.
    [SerializeField] private TMP_Text moneyOptionName; // Displays the name of the money option.
    [SerializeField] private TMP_Text moneyOptionPrice; // Displays the price of the money option.

    private MoneyOption moneyOption; // Stores the current money option data.
    private MoneyOptionDetails moneyOptionDetails; // Reference to the MoneyOptionDetails component for showing product details.

    // Sets the money option information on this cell.
    public void ShowMoneyOption(MoneyOption moneyOption, MoneyOptionDetails moneyOptionDetails)
    {
        this.moneyOption = moneyOption; // Assign the money option data.

        // Update the UI with the money option details.
        moneyOptionImage.sprite = moneyOption.moneyOptionImage;
        moneyOptionName.text = moneyOption.moneyOptionName;
        moneyOptionPrice.text = $"${moneyOption.realMoneyPrice}";

        // Store the MoneyOptionDetails reference for future use.
        this.moneyOptionDetails = moneyOptionDetails;
    }

    // Opens the details of this product in the MoneyOptionDetails component.
    public void OpenYourProductDetails()
    {
        moneyOptionDetails.ShowMoneyOptionDetails(moneyOption);
    }
}