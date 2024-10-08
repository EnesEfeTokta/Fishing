using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PastPlay : MonoBehaviour
{
    [Header("Past Play Info")]
    [SerializeField] private Image icon; // Icon representing the past play
    [SerializeField] private new TMP_Text name; // Name of the past play
    [SerializeField] private TMP_Text level; // Level reached in the past play

    // Visual bars representing percentages.
    [Header("Value Bars")]
    [SerializeField] private Image scoreBarValue;
    [SerializeField] private Image fishBarValue;
    [SerializeField] private Image moneyBarValue;

    // Texts showing the data.
    [Header("Value Texts")]
    [SerializeField] private TMP_Text scoreTextValue;
    [SerializeField] private TMP_Text fishTextValue;
    [SerializeField] private TMP_Text moneyTextValue;

    // Method to display past play features on the UI.
    public void ShowPastPlayFeatures(PastPlayData showPastPlayParameter)
    {
        this.icon.sprite = showPastPlayParameter.icon; // Set the icon.
        this.name.text = showPastPlayParameter.name; // Set the name.
        level.text = $"Level {showPastPlayParameter.levelIndex}"; // Set the level text.

        // Set the text values and calculate the fill amounts for the progress bars.
        scoreTextValue.text = $"%{showPastPlayParameter.scoreValue}";
        fishTextValue.text = $"%{showPastPlayParameter.fishValue}";
        moneyTextValue.text = $"%{showPastPlayParameter.moneyValue}";

        // Calculate and set the fill amounts for the visual bars.
        scoreBarValue.fillAmount = CalculatePercentageRatio(showPastPlayParameter.scoreValue, showPastPlayParameter.maxScoreValue);
        fishBarValue.fillAmount = CalculatePercentageRatio(showPastPlayParameter.fishValue, showPastPlayParameter.maxFishValue);
        moneyBarValue.fillAmount = CalculatePercentageRatio(showPastPlayParameter.moneyValue, showPastPlayParameter.maxMoneyValue);
    }

    // Helper method to calculate the percentage ratio for progress bars.
    float CalculatePercentageRatio(float value, float maxValue)
    {
        // Ensure the ratio never exceeds 1.
        if (value / maxValue > 1)
        {
            return 1; // Return maximum ratio.
        }
        else
        {
            return value / maxValue; // Return actual ratio.
        }
    }
}