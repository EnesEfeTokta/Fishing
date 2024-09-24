using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Reference to the TextMeshPro UI element that will display the score.
    [SerializeField] private TMP_Text score;

    // Integer to keep track of the current score count.
    private int scoreCount;

    void Start()
    {
        // Initialize the score text to "0" when the game starts.
        score.text = "0";
    }

    // Method to increase the score by a specified amount.
    public void ScoreIncrease(int score)
    {
        // Add the given score to the current score count.
        scoreCount += score;

        // Update the score UI text to reflect the new score count.
        this.score.text = scoreCount.ToString();
    }
}