using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Ensures the component "MoneyOptionDetails" is attached to the GameObject this script is attached to.
[RequireComponent(typeof(MoneyOptionDetails))]
public class MoneyLoadingPanel : MonoBehaviour
{
    [Header("Money Loading Data")]
    [SerializeField] private MoneyLoadingData moneyLoadingData; // Holds data related to available money options.
    [SerializeField] private PlayerProgressData playerProgressData; // Holds player-related data.
    [SerializeField] private TMP_Text money; // Displays the player's total money.

    [Header("Money Option Cell")]
    [SerializeField] private Transform parentMoneyOptionCell; // Parent transform where money option cells will be instantiated.
    [SerializeField] private GameObject moneyOptionCellPrefab; // Prefab for individual money option cells.

    [Header("Panel")]
    [SerializeField] private GameObject moneyLoadingPanel; // The panel to display money-loading options.

    private MoneyOptionDetails moneyOptionDetails; // Stores a reference to the MoneyOptionDetails component.

    private List<MoneyOptionCell> showingMoneyOptionCells = new List<MoneyOptionCell>(); // Keeps track of instantiated money option cells.

    private int totalPlayerMoney; // Stores the player's total money.

    void Start()
    {
        // Initializes the MoneyOptionDetails component.
        moneyOptionDetails = GetComponent<MoneyOptionDetails>();
    }

    // Opens or closes the money loading panel based on the boolean value passed.
    public void PanelOpenClose(bool isOpen)
    {
        moneyLoadingPanel.SetActive(isOpen); // Sets the panel's active state.

        // If the panel is opened, generate the sale covers.
        if (isOpen)
        {
            SaleCorversAreProduced();
            UpdateEconomicData();
        }
    }

    // Updates the player's economic data.
    public void UpdateEconomicData()
    {
        totalPlayerMoney = playerProgressData.totalPlayerMoney;
        money.text = totalPlayerMoney.ToString();
    }

    // Generates and displays money option cells dynamically.
    void SaleCorversAreProduced()
    {
        // Loop through all money options defined in the data.
        foreach (MoneyOption moneyOption in moneyLoadingData.moneyOptions)
        {
            // Instantiate a money option cell and set its parent.
            MoneyOptionCell moneyOptionCell = Instantiate(moneyOptionCellPrefab, parentMoneyOptionCell).GetComponent<MoneyOptionCell>();

            // Show the money option data in the instantiated cell.
            moneyOptionCell.ShowMoneyOption(moneyOption, moneyOptionDetails);

            // Add the created cell to the list for tracking.
            showingMoneyOptionCells.Add(moneyOptionCell);
        }

        // Automatically open the first product's details.
        showingMoneyOptionCells[0].OpenYourProductDetails();
    }

    void PaymentCompletion(int addMoney)
    {
        // The player's total money is updated.
        playerProgressData.totalPlayerMoney += addMoney;
        UpdateEconomicData();
        
        Message.Instance.NewMessage(
            "SUCCESSFUL PAYMENT", 
            "Your payment has been verified.", 
            MessageStatus.LessImportant, 
            5
        );
    }
}