using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MoneyOptionDetails : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text selectedProductName; // Displays the name of the selected product.
    [SerializeField] private TMP_Text selectedProductPrice; // Displays the price of the selected product.

    [Header("Money Feature Cell")]
    [SerializeField] private Transform parentMoneyFeatureCell; // Parent transform for displaying product feature cells.
    [SerializeField] private GameObject moneyOptionFeatureCellPrefab; // Prefab for product feature cells.

    private List<GameObject> showingMoneyOptionDetails = new List<GameObject>(); // List to track currently displayed feature cells.

    // Shows the details of the selected money option, including its features.
    public void ShowMoneyOptionDetails(MoneyOption moneyOption)
    {
        // Destroy all previously displayed feature cells to avoid duplication.
        foreach (GameObject showingMoneyOptionDetail in showingMoneyOptionDetails)
        {
            Destroy(showingMoneyOptionDetail);
        }

        // Clear the list after destroying old elements.
        showingMoneyOptionDetails.Clear();

        // Set the name and price of the selected product.
        selectedProductName.text = moneyOption.moneyOptionName;
        selectedProductPrice.text = $"Buy for ${moneyOption.realMoneyPrice}";

        // Display each feature of the selected money option.
        foreach (MoneyOptionFeature moneyOptionFeature in moneyOption.moneyOptionFeature)
        {
            // Instantiate a new feature cell and set its parent.
            MoneyOptionDetailsCell moneyOptionDetailsCell = Instantiate(moneyOptionFeatureCellPrefab, parentMoneyFeatureCell).GetComponent<MoneyOptionDetailsCell>();

            // Set the details of the feature on the instantiated cell.
            moneyOptionDetailsCell.SetMoneyOptionDetails(moneyOptionFeature.moneyOptionFeatureName, moneyOptionFeature.moneyOptionFeatureIcon, moneyOptionFeature.moneyOptionFeaturePrice);

            // Add the created cell to the tracking list.
            showingMoneyOptionDetails.Add(moneyOptionDetailsCell.gameObject);
        }
    }
}