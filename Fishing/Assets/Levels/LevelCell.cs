using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelCell : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text levelIndexText; // Text to display the level index.
    [SerializeField] private Image levelBgImage; // Background image of the level cell.
    [SerializeField] private Button levelButton; // Button to interact with the level cell.

    [Header("Game Objects")]
    [SerializeField] private GameObject levelLock; // GameObject to indicate if the level is locked.
    [SerializeField] private GameObject levelCompleted; // GameObject to indicate if the level is completed.

    private RectTransform rectTransform;

    private string levelName;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

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
        rectTransform = GetComponent<RectTransform>();

        levelName = $"Level {levelIndex}";

        SetLevelIndex(levelIndex);
        SetLevelIndexColor(color);
        SetLevelLocked(isLocked);
        SetLevelCompleted(isCompleted);
        SetButtonInteractable(isInteractable);
        SetButton();

        LevelCellAnimation(1f, 0.5f, color, Color.white, isCompleted, isLocked);
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
        PlayerPrefs.SetString("SelectedLevel", levelName);
        SceneManager.LoadScene("Game"); // Load scene...
    }

    /// <summary>
    /// Animates the level cell with scaling and color transitions.
    /// </summary>
    /// <param name="scale">The target scale of the animation.</param>
    /// <param name="duration">The duration of the animation.</param>
    /// <param name="levelIndexTextColor">The target color for the level index text.</param>
    /// <param name="levelBgColor">The target color for the level background.</param>
    /// <param name="isCompleted">Whether the level is completed.</param>
    /// <param name="isLocked">Whether the level is locked.</param>
    void LevelCellAnimation(float scale, float duration, Color levelIndexTextColor, Color levelBgColor, bool isCompleted, bool isLocked)
    {
        // Adjust animation parameters based on level completion and locked status.
        if (!isCompleted && !isLocked)
        {
            // Increase scale and duration for available levels.
            scale = scale * 1.05f;
            duration = duration * 1.05f;
        }
        else
        {
            // Slightly increase scale and duration for completed/locked levels.
            scale = scale * 1.01f;
            duration = duration * 1.01f;

            // Set text color to white for completed/locked levels.
            levelIndexTextColor = Color.white;
        }

        // Apply scaling animation with yoyo effect.
        rectTransform.DOScale(scale, duration).SetLoops(-1, LoopType.Yoyo);

        // Apply color transition animation to the level index text with yoyo effect.
        levelIndexText.DOColor(levelIndexTextColor, duration).SetLoops(-1, LoopType.Yoyo);

        // Apply color transition animation to the level background image with yoyo effect.
        levelBgImage.DOColor(levelBgColor, duration).SetLoops(-1, LoopType.Yoyo);
    }
}