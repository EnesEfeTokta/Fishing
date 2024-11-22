using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance;

    // Holds the player's progress data (e.g., score, money, fish count).
    [HideInInspector] public PlayerProgressData playerProgressData;

    // Reference to the GameManager to access and modify player data.
    private GameManager gameManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called once at the start of the game to initialize player progress.
    void Start()
    {
        // Get the GameManager component attached to this GameObject.
        gameManager = GetComponent<GameManager>();

        // Load the player's progress data.
        SetPlayerProgress();
    }

    /// <summary>
    /// Loads the player's progress data from the GameManager.
    /// </summary>
    void SetPlayerProgress()
    {
        playerProgressData = gameManager.ReadPlayerProgressData(playerProgressData);
    }

    /// <summary>
    /// Displays the player's progress data on the provided TMP_Text elements.
    /// </summary>
    /// <param name="score">Text field to display the player's total score.</param>
    /// <param name="money">Text field to display the player's total money.</param>
    /// <param name="fish">Text field to display the player's total fish count.</param>
    public void TextPrintPlayerProgressData(TMP_Text score, TMP_Text money, TMP_Text fish)
    {
        score.text = playerProgressData.totalPlayerScore.ToString();
        money.text = playerProgressData.totalPlayerMoney.ToString();
        fish.text = playerProgressData.totalPlayerFish.ToString();
    }

    /// <summary>
    /// Adds money to the player's total money count.
    /// </summary>
    /// <param name="count">The amount of money to add.</param>
    public void AddMoney(int count)
    {
        playerProgressData.totalPlayerMoney += count; // Update the total money.
    }

    /// <summary>
    /// Decreases the player's total money count.
    /// </summary>
    /// <param name="count">The amount of money to subtract.</param>
    public void DecreasingMoney(int count)
    {
        playerProgressData.totalPlayerMoney -= count; // Subtract from the total money.
    }

    /// <summary>
    /// Adds score to the player's total score.
    /// </summary>
    /// <param name="count">The amount of score to add.</param>
    public void AddScore(int count)
    {
        playerProgressData.totalPlayerScore += count; // Update the total score.
    }

    /// <summary>
    /// Adds fish to the player's total fish count.
    /// </summary>
    /// <param name="count">The number of fish to add.</param>
    public void AddFish(int count)
    {
        playerProgressData.totalPlayerFish += count; // Update the total fish count.
    }

    /// <summary>
    /// Reads and returns the player's total money.
    /// </summary>
    /// <returns>The player's total money.</returns>
    public int ReadMoney()
    {
        return playerProgressData.totalPlayerMoney;
    }

    /// <summary>
    /// Reads and returns the player's total fish count.
    /// </summary>
    /// <returns>The player's total fish count.</returns>
    public int ReadFish()
    {
        return playerProgressData.totalPlayerFish;
    }

    /// <summary>
    /// Reads and returns the player's total score.
    /// </summary>
    /// <returns>The player's total score.</returns>
    public int ReadScore()
    {
        return playerProgressData.totalPlayerScore;
    }
}