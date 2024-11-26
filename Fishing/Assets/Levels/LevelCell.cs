using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text levelIndexText;
    [SerializeField] private Image levelBgImage;
    [SerializeField] private Button levelButton;

    [Header("Game Objects")]
    [SerializeField] private GameObject levelLock;
    [SerializeField] private GameObject levelCompleted;


    public void SetLevelIndex(int levelIndex)
    {
        levelIndexText.text = levelIndex.ToString();
    }

    public void SetLevelIndexColor(Color color)
    {
        levelBgImage.color = color;
    }

    public void SetLevelLocked(bool isLocked)
    {
        levelLock.gameObject.SetActive(isLocked);
    }

    public void SetLevelCompleted(bool isCompleted)
    {
        levelCompleted.SetActive(isCompleted);
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        levelButton.interactable = isInteractable;
    }

    public void SetButton()
    {
        levelButton.onClick.AddListener(OnLevelButtonClick);
    }

    void OnLevelButtonClick()
    {
        Debug.Log("Level button clicked!");
    }
}
