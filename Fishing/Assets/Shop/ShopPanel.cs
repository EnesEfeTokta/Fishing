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
    [SerializeField] private Button spearButton; // Button to display spear-related products.
    [SerializeField] private Button coatingsButton; // Button to display coatings-related products.

    [Header("Ads Panel")]
    [SerializeField] private Transform adsPanelParent;
    [SerializeField] private GameObject adsProductPrefab;
    [SerializeField] private AdsProductsData adsProductsData;
    private List<Sprite> adsProducts = new List<Sprite>();

    private List<GameObject> showingProducts = new List<GameObject>(); // List to keep track of displayed products.

    void Start()
    {
        // Initialize the player progress data component.
        playerProgress = GetComponent<PlayerProgress>();

        StartCoroutine(ShowAdsProduct());
    }

    public void OnSpearButtonClick()
    {
        // Destroy all currently displayed products to avoid duplication.
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }

        // Show only the products that are of type "Spear".
        ShowProduct(ProductType.Spear);

        // Set button interactability: Disable the spear button to prevent multiple clicks.
        spearButton.interactable = false;
        coatingsButton.interactable = true;
    }

    public void OnCoatingsButtonClick()
    {
        // Destroy all currently displayed products to avoid duplication.
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }

        // Show only the products that are of type "Coatings".
        ShowProduct(ProductType.Coatings);

        // Set button interactability: Disable the coatings button to prevent multiple clicks.
        coatingsButton.interactable = false;
        spearButton.interactable = true;
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
            OnSpearButtonClick();
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
    void ShowProduct(ProductType productType)
    {
        // Pre-manufactured and listed products are cleaned.
        showingProducts.Clear();

        // Loop through all products defined in the showcaseProductsData.
        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            // Check if the product matches the selected product type.
            if (showcaseProduct.productType == productType)
            {
                // Instantiate a product prefab as a child of the parent transform.
                ProductCell productCell = Instantiate(productPrefab, parent).GetComponent<ProductCell>();

                // Call the method on the product cell to populate the UI with product information.
                productCell.ShowProductInformation(showcaseProduct, playerProgress);

                // Produced products are listed.
                showingProducts.Add(productCell.gameObject);
            }
        }
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