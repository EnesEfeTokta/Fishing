using System;
using System.Collections.Generic;
using UnityEngine;

// This script defines a ScriptableObject that holds the list of showcase products for the shop.
// Each product has its own data such as name, price, image, description, etc.
[CreateAssetMenu(fileName = "ShowcaseProductsData", menuName = "ScriptableObject/ShowcaseProductsData")]
public class ShowcaseProductsData : ScriptableObject
{
    // List to hold all the products available for showcase in the shop.
    public List<ShowcaseProduct> showcaseProducts = new List<ShowcaseProduct>();
}

[Serializable]
public class ShowcaseProduct
{
    // Product ID for uniquely identifying each product.
    public string name;

    // It holds the type of product.
    public ProductType productType = ProductType.Object;

    [Space]

    // Image of the product to be displayed in the UI.
    public Sprite productImage;

    // Name of the product.
    public string productName;

    // Price of the product.
    public int productPrice;

    // Description of the product (TextArea allows multi-line input in the Inspector).
    [TextArea] public string productDescription;

    [Space]
/*
    public Material material = null;
    public GameObject spear = null;
*/

    public SpearDress spearDress = null;

    public Material material = null;

    [Space]

    public object additionalData;

    [Space]

    // Boolean flag to indicate whether the product has been purchased.
    public bool isPurchased = false;

    // The date when the product was received after purchase.
    public string productReceivedDate;

    // Method to mark the product as purchased and set the received date to the current date.
    public bool StartPurchased()
    {
        // Check if the product is already purchased.
        if (isPurchased)
        {
            return false;
        }

        // Get the current date and time.
        DateTime now = DateTime.Now;

        // Convert the date to a string and store it in productReceivedDate.
        productReceivedDate = now.ToString();

        // Mark the product as purchased.
        isPurchased = true;

        return isPurchased;
    }
}

// The type options of the product. The types of the product can be determined.
public enum ProductType
{
    Object,
    Material
}