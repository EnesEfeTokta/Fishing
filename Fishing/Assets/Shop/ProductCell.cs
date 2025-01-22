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

    private ShopPanel shopPanel;
    [HideInInspector] public ShowcaseProduct showcaseProduct;

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
        shopPanel.SelectProduct(showcaseProduct, gameObject);
    }

    public void ProductCellBackgroundColorChanged(Color color)
    {
        productBackground.color = color;
    }
}