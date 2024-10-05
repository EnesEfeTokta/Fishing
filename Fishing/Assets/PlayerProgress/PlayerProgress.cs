using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    // Reference to the PlayerProgressData ScriptableObject that holds player progress information.
    [SerializeField] private PlayerProgressData playerProgressData;

    // Variables to track player's total score, money, and fish count.
    [SerializeField] private int totalPlayerScore;
    [SerializeField] private int totalPlayerMoney;
    [SerializeField] private int totalPlayerFish;

    // Initialize the player's progress data when the game starts.
    void Start()
    {
        // Load the data from the ScriptableObject into the local variables.
        totalPlayerScore = playerProgressData.totalPlayerScore;
        totalPlayerMoney = playerProgressData.totalPlayerMoney;
        totalPlayerFish = playerProgressData.totalPlayerFish;
    }

    // Method to print the player's data to TMP_Texts.
    public void TextPrintPlayerProgressData(TMP_Text score, TMP_Text money, TMP_Text fish)
    {
        // The process of printing the player's data to TMP_Texts.
        score.text = totalPlayerScore.ToString();
        money.text = totalPlayerMoney.ToString();
        fish.text = totalPlayerFish.ToString();
    }

    // Method to add money to the player's total money count.
    public void AddMoney(int count)
    {
        // Update the ScriptableObject with the new money amount.
        playerProgressData.totalPlayerMoney += count;
    }

    // Method to decrease money from the player's total money count.
    public void DecreasingMoney(int count)
    {
        // Deduct the specified amount from the player's total money in the ScriptableObject.
        playerProgressData.totalPlayerMoney -= count;
    }

    // Method to add score to the player's total score.
    public void AddScore(int count)
    {
        // Update the ScriptableObject with the new score.
        playerProgressData.totalPlayerScore += count;
    }

    // Method to add fish to the player's total fish count.
    public void AddFish(int count)
    {
        // Update the ScriptableObject with the new fish count.
        playerProgressData.totalPlayerFish += count;
    }
}