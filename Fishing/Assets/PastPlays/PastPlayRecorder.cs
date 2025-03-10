using System.Collections.Generic;
using UnityEngine;

public class PastPlayRecorder : MonoBehaviour
{
    public static PastPlayRecorder Instance;
    // Stores the data for past plays.
    private PastPlaysData pastPlaysData;

    // A list of icons to represent past plays.
    [SerializeField] private List<Sprite> pastPlayIcons = new List<Sprite>();

    void Awake()
    {
        // Singleton pattern: Ensure only one instance of PastPlayRecorder exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any extra instances if already one exists
        }
    }

    // Initialize components and set up past play data.
    void Start()
    {
        // Load the data for previous plays.
        SetPastPlaysData();

        // If the pastPlaysData or pastPlayIcons are not assigned, stop execution.
        if (pastPlaysData == null || pastPlayIcons == null) // Changed '&&' to '||' since either being null is problematic.
        {
            Debug.LogWarning("PastPlaysData or pastPlayIcons is not assigned."); // Optional log for easier debugging.
            return;
        }
    }

    // Loads past play data using the GameManager.
    void SetPastPlaysData()
    {
        pastPlaysData = GameManager.Instance.ReadPastPlaysData(pastPlaysData);
    }

    /// <summary>
    /// Adds a new past play record to the list of past plays.
    /// </summary>
    /// <param name="name">Name of the player or session.</param>
    /// <param name="levelIndex">Index of the level played.</param>
    /// <param name="scoreValue">Player's score in the level.</param>
    /// <param name="fishValue">Amount of fish caught in the level.</param>
    /// <param name="moneyValue">Money earned during the level.</param>
    /// <param name="levelInformationData">Data about the level's objectives.</param>
    public void AddPastPlayData( 
        float scoreValue, 
        float fishValue, 
        float moneyValue, 
        LevelInformationData levelInformationData)
    {
        // Get the name and level index from the level information data.
        //string name = levelInformationData.levelName;
        string name = "Player";
        int levelIndex = levelInformationData.LevelIndex;

        // Create a new instance of PastPlayData.
        PastPlayData newPastPlay = new PastPlayData();

        // Populate the new past play with provided data.
        newPastPlay = CreatePastPlay(
            newPastPlay, name, levelIndex, 
            scoreValue, fishValue, moneyValue, levelInformationData
        );

        // Add the new past play data to the list.
        pastPlaysData.pastPlayDatas.Add(newPastPlay);

        new DBSave(pastPlaysData); // Save the updated data.
    }

    /// <summary>
    /// Creates and populates a PastPlayData object.
    /// </summary>
    /// <param name="pastPlayData">The PastPlayData instance to populate.</param>
    /// <param name="name">Name of the player or session.</param>
    /// <param name="levelIndex">Index of the level played.</param>
    /// <param name="scoreValue">Player's score in the level.</param>
    /// <param name="fishValue">Amount of fish caught in the level.</param>
    /// <param name="moneyValue">Money earned during the level.</param>
    /// <param name="levelInformationData">Data about the level's objectives.</param>
    /// <returns>A populated PastPlayData object.</returns>
    PastPlayData CreatePastPlay(
        PastPlayData pastPlayData,
        string name, 
        int levelIndex, 
        float scoreValue, 
        float fishValue, 
        float moneyValue, 
        LevelInformationData levelInformationData)
    {
        // Assign a random icon from the list of available icons.
        pastPlayData.icon = RandomIcon(pastPlayIcons);

        // Assign the basic information.
        pastPlayData.name = name;
        pastPlayData.levelIndex = levelIndex;

        // Assign performance values.
        pastPlayData.scoreValue = scoreValue;
        pastPlayData.fishValue = fishValue;
        pastPlayData.moneyValue = moneyValue;

        // Assign level-related maximum values for comparison.
        pastPlayData.maxScoreValue = levelInformationData.maxScoreCount;
        pastPlayData.maxFishValue = levelInformationData.totalFishCount;
        pastPlayData.maxMoneyValue = levelInformationData.maxMoneyCount;

        return pastPlayData;
    }

    /// <summary>
    /// Selects a random sprite from a list of sprites.
    /// </summary>
    /// <param name="sprites">The list of sprites to choose from.</param>
    /// <returns>A randomly selected sprite.</returns>
    Sprite RandomIcon(List<Sprite> sprites)
    {
        // Ensure the list is not empty to avoid exceptions.
        if (sprites.Count == 0)
        {
            Debug.LogWarning("No sprites available for selection.");
            return null; // Return null if no sprites are available.
        }

        // Select a random index within the list's range.
        int randomIndex = Random.Range(0, sprites.Count);

        // Return the selected sprite.
        return sprites[randomIndex];
    }
}