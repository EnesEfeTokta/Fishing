using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostumeWearCell : MonoBehaviour
{
    [Header("UI")]
    public Image background; // The background image of the costume wear cell.
    public Image icon; // The icon image of the costume wear cell.
    public TMP_Text nameText; // The name text of the costume wear cell.
    public TMP_Text buttonText; // The button text of the commission cell.
    public Button button; // The button component of the costume wear cell.

    private OwnedMaterial ownedMaterial;
    private OwnedSpear ownedSpear;

    private bool isChoose = false;

    private CostumeWearPanel costumeWearPanel; // The panel containing the costume wear cell.

    private ProductType productType;

    void Start()
    {
        button.interactable = true; // Enable the button component.
        button.onClick.AddListener(OnCellClicked); // Add a listener to the button component.
    }

    void OnCellClicked()
    {
        isChoose = !isChoose; // Set the isChoose flag to the opposite of its current value.
        background.color = isChoose? Color.green : Color.white; // Change the background color of the commission cell based on the isChoose flag.
        buttonText.text = isChoose? "Unchoose" : "Choose"; // Change the button text of the commission cell based on the isChoose flag.
        
        if (isChoose)
        {
            switch (productType)
            {
                case ProductType.Object:
                    costumeWearPanel.SelectOwnedSpear(this, ownedSpear); // Call the OnCostumeWearCellChoose method of the costumeWearPanel.
                    break;
                case ProductType.Material:
                    costumeWearPanel.SelectOwnedMaterial(this, ownedMaterial); // Call the OnCostumeWearCellChoose method of the costumeWearPanel.
                    break;
                default:
                    break;
            }
        }        
    }

    public void ResetChoose()
    {
        isChoose = false;
        background.color = Color.white; 
        buttonText.text = "Choose";
    }

    public void InteractableButton(bool enable)
    {
        button.interactable = enable; // Enable or disable the button component based on the input parameter.
    }

    public void SetCostumeWearCell<T>(T ownedItem, CostumeWearPanel costumeWearPanel, ProductType productType)
    {
        this.costumeWearPanel = costumeWearPanel; // Assign the costumeWearPanel to the costumeWearCell.
        this.productType = productType;
        
        // Set the costume wear cell with the given ownedItem.
        if (ownedItem is OwnedMaterial)
        {
            ownedMaterial = ownedItem as OwnedMaterial; // Cast the ownedItem to OwnedMaterial and assign it to ownedMaterial.
            icon.sprite = ownedMaterial.materialSprite; // Set the icon image of the costume wear cell.
            nameText.text = ownedMaterial.materialName; // Set the name text of the costume wear cell.
        }
        else if (ownedItem is OwnedSpear)
        {
            ownedSpear = ownedItem as OwnedSpear; // Cast the ownedItem to OwnedSpear and assign it to ownedSpear.
            icon.sprite = ownedSpear.spearSprite; // Set the icon image of the costume wear cell.
            nameText.text = ownedSpear.spearName; // Set the name text of the costume wear cell.
        }
    }
}
