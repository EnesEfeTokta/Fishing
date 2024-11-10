using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public (List<GameObject>, List<FishData>) ReadFishCreatAndDeadList()
    {
        return (null, null);
    }
}