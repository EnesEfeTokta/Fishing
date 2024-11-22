using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ensures that all necessary components are attached to the GameObject.
[RequireComponent(typeof(PastPlayRecorder))]
[RequireComponent(typeof(Score))]
[RequireComponent(typeof(PlayerProgress))]
[RequireComponent(typeof(SettingsFounder))]
[RequireComponent(typeof(LevelInformationController))]
[RequireComponent(typeof(FishIconMovement))]

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Serialized fields to hold references to various data containers.
    [SerializeField] private PastPlaysData pastPlaysData;
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private LevelInformationData levelInformationData;
    [SerializeField] private SettingsData settingsData;

    public List<FishData> fishsDeath = new List<FishData>(); // List of fish data.
    public List<GameObject> fishsCreated = new List<GameObject>(); // List of fish created.

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

    /// <summary>
    /// Provides access to the past plays data.
    /// </summary>
    /// <param name="pastPlaysData">The data object to be populated.</param>
    /// <returns>Returns the populated PastPlaysData object.</returns>
    public PastPlaysData ReadPastPlaysData(PastPlaysData pastPlaysData = null)
    {
        return pastPlaysData = this.pastPlaysData;
    }

    /// <summary>
    /// Provides access to the player progress data.
    /// </summary>
    /// <param name="playerProgressData">The data object to be populated.</param>
    /// <returns>Returns the populated PlayerProgressData object.</returns>
    public PlayerProgressData ReadPlayerProgressData(PlayerProgressData playerProgressData = null)
    {
        return playerProgressData = this.playerProgressData;
    }

    /// <summary>
    /// Provides access to the level information data.
    /// </summary>
    /// <param name="levelInformationData">The data object to be populated.</param>
    /// <returns>Returns the populated LevelInformationData object.</returns>
    public LevelInformationData ReadLevelInformationData(LevelInformationData levelInformationData = null)
    {
        return levelInformationData = this.levelInformationData;
    }

    /// <summary>
    /// Provides access to the settings data.
    /// </summary>
    /// <param name="settingsData">The data object to be populated.</param>
    /// <returns>Returns the populated SettingsData object.</returns>
    public SettingsData ReadSettingsData(SettingsData settingsData = null)
    {
        return settingsData = this.settingsData;
    }

    public void FishDeath(GameObject fishDeathObject)
    {
        FishData fishData = fishDeathObject.GetComponent<Fish>().ReadFishData();

        // Remove fish object from created list and add its data to death list.
        fishsCreated.Remove(fishDeathObject);
        fishsDeath.Add(fishData);
        Destroy(fishDeathObject);

        IsLevelFinished();
    }

    public (List<GameObject>, List<FishData>) ReadFishCreatAndDeadList(List<GameObject> fishsCreated = null, List<FishData> fishsDeath = null)
    {
        this.fishsCreated = fishsCreated ?? this.fishsCreated;
        this.fishsDeath = fishsDeath ?? this.fishsDeath;

        return (this.fishsCreated, this.fishsDeath);
    }

    public bool IsLevelFinished()
    {
        int totalFishs = ReadLevelInformationData().fishTypeAndNumbers.Count;

        float time = Timer.Instance.InstantTime();

        if (totalFishs == fishsDeath.Count | time >= ReadLevelInformationData().levelTime)
        {
            AchievementScreen.Instance.StartAchievementScreen();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SceneRouterButton(string name)
    {
        SceneManager.LoadScene(name);
    }
}