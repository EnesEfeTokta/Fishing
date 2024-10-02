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

    public void ShowProductInformation(ShowcaseProduct showcaseProduct)
    {
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
}
