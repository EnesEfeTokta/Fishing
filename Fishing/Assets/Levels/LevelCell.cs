using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text levelIndexText; // Text to display the level index.
    [SerializeField] private Image levelBgImage; // Background image of the level cell.
    [SerializeField] private Button levelButton; // Button to interact with the level cell.

    [Header("Game Objects")]
    [SerializeField] private GameObject levelLock; // GameObject to indicate if the level is locked.
    [SerializeField] private GameObject levelCompleted; // GameObject to indicate if the level is completed.

    /// <summary>
    /// Sets up the level cell with the provided information.
    /// </summary>
    /// <param name="levelIndex">The index of the level.</param>
    /// <param name="color">The background color of the level cell.</param>
    /// <param name="isLocked">Whether the level is locked.</param>
    /// <param name="isCompleted">Whether the level is completed.</param>
    /// <param name="isInteractable">Whether the level button is interactable.</param>
    public void SetLevelCell(int levelIndex, Color color, bool isLocked, bool isCompleted, bool isInteractable)
    {
        SetLevelIndex(levelIndex);
        SetLevelIndexColor(color);
        SetLevelLocked(isLocked);
        SetLevelCompleted(isCompleted);
        SetButtonInteractable(isInteractable);
        SetButton();
    }

    /// <summary>
    /// Sets the level index text.
    /// </summary>
    /// <param name="levelIndex">The index of the level.</param>
    void SetLevelIndex(int levelIndex)
    {
        levelIndexText.text = levelIndex.ToString();
    }

    /// <summary>
    /// Sets the background color of the level cell.
    /// </summary>
    /// <param name="color">The color to set.</param>
    void SetLevelIndexColor(Color color)
    {
        levelBgImage.color = color;
    }

    /// <summary>
    /// Sets the visibility of the lock icon.
    /// </summary>
    /// <param name="isLocked">Whether the level is locked.</param>
    void SetLevelLocked(bool isLocked)
    {
        levelLock.gameObject.SetActive(isLocked);
    }

    /// <summary>
    /// Sets the visibility of the completed icon.
    /// </summary>
    /// <param name="isCompleted">Whether the level is completed.</param>
    void SetLevelCompleted(bool isCompleted)
    {
        levelCompleted.SetActive(isCompleted);
    }

    /// <summary>
    /// Sets the interactability of the level button.
    /// </summary>
    /// <param name="isInteractable">Whether the button should be interactable.</param>
    void SetButtonInteractable(bool isInteractable)
    {
        levelButton.interactable = isInteractable;
    }

    /// <summary>
    /// Adds a listener to the button's click event.
    /// </summary>
    void SetButton()
    {
        levelButton.onClick.AddListener(OnLevelButtonClick);
    }

    /// <summary>
    /// Called when the level button is clicked.
    /// </summary>
    void OnLevelButtonClick()
    {
        Debug.Log("Level button clicked!"); // Placeholder for level loading logic.
    }
}