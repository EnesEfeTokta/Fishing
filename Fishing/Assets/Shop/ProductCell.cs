using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCell : MonoBehaviour
{
    [Header("Product Features UI")]
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productPrice;
    [SerializeField] private TMP_Text productDescription;
    [SerializeField] private TMP_Text productStatus;
    [SerializeField] private TMP_Text productReceivedDate;
    [SerializeField] private Button transactionButton;

    private ShowcaseProduct showcaseProduct;
    private PlayerProgressData playerProgressData;
    private PlayerProgress playerProgress;

    void Start()
    {
        transactionButton.onClick.AddListener(ProductSelect);
    }

    void ProductSelect()
    {
        if (showcaseProduct.isPurchased)
        {
            ProductUse();
        }
        else
        {
            ProductBuy();
        }

        ShowProductInformation(showcaseProduct, playerProgress);
    }

    public void ShowProductInformation(ShowcaseProduct showcaseProduct, PlayerProgress playerProgress)
    {
        this.showcaseProduct = showcaseProduct;
        this.playerProgress = playerProgress;
        playerProgressData = playerProgress.playerProgressData;

        productImage.sprite = showcaseProduct.productImage;

        productName.text = showcaseProduct.productName;
        productPrice.text = showcaseProduct.productPrice.ToString();
        productDescription.text = showcaseProduct.productDescription;

        if (showcaseProduct.isPurchased)
        {
            productStatus.text = "Use";

            productReceivedDate.text = showcaseProduct.productReceivedDate;
        }
        else
        {
            productStatus.text = "Buy";

            productReceivedDate.text = "";
        }
    }

    bool PaymentApproval()
    {
        int firstMoney = playerProgressData.totalPlayerMoney;
        int productPrice = showcaseProduct.productPrice;

        if (firstMoney >= productPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ProductUse()
    {
        // Ürün burada giyiliyor.
    }

    void ProductBuy()
    {
        if (PaymentApproval())
        {
            ShowDebugMessage("Ödeme Başarılı");

            showcaseProduct.StartPurchased();

            playerProgress.DecreasingMoney(showcaseProduct.productPrice);
        }
        else
        {
            ShowDebugMessage("Ödeme başarısız oldu.");
        }
    }

    void ShowDebugMessage(string message)
    {
        Debug.Log(message);
    }
}