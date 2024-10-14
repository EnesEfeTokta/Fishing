using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    // Reference to the PlayerProgressData ScriptableObject that holds player progress information.
    public PlayerProgressData playerProgressData;

    // Method to print the player's data to TMP_Texts.
    public void TextPrintPlayerProgressData(TMP_Text score, TMP_Text money, TMP_Text fish)
    {
        // The process of printing the player's data to TMP_Texts.
        score.text = playerProgressData.totalPlayerScore.ToString();
        money.text = playerProgressData.totalPlayerMoney.ToString();
        fish.text = playerProgressData.totalPlayerFish.ToString();
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