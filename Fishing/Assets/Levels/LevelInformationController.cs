using System.Collections.Generic;
using UnityEngine;

public class LevelInformationController : MonoBehaviour
{
    public static LevelInformationController Instance;

    // Holds the level-specific data, including fish types and their counts.
    private LevelInformationData levelInformationData;

    // Stores references to the fish GameObjects that are created during the level.
    [HideInInspector] public List<GameObject> fishsCreated = new List<GameObject>();

    // The position where fish will be instantiated.
    [SerializeField] private Transform startPoint;

    void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of LevelInformationController exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    // Called once when the game starts, setting up the level.
    public void StartLevelInformationController()
    {
        // Load the level information data.
        SetLevelInformationData();

        // Begin the process of reading the data and spawning fish.
        ReadYourLevelInformationAndWork();
    }

    /// <summary>
    /// Loads level information from the GameManager.
    /// </summary>
    void SetLevelInformationData()
    {
        levelInformationData = GameManager.Instance.ReadLevelInformationData(levelInformationData);
    }

    /// <summary>
    /// Reads level information and initiates the fish spawning process.
    /// </summary>
    void ReadYourLevelInformationAndWork()
    {
        ProduceFish(levelInformationData.fishTypeAndNumbers); // Spawn fish based on level data.
    }

    /// <summary>
    /// Spawns fish according to the specified types and numbers from the level data.
    /// </summary>
    /// <param name="fishTypeAndNumbers">List of fish types and their respective counts.</param>
    void ProduceFish(List<FishTypeAndNumber> fishTypeAndNumbers)
    {
        // Iterate through each fish type and spawn the specified number of fish.
        foreach (FishTypeAndNumber fishTypeAndNumber in fishTypeAndNumbers)
        {
            for (int i = 0; i < fishTypeAndNumber.fishCustom; i++)
            {
                // Select a random fish prefab from the list of available prefabs.
                int randomIndex = Random.Range(0, fishTypeAndNumber.fishData.spawnFishPrefabs.Count);

                // Instantiate the selected fish at the designated starting point with default rotation.
                Fish newFish = Instantiate(
                    fishTypeAndNumber.fishData.spawnFishPrefabs[randomIndex].spawnFishObject,
                    startPoint.position,
                    startPoint.rotation
                ).GetComponent<Fish>();

                // Initialize the fish with its associated data (e.g., stats, behaviors).
                newFish.StartFish(fishTypeAndNumber.fishData, levelInformationData, fishTypeAndNumber.fishData.spawnFishPrefabs[randomIndex]);

                // Add the newly created fish to the list of spawned fish.
                fishsCreated.Add(newFish.gameObject);
            }
        }

        GameManager.Instance.ReadFishCreatAndDeadList(fishsCreated);
    }
}