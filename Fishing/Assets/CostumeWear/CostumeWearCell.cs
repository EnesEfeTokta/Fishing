using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostumeWearCell : MonoBehaviour
{
    [Header("UI")]
    public Image icon; // The icon image of the costume wear cell.
    public TMP_Text nameText; // The name text of the costume wear cell.
    public Button button; // The button component of the costume wear cell.

    void Start()
    {
        button.interactable = true; // Enable the button component.
        button.onClick.AddListener(OnCellClicked); // Add a listener to the button component.
    }

    void OnCellClicked()
    {
        if (!button.interactable) return; // If the button is not interactable, return early.

        button.interactable = false; // Disable the button component.
        // Seçim yapıldığında yapılacak işlemler buraya yazılacak.
    }

    public void InteractableButton(bool enable)
    {
        button.interactable = enable; // Enable or disable the button component based on the input parameter.
    }
}
