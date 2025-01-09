using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCell : MonoBehaviour
{
    [Header("Product Features UI")]
    [SerializeField] private Image productBackground; // UI element to display the product's background.
    [SerializeField] private Image productImage; // UI element to display the product's image.
    [SerializeField] private TMP_Text productName; // UI element to display the product's name.
    [SerializeField] private TMP_Text productPrice; // UI element to display the product's price.
    [SerializeField] private Button transactionButton; // Button to initiate product transactions (use or buy).
    /*
    [SerializeField] private TMP_Text productDescription; // UI element to display the product's description.
    [SerializeField] private TMP_Text productStatus; // UI element to show whether the product can be "Used" or "Bought".
    [SerializeField] private TMP_Text productReceivedDate; // UI element to display the date when the product was received.

    private ShowcaseProduct showcaseProduct; // Reference to the product data.
    private PlayerProgressData playerProgressData; // Reference to the player's progress data.
    private PlayerProgress playerProgress; // Reference to the player's overall progress.
    */

    private ShopPanel shopPanel;
    private ShowcaseProduct showcaseProduct;

    void Start()
    {
        // Add a listener to the transaction button that triggers the ProductSelect method when clicked.
        transactionButton.onClick.AddListener(ProductSelect);
    }

    public void SetProductCell(ShowcaseProduct showcaseProduct, ShopPanel shopPanel)
    {
        productImage.sprite = showcaseProduct.productImage;
        productName.text = showcaseProduct.productName;
        productPrice.text = showcaseProduct.productPrice.ToString();

        this.shopPanel = shopPanel;
        this.showcaseProduct = showcaseProduct;
    }

    void ProductSelect()
    {
        // If the product has been purchased, allow the player to use it.
        shopPanel.ProductSelect(showcaseProduct, gameObject);
    }

    public void ProductCellBackgroundColorChanged(Color color)
    {
        productBackground.color = color;
    }





    

/*
    void Start()
    {
        // Add a listener to the transaction button that triggers the ProductSelect method when clicked.
        transactionButton.onClick.AddListener(ProductSelect);

        // GameManager finds the object and reaches the shoppel.cs compensation.
        shopPanel = GameObject.Find("HomeManager").GetComponent<ShopPanel>();
    }

    // Method to handle what happens when the product is selected.
    void ProductSelect()
    {
        if (showcaseProduct.isPurchased)
        {
            // If the product has been purchased, allow the player to use it.
            ProductUse();
        }
        else
        {
            // If the product has not been purchased, attempt to buy it.
            ProductBuy();
        }

        // After interacting with the product, update the UI with the product's information.
        ShowProductInformation(showcaseProduct, playerProgress);
    }

    // Method to update the UI with product information.
    public void ShowProductInformation(ShowcaseProduct showcaseProduct, PlayerProgress playerProgress)
    {
        this.showcaseProduct = showcaseProduct; // Store the reference to the product.
        this.playerProgress = playerProgress; // Store the reference to the player's progress.
        playerProgressData = playerProgress.playerProgressData; // Get the player's progress data.

        // Update the product UI with the product's image, name, price, and description.
        productImage.sprite = showcaseProduct.productImage;
        productName.text = showcaseProduct.productName;
        productPrice.text = showcaseProduct.productPrice.ToString();
        productDescription.text = showcaseProduct.productDescription;

        // Check if the product is purchased.
        if (showcaseProduct.isPurchased)
        {
            // If purchased, set the status to "Use" and display the received date.
            productStatus.text = "Use";
            productReceivedDate.text = showcaseProduct.productReceivedDate;
        }
        else
        {
            // If not purchased, set the status to "Buy" and leave the received date empty.
            productStatus.text = "Buy";
            productReceivedDate.text = "";
        }
    }

    // Method to check if the player has enough money to buy the product.
    bool PaymentApproval()
    {
        int firstMoney = playerProgressData.totalPlayerMoney; // Get the player's current total money.
        int productPrice = showcaseProduct.productPrice; // Get the product's price.

        // Check if the player has enough money.
        if (firstMoney >= productPrice)
        {
            return true; // Payment approved.
        }
        else
        {
            return false; // Payment not approved.
        }
    }

    // Method to use the product (implementation depends on the game's functionality).
    void ProductUse()
    {
        // The product is being equipped or used here.
    }

    // Method to handle buying the product.
    void ProductBuy()
    {
        // Check if the player has enough money to purchase the product.
        if (PaymentApproval())
        {
            // Show a success message when payment is approved.
            ShowDebugMessage(
                "PAYMENT SUCCESSFUL", 
                "Your payment has been subtracted from your balance. You are ready to use.", 
                MessageStatus.LessImportant, 
                5
            );

            // Mark the product as purchased.
            showcaseProduct.StartPurchased();

            // Deduct the product's price from the player's total money.
            playerProgress.DecreasingMoney(showcaseProduct.productPrice);

            // The user's new total money is printed back.
            shopPanel.UpdateEconomicData();
        }
        else
        {
            // Show a failure message when the player doesn't have enough money.
            ShowDebugMessage(
                "PAYMENT FAILED", 
                "Your balance is not enough. Please increase your balance.", 
                MessageStatus.VeryImportant, 
                5
            );
        }
    }

    // Helper method to display messages using the global Message class.
    void ShowDebugMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        Message.NewMessage(messageName, messageDescription, messageStatus, showTime);
    }
*/
}