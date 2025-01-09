using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Option Buttons")]
    [SerializeField] private Button objectButton; // Button to display spear-related products.
    [SerializeField] private Button materialButton; // Button to display coatings-related products.

    [Header("Ads Panel")]
    [SerializeField] private Transform adsPanelParent;
    [SerializeField] private GameObject adsProductPrefab;
    [SerializeField] private AdsProductsData adsProductsData;
    private List<Sprite> adsProducts = new List<Sprite>();

    [Header("Product Showcase")]
    [SerializeField] private TMP_Text productName; // UI element to display the product's name.
    [SerializeField] private TMP_Text productPrice; // UI element to display the product's price.
    [SerializeField] private TMP_Text productDescription; // UI element to display the product's description.
    [SerializeField] private TMP_Text productStatus; // UI element to show whether the product can be "Used" or "Bought".
    [SerializeField] private TMP_Text productReceivedDate; // UI element to display the date when the product was received.
    [SerializeField] private TMP_Text productSelectIndex; // UI element to display the index of the product selected.

    private List<GameObject> showingProducts = new List<GameObject>(); // List to keep track of displayed products.
    private int selectedProductIndex = 0; // Index of the currently selected product.

    [Header("Platform")]
    [SerializeField] private Transform spearPlatformObjectTransform;

    void Start()
    {
        // Initialize the player progress data component.
        playerProgress = GetComponent<PlayerProgress>();

        StartCoroutine(ShowAdsProduct());

        // Show all products by default when the game starts.
        ShowAllProduct();

        ProductSelect(showcaseProductsData.showcaseProducts[selectedProductIndex], showingProducts[selectedProductIndex]);
    }

    public void OnObjectButtonClick()
    {
        // Destroy all currently displayed products to avoid duplication.
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }

        // Show only the products that are of type "Object".
        ShowProduct(ProductType.Object);

        // Set button interactability: Disable the spear button to prevent multiple clicks.
        objectButton.interactable = false;
        materialButton.interactable = true;
    }

    public void OnMaterialButtonClick()
    {
        // Destroy all currently displayed products to avoid duplication.
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }

        // Show only the products that are of type "Material".
        ShowProduct(ProductType.Material);

        // Set button interactability: Disable the coatings button to prevent multiple clicks.
        materialButton.interactable = false;
        objectButton.interactable = true;
    }

    // Method to open or close the shop panel.
    public void PanelOpenClose(bool isOpen)
    {
        // Enable or disable the settings panel based on the boolean parameter.
        settingsPanel.SetActive(isOpen);
        isOpenPanel = isOpen;

        // It is inspected that the panel is open.
        if (isOpen)
        {
            // Display spear-related products by default when the panel opens.
            OnObjectButtonClick();
        }

        StartCoroutine(ShowAdsProduct());

        // Update the visual representation of the player's current money.
        UpdateEconomicData();
    }

    // Economic data is updated.
    public void UpdateEconomicData()
    {
        // The current amount of money is taken.
        totalPlayerMoney = ReadingMoney(playerProgressData);
        // Current money is printed.
        money.text = totalPlayerMoney.ToString();
    }

    // The total amount of money of the user is pulled and then returned.
    int ReadingMoney(PlayerProgressData playerProgressData)
    {
        return playerProgressData.totalPlayerMoney;
    }

    // Method to instantiate product UI elements and populate them with product data.
    void ShowAllProduct()
    {
        // Pre-manufactured and listed products are cleaned.
        showingProducts.Clear();

        foreach (Transform oldProduct in parent)
        {
            Destroy(oldProduct.gameObject);
        }

        // Loop through all products defined in the showcaseProductsData.
        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            // Instantiate a new product UI element as a child of the specified parent.
            GameObject newProduct = Instantiate(productPrefab, parent);

            newProduct.GetComponent<ProductCell>().SetProductCell(showcaseProduct, this);
            showingProducts.Add(newProduct);
        }
    }

    void ShowProduct(ProductType productType)
    {
        // Pre-manufactured and listed products are cleaned.
        showingProducts.Clear();

        // Destroy all currently displayed products to avoid duplication.
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }

        // Loop through all products defined in the showcaseProductsData.
        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            // Check if the product type matches the specified productType.
            if (showcaseProduct.productType == productType)
            {
                // Instantiate a new product UI element as a child of the specified parent.
                GameObject newProduct = Instantiate(productPrefab, parent);

                newProduct.GetComponent<ProductCell>().SetProductCell(showcaseProduct, this);
                showingProducts.Add(newProduct);
            }
        }
    }

    public void ActionButtonClick(bool direction)
    {
        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.white);

        if (direction)
        {
            selectedProductIndex = (selectedProductIndex + 1) % showingProducts.Count;
        }
        else
        {
            selectedProductIndex = (selectedProductIndex - 1 + showingProducts.Count) % showingProducts.Count;
        }

        ProductSelect(showcaseProductsData.showcaseProducts[selectedProductIndex], showingProducts[selectedProductIndex]);

        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.green);
    }

    void ProductBuy()
    {
        // The product is purchased.
    }

    public void ProductSelect(ShowcaseProduct showcaseProduct, GameObject product)
    {
        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.white);

        // The product is selected.
        selectedProductIndex = showingProducts.IndexOf(product);
        productSelectIndex.text = $"Selected Index: {selectedProductIndex + 1}";
        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.green);

        // Set product UI elements with the provided data.
        productName.text = showcaseProduct.productName;
        productPrice.text = $"{showcaseProduct.productPrice}$";
        productDescription.text = showcaseProduct.productDescription;
        productStatus.text = showcaseProduct.isPurchased ? "Use" : "Buy";
        productReceivedDate.text = showcaseProduct.productReceivedDate != null ? showcaseProduct.productReceivedDate : "--.--.----";

        PatformChanged(showcaseProduct.productType);
    }

    void PatformChanged(ProductType productType)
    {
        switch (productType)
        {
            case ProductType.Object:
                ObjectChanged(showcaseProductsData.showcaseProducts[selectedProductIndex].spear);
                break;
            case ProductType.Material:
                ObjectChanged(playerProgressData.selectSpearObject);
                MaterialChanged(showcaseProductsData.showcaseProducts[selectedProductIndex].material);
                break;
        }
    }

    void ObjectChanged(GameObject gameObject)
    {
        if (showcaseProductsData.showcaseProducts[selectedProductIndex].spear == null) return;

        foreach (Transform child in spearPlatformObjectTransform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(gameObject, spearPlatformObjectTransform);
    }

    void MaterialChanged(Material material)
    {
        // If there is no material, break the method.
        if (showcaseProductsData.showcaseProducts[selectedProductIndex].material == null) return;

        spearPlatformObjectTransform.GetChild(0).GetComponent<Renderer>().material = material;
    }




    // Coroutine to fade in the ad.
    IEnumerator ShowAdsProduct()
    {
        // Get your ad list.
        adsProducts = adsProductsData.adsProductsDatas;

        // If there are no ads, break the loop.
        if (adsPanelParent.childCount != 0) yield break;

        // Create advertising items and add Canvasgroup.
        foreach (Sprite adSprite in adsProducts)
        {
            GameObject newAdProduct = Instantiate(adsProductPrefab, adsPanelParent);
            Image adImage = newAdProduct.GetComponent<Image>();
            adImage.sprite = adSprite;

            // We can control Alpha by adding Canvasgroup.
            CanvasGroup canvasGroup = newAdProduct.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0; // At first it will be invisible.
        }

        float fadeDuration = 0.2f; // Fade-in and Fade-Out time.
        float displayDuration = 5f; // The time of demonstration of ads.
        int currentAdIndex = 0;

        // Advertising cycle.
        while (true)
        {
            if (!isOpenPanel) // Only the animation works when the panel is on.
            {
                // Choose the current ad.
                Transform currentAd = adsPanelParent.GetChild(currentAdIndex);
                CanvasGroup currentCanvasGroup = currentAd.GetComponent<CanvasGroup>();

                // Make the ad fade-in.
                yield return StartCoroutine(FadeIn(currentCanvasGroup, fadeDuration));

                // Show the ad a certain period of time.
                yield return new WaitForSeconds(displayDuration);

                // Make the ad fade-out.
                yield return StartCoroutine(FadeOut(currentCanvasGroup, fadeDuration));

                // Cross the next ad.
                currentAdIndex = (currentAdIndex + 1) % adsPanelParent.childCount;
            }
            else
            {
                // If the panel is closed, wait.
                yield return null;
            }
        }
    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = 1; // Make the visibility exactly 1.
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = 0; // Make the visibility exactly 0.
    }
}