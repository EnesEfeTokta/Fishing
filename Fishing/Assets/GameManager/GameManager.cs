using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.SceneManagement;

// Ensures that all required components are attached to the GameObject.
[RequireComponent(typeof(PastPlayRecorder))] // Handles the recording of past plays.
[RequireComponent(typeof(Score))] // Manages the scoring system.
[RequireComponent(typeof(PlayerProgress))] // Tracks the player's progress.
[RequireComponent(typeof(SettingsFounder))] // Handles settings management.
[RequireComponent(typeof(LevelInformationController))] // Controls level-related information.
[RequireComponent(typeof(FishIconMovement))] // Manages the movement of fish icons.
[RequireComponent(typeof(AudioSource))] // Plays sound effects.
[RequireComponent(typeof(PowerUpPanel))] // Manages the panel of power-up.

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager.
    public static GameManager Instance;

    // Serialized fields to store references to data objects.
    [SerializeField] private PlayerProgressData playerProgressData; // Data about player progress.


    [HideInInspector] public LevelInformationData levelInformationData; // Level-related data.

    // Lists to track fish objects and their states.
    [HideInInspector] public List<FishData> fishsDeath = new List<FishData>(); // List of data for fish that have died.
    [HideInInspector] public List<GameObject> fishsCreated = new List<GameObject>(); // List of created fish GameObjects.

    private AudioSource audioSource; // Getting the AudioSource component attached to this GameObject.

    void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of GameManager exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        levelInformationData = LevelChanged(playerProgressData.levelInformationDatas);

        LevelInformationController.Instance.StartLevelInformationController();

        Timer.Instance.SetFinishTime(levelInformationData.levelTime);
    }

    LevelInformationData LevelChanged(List<LevelInformationData> levelInformationDatas)
    {
        foreach (LevelInformationData levelInformationData in levelInformationDatas)
        {
            if (levelInformationData.IsSelected)
            {
                return levelInformationData;
            }
        }

        return null;
    }

    /// <summary>
    /// Provides access to the past plays data.
    /// </summary>
    public PastPlaysData ReadPastPlaysData(PastPlaysData pastPlaysData = null) => pastPlaysData = playerProgressData.pastPlaysData;

    /// <summary>
    /// Provides access to the player progress data.
    /// </summary>
    public PlayerProgressData ReadPlayerProgressData(PlayerProgressData playerProgressData = null) => playerProgressData = this.playerProgressData;

    /// <summary>
    /// Provides access to the level information data.
    /// </summary>
    public LevelInformationData ReadLevelInformationData(LevelInformationData levelInformationData = null) => levelInformationData = this.levelInformationData;

    /// <summary>
    /// Provides access to the settings data.
    /// </summary>
    public SettingsData ReadSettingsData(SettingsData settingsData = null) => settingsData = playerProgressData.settingsData;

    /// <summary>
    /// Handles the death of a fish in the game.
    /// </summary>
    public void FishDeath(GameObject fishDeathObject)
    {
        // Retrieve the data of the fish that has died.
        FishData fishData = fishDeathObject.GetComponent<Fish>().ReadFishData();

        // Remove the fish from the list of created fish and add it to the death list.
        fishsCreated.Remove(fishDeathObject);
        fishsDeath.Add(fishData);
        Destroy(fishDeathObject); // Destroy the fish GameObject.

        // Check if the level is completed after the fish is removed.
        IsLevelFinished();
    }

    /// <summary>
    /// Reads or updates the lists of created and dead fish.
    /// </summary>
    public (List<GameObject>, List<FishData>) ReadFishCreatAndDeadList(List<GameObject> fishsCreated = null, List<FishData> fishsDeath = null)
    {
        this.fishsCreated = fishsCreated ?? this.fishsCreated;
        this.fishsDeath = fishsDeath ?? this.fishsDeath;

        return (this.fishsCreated, this.fishsDeath);
    }

    /// <summary>
    /// Checks if the current level has been completed.
    /// </summary>
    public bool IsLevelFinished()
    {
        float time = Timer.Instance.InstantTime();

        if (time >= levelInformationData.levelTime)
        {
            Timer.Instance.GameFinished(); // Stop the timer.
            GameEndStatus.Instance.EndGame(ValueCalculationType.Failed);
            return true;
        }

        // Check if all fish are dead or the level time has expired.
        if (levelInformationData.totalFishCount == fishsDeath.Count)
        {
            //Timer.Instance.GameFinished(); // Stop the timer.
            GameEndStatus.Instance.EndGame(ValueCalculationType.Success); // Trigger the achievement screen.
            levelInformationData.IsSelected = false;

            Debug.Log($"Level {levelInformationData.levelName} completed!");

            RegisterTheLevel();

            return true; // Level is completed.
        }
        else
        {
            return false; // Level is not yet completed.
        }
    }

    private void RegisterTheLevel()
    {
        LevelInformationData nextLevelInformationData = levelInformationData.nextLevelInformationData;
        nextLevelInformationData.IsLevelOver = true;
        levelInformationData.IsLevelFinished = true;
    }

    /// <summary>
    /// Loads a scene with the given name.
    /// </summary>
    public void GoToHome(string name = "Home")
    {
        if (RecordTheProgress()) // Check if the progress can be recorded.
        {
            AddPastPlayData(); // Add the past play data to the game manager.
            SceneManager.LoadScene(name); // Load the specified scene.
        }
    }

    private void AddPastPlayData()
    {
        // Calculate the player's score, fish value, and money value.
        float scoreValue = GameEndStatus.Instance.levelResults["TotalScore"];
        float fishValue = GameEndStatus.Instance.levelResults["TotalFish"];
        float moneyValue = GameEndStatus.Instance.levelResults["TotalMoney"];

        PastPlayRecorder.Instance.AddPastPlayData(scoreValue, fishValue, moneyValue, ReadLevelInformationData());
    }

    /// <summary>
    /// Attempts to restart the game.
    /// </summary>
    public void AgainButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    /// <summary>
    /// Records the player's progress by updating their score, money, and fish count.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c> if the progress is successfully recorded; otherwise, <c>false</c> if an error occurs.
    /// </returns>
    bool RecordTheProgress()
    {
        try
        {
            int score = GameEndStatus.Instance.levelResults["TotalScore"];
            int money = GameEndStatus.Instance.levelResults["TotalMoney"];
            int fish = GameEndStatus.Instance.levelResults["TotalFish"];

            PlayerProgress.Instance.AddFish(fish);
            PlayerProgress.Instance.AddMoney(money);
            PlayerProgress.Instance.AddScore(score);

            return true; // Return true if all operations succeed.
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while recording progress: {ex.Message}");
            return false; // Return false if any exception is caught. 
        }
    }


    /// <summary>
    /// Plays a sound using the specified audio clip and settings.
    /// </summary>
    /// <param name="clip">The audio clip to be played.</param>
    /// <param name="volume">The volume level at which the clip should be played. Default is 1f.</param>
    /// <param name="priority">The priority level of the audio source. Default is 128.</param>
    /// <param name="pitch">The pitch level at which the clip should be played. Default is 1f.</param>
    public void PlaySound(AudioClip clip, float volume = 1f, int priority = 128, float pitch = 1f)
    {
        audioSource.clip = clip;

        audioSource.volume = volume;
        audioSource.priority = priority;
        audioSource.pitch = pitch;

        audioSource.Play();
    }

    public void ResetGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void QuitGame() => Application.Quit();

    public void GoToHomeScreen() => SceneManager.LoadScene("Home");
}