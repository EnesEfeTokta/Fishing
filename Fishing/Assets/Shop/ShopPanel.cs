using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("Showcase Products Data")]
    [SerializeField] private ShowcaseProductsData showcaseProductsData;

    [Header("Product Object Prefab")]
    [SerializeField] private GameObject productPrefab;

    [Header("Parent")]
    [SerializeField] private Transform parent;

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;
    private bool isOpenPanel = false;

    [Header("Player's Money Management")]
    [SerializeField] private PlayerProgressData playerProgressData;
    private int totalPlayerMoney;
    [SerializeField] private TMP_Text money;
    private PlayerProgress playerProgress;

    [Header("Option Buttons")]
    [SerializeField] private Button objectButton;
    [SerializeField] private Button materialButton;

    [Header("Ads Panel")]
    [SerializeField] private Transform adsPanelParent;
    [SerializeField] private GameObject adsProductPrefab;
    [SerializeField] private AdsProductsData adsProductsData;
    private List<Sprite> adsProducts = new List<Sprite>();
    private Coroutine adsCoroutine;

    [Header("Product Showcase")]
    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productPrice;
    [SerializeField] private TMP_Text productDescription;
    [SerializeField] private TMP_Text productStatus;
    [SerializeField] private TMP_Text productReceivedDate;
    [SerializeField] private TMP_Text productSelectIndex;
    [SerializeField] private Button productTransactionButton;

    private List<GameObject> showingProducts = new List<GameObject>();
    private int selectedProductIndex = 0;

    [Header("Platform")]
    [SerializeField] private GameObject spearObj;

    void Start()
    {
        playerProgress = GetComponent<PlayerProgress>();
        UpdateEconomicData();
        ShowAllProducts();
        OnObjectButtonClick();
    }

    public void OnObjectButtonClick()
    {
        ClearDisplayedProducts();
        ShowProducts(ProductType.Object);
        SelectFirstProduct();

        objectButton.interactable = false;
        materialButton.interactable = true;
    }

    public void OnMaterialButtonClick()
    {
        ClearDisplayedProducts();
        ShowProducts(ProductType.Material);
        SelectFirstProduct();

        materialButton.interactable = false;
        objectButton.interactable = true;
    }

    public void PanelOpenClose(bool isOpen)
    {
        settingsPanel.SetActive(isOpen);
        isOpenPanel = isOpen;

        if (isOpen)
        {
            OnObjectButtonClick();
            adsCoroutine = StartCoroutine(ShowAdsProduct());
        }
        else
        {
            StopCoroutine(adsCoroutine);
        }

        UpdateEconomicData();
    }

    public void UpdateEconomicData()
    {
        totalPlayerMoney = playerProgressData.totalPlayerMoney;
        money.text = totalPlayerMoney.ToString();
    }

    void ClearDisplayedProducts()
    {
        foreach (GameObject product in showingProducts)
        {
            Destroy(product);
        }
        showingProducts.Clear();
        selectedProductIndex = 0;
    }

    void ShowAllProducts()
    {
        ClearDisplayedProducts();

        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            GameObject newProduct = Instantiate(productPrefab, parent);
            newProduct.GetComponent<ProductCell>().SetProductCell(showcaseProduct, this);
            showingProducts.Add(newProduct);
        }
    }

    void ShowProducts(ProductType productType)
    {
        ClearDisplayedProducts();

        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            if (showcaseProduct.productType == productType)
            {
                GameObject newProduct = Instantiate(productPrefab, parent);
                newProduct.GetComponent<ProductCell>().SetProductCell(showcaseProduct, this);
                showingProducts.Add(newProduct);
            }
        }
    }

    public void ActionButtonClick(bool direction)
    {
        if (showingProducts.Count <= 0) return;

        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.white);

        if (direction)
        {
            selectedProductIndex = (selectedProductIndex + 1) % showingProducts.Count;
        }
        else
        {
            selectedProductIndex = (selectedProductIndex - 1 + showingProducts.Count) % showingProducts.Count;
        }

        SelectProduct(showcaseProductsData.showcaseProducts[selectedProductIndex], showingProducts[selectedProductIndex]);
    }

    public void SelectProduct(ShowcaseProduct showcaseProduct, GameObject product)
    {
        if (showingProducts.Count <= 0) return;

        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.white);

        selectedProductIndex = showingProducts.IndexOf(product);
        productSelectIndex.text = $"Selected Index: {selectedProductIndex + 1}";
        showingProducts[selectedProductIndex].GetComponent<ProductCell>().ProductCellBackgroundColorChanged(Color.green);

        productName.text = showcaseProduct.productName;
        productPrice.text = $"{showcaseProduct.productPrice}$";
        productDescription.text = showcaseProduct.productDescription;
        productStatus.text = showcaseProduct.isPurchased ? "Use" : "Buy";
        productReceivedDate.text = showcaseProduct.productReceivedDate != null ? showcaseProduct.productReceivedDate : "--.--.----";

        productTransactionButton.onClick.RemoveAllListeners();
        productTransactionButton.onClick.AddListener(ProductBuy);

        PatformChanged(showcaseProduct.productType);
    }

    void PatformChanged(ProductType productType)
    {
        switch (productType)
        {
            case ProductType.Object:
                SpearDress spearDress = showingProducts[selectedProductIndex].GetComponent<ProductCell>().showcaseProduct.spearDress;
                spearObj.GetComponent<SpearDressing>().StartSpearDressing(spearDress.mesh, spearDress.materials);
                break;
            case ProductType.Material:
                Material spearMaterial = showingProducts[selectedProductIndex].GetComponent<ProductCell>().showcaseProduct.material;
                spearObj.GetComponent<SpearDressing>().StartSpearSpecialDressing(playerProgressData.spearDress.mesh, spearMaterial);
                break;
        }
    }

    void ProductBuy()
    {
         if (showcaseProductsData.showcaseProducts[selectedProductIndex].isPurchased) return;

        if (BalanceSufficient())
        {
            showcaseProductsData.showcaseProducts[selectedProductIndex].StartPurchased();
            playerProgressData.totalPlayerMoney -= showcaseProductsData.showcaseProducts[selectedProductIndex].productPrice;
            UpdateEconomicData();

            productStatus.text = "Use";
            productReceivedDate.text = DateTime.Now.ToString("dd.MM.yyyy. HH:mm:ss");
            AcceptItem();
            SelectProduct(showcaseProductsData.showcaseProducts[selectedProductIndex], showingProducts[selectedProductIndex]);

            Message.Instance.NewMessage(
                "PAYMENT SUCCESSFUL",
                "Your payment has been subtracted from your balance. You are ready to use.",
                MessageStatus.LessImportant,
                5
            );
        }
        else
        {
            Message.Instance.NewMessage(
                "PAYMENT FAILED",
                "Your balance is not enough. Please increase your balance.",
                MessageStatus.VeryImportant,
                5
            );
        }
    }

    bool BalanceSufficient()
    {
        int productPrice = showcaseProductsData.showcaseProducts[selectedProductIndex].productPrice;
        int playerPrice = playerProgressData.totalPlayerMoney;

        return playerPrice >= productPrice;
    }

    void AcceptItem()
    {
         if (showcaseProductsData.showcaseProducts[selectedProductIndex].productType == ProductType.Object)
        {
             playerProgressData.possessedSpearDresses.Add(showcaseProductsData.showcaseProducts[selectedProductIndex].spearDress);
        }
    }

    private void SelectFirstProduct()
    {
        if (showingProducts.Count > 0)
        {
             SelectProduct(showcaseProductsData.showcaseProducts[0], showingProducts[0]);
        }
    }


    IEnumerator ShowAdsProduct()
    {
        adsProducts = adsProductsData.adsProductsDatas;

        if (adsProducts == null || adsProducts.Count <= 0) yield break;
        if (adsPanelParent.childCount > 0) yield break;

        foreach (Sprite adSprite in adsProducts)
        {
            GameObject newAdProduct = Instantiate(adsProductPrefab, adsPanelParent);
            Image adImage = newAdProduct.GetComponent<Image>();
            adImage.sprite = adSprite;

            CanvasGroup canvasGroup = newAdProduct.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        float fadeDuration = 0.2f;
        float displayDuration = 5f;
        int currentAdIndex = 0;

        while (true)
        {
            if (!isOpenPanel)
            {
                Transform currentAd = adsPanelParent.GetChild(currentAdIndex);
                CanvasGroup currentCanvasGroup = currentAd.GetComponent<CanvasGroup>();

                yield return StartCoroutine(FadeIn(currentCanvasGroup, fadeDuration));
                yield return new WaitForSeconds(displayDuration);
                yield return StartCoroutine(FadeOut(currentCanvasGroup, fadeDuration));

                currentAdIndex = (currentAdIndex + 1) % adsPanelParent.childCount;
            }
            else
            {
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
        canvasGroup.alpha = 1;
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
        canvasGroup.alpha = 0;
    }
}