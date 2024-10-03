using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCell : MonoBehaviour
{
    [SerializeField] private Image productImage;

    [SerializeField] private TMP_Text productName;
    [SerializeField] private TMP_Text productPrice;
    [SerializeField] private TMP_Text productDescription;
    [SerializeField] private TMP_Text productStatus;
    [SerializeField] private TMP_Text productReceivedDate;

    [SerializeField] private Button button;

    private ShowcaseProduct showcaseProduct;

    void Start()
    {
        button.onClick.AddListener(() => ProductSelect());
    }

    void ProductSelect()
    {
        if (showcaseProduct.isPurchased)
        {
            // The product will be used ...
        }
        else
        {
            if (PaymentApproval())
            {
                Debug.Log("Payment is successful.");

                showcaseProduct.StartPurchased();
            }
            else
            {
                Debug.Log("The payment failed.");
            }
        }
    }

    public void ShowProductInformation(ShowcaseProduct showcaseProduct)
    {
        this.showcaseProduct = showcaseProduct;

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
        // The payment process will be verified ...

        return true;
    }
}