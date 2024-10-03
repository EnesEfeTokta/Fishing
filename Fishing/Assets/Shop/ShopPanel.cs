using UnityEngine;

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

    public void PanelOpenClose(bool isOpen)
    {
        settingsPanel.SetActive(isOpen);
        isOpenPanel = isOpen;

        ShowProduct();
    }

    void ShowProduct()
    {
        foreach (ShowcaseProduct showcaseProduct in showcaseProductsData.showcaseProducts)
        {
            ProductCell productCell = Instantiate(productPrefab, parent).GetComponent<ProductCell>();

            productCell.ShowProductInformation(showcaseProduct);
        }
    }
}
