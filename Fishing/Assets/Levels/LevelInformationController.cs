using System.Collections.Generic;
using UnityEngine;

public class LevelInformationController : MonoBehaviour
{
    [Header("Level Information Data")]
    [SerializeField] private LevelInformationData levelInformationData; // Reference to level information data.

    public List<GameObject> createFishs = new List<GameObject>();  // List to store instantiated fish objects.

    [SerializeField] private Transform startPoint;

    // Called when the game starts. Triggers level setup.
    void Start()
    {
        ReadYourLevelInformationAndWork(); // Start reading the level data and spawning fish.
    }

    // Reads the level information and triggers the fish production process.
    void ReadYourLevelInformationAndWork()
    {
        ProduceFish(levelInformationData.fishTypeAndNumbers); // Spawn fish based on the data.
    }

    // Spawns fish according to the specified types and numbers in the level data.
    void ProduceFish(List<FishTypeAndNumber> fishTypeAndNumbers)
    {
        // Iterate through each fish type and its corresponding count.
        foreach (FishTypeAndNumber fishTypeAndNumber in fishTypeAndNumbers)
        {
            // Spawn the required number of fish for each type.
            for (int i = 0; i < fishTypeAndNumber.fishCustom; i++)
            {
                // Select a random fish prefab from the list.
                int randomIndex = Random.Range(0, fishTypeAndNumber.fishData.fishPrefabs.Count);

                // Instantiate the fish prefab at the controller's position with default rotation.
                Fish newFish = Instantiate(
                    fishTypeAndNumber.fishData.fishPrefabs[randomIndex], 
                    startPoint.position, 
                    startPoint.rotation
                ).GetComponent<Fish>();

                // The identity information of the fish is sent.
                newFish.StartFish(fishTypeAndNumber.fishData);

                // Add the newly created fish to the list of spawned fish.
                createFishs.Add(newFish.gameObject);
            }
        }
    }
}
