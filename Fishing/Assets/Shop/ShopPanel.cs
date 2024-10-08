using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    // Reference to the data container that holds all products for display in the shop.
    [Header("Showcase Products Data")]
    [SerializeField] private ShowcaseProductsData showcaseProductsData;

    // Prefab for individual product display (e.g., a UI element for each product).
    [Header("Product Object Prefab")]
    [SerializeField] private GameObject productPrefab;

    // The parent transform where product UI elements will be instantiated (e.g., a ScrollView content area).
    [Header("Parent")]
    [SerializeField] private Transform parent;

    // Reference to the shop panel game object (which can be opened or closed).
    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;

    // Boolean flag to track whether the panel is currently open or closed.
    private bool isOpenPanel = false;

    [Header("Player's Money Management")]
    [SerializeField] private PlayerProgressData playerProgressData;
    private int totalPlayerMoney;
    [SerializeField] private TMP_Text money;
    private PlayerProgress playerProgress;

    void Start()
    {
        playerProgress = GetComponent<PlayerProgress>();
    }

    // Method to open or close the shop panel.
    public void PanelOpenClose(bool isOpen)
    {
        // Enable or disable the settings panel based on the boolean parameter.
        settingsPanel.SetActive(isOpen);
        isOpenPanel = isOpen;

        // Display the products in the shop when the panel is opened.
        ShowProduct();

        totalPlayerMoney = ReadingMoney(playerProgressData);
        money.text = totalPlayerMoney.ToString();
    }

    int ReadingMoney(PlayerProgressData playerProgressData)
    {
        return playerProgressData.totalPlayerMoney;
    }

    // Method to instantiate product UI elements and populate them with product data.
    void ShowProduct()
    {
        // Loop through all products defined in the showcaseProductsData.
        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            // Instantiate a product prefab as a child of the parent transform.
            ProductCell productCell = Instantiate(productPrefab, parent).GetComponent<ProductCell>();

            // Call the method on the product cell to populate the UI with product information.
            productCell.ShowProductInformation(showcaseProduct, playerProgress);
        }
    }
}