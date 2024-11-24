using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Reference to the TextMeshPro UI element that displays the score.
    [SerializeField] private TMP_Text score;

    // Keeps track of the player's current score.
    private int scoreCount;

    // Called once at the start of the game to initialize the score.
    void Start()
    {
        // Set the initial score text to "0".
        score.text = "0";
    }

    /// <summary>
    /// Increases the player's score by the specified amount.
    /// </summary>
    /// <param name="score">The amount of score to add.</param>
    public void ScoreIncrease(int score)
    {
        // Add the specified score to the current score count.
        scoreCount += score;

        // Update the score UI text to reflect the new score count.
        this.score.text = scoreCount.ToString();

        // Update the player's progress with the new score.
        AddFishData(score);
    }

    /// <summary>
    /// Updates the PlayerProgress with the added score.
    /// </summary>
    /// <param name="score">The amount of score added.</param>
    void AddFishData(int score)
    {
        PlayerProgress.Instance.AddScore(score);
    }
}