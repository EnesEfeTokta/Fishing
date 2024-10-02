using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShowcaseProductsData", menuName = "ScriptableObject/ShowcaseProductsData")]
public class ShowcaseProductsData : ScriptableObject
{
    public List<ShowcaseProduct> showcaseProducts = new List<ShowcaseProduct>();
}

[Serializable]
public class ShowcaseProduct
{
    public int productId = 000;

    [Space]

    public Sprite productImage;
    public string productName;
    public int productPrice;
    [TextArea] public string productDescription;

    [Space]

    public bool isPurchased = false;
    public string productReceivedDate;

    public void StartPurchased()
    {
        DateTime now = DateTime.Now;
        productReceivedDate = now.ToString();
        isPurchased = true;
    }
}
