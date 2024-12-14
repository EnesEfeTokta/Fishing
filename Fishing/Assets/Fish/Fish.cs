using UnityEngine;

// The required components for the fish object are added.
// These components include properties like health, movement points, and collision.
[RequireComponent(typeof(HealthFish))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Emoji))]
[RequireComponent(typeof(EmojiDetermination))]
[RequireComponent(typeof(FishDamage))]
[RequireComponent(typeof(FishHealthBarUI))]
[RequireComponent(typeof(FishMovement))]
public class Fish : MonoBehaviour
{
    // The identity information of the fish will be received here.
    private FishData fishData; // Holds the identity data of the fish

    private LevelInformationData levelInformationData; // Holds level-related data for the fish

    private HealthFish healthFish; // Reference to the fish's health component

    private InstantiateFish instantiateFish; // Reference to the fish instantiating logic

    /// <summary>
    /// Initializes the fish object by fetching the required components.
    /// </summary>
    void Start()
    {
        healthFish = GetComponent<HealthFish>(); // Get the HealthFish component attached to this fish
    }

    /// <summary>
    /// Starts the fish by assigning its identity and level information.
    /// Called when the fish object is initialized with data.
    /// </summary>
    /// <param name="fishData">The identity data for the fish.</param>
    /// <param name="levelInformationData">Level-specific data related to the fish.</param>
    /// <param name="instantiateFish">The instantiation logic for the fish object.</param>
    public void StartFish(FishData fishData, LevelInformationData levelInformationData, InstantiateFish instantiateFish)
    {
        // Check if the identity information has been received.
        if (fishData == null && levelInformationData == null)
        {
            return;
        }

        this.fishData = fishData; // Assign the fish identity data
        this.levelInformationData = levelInformationData; // Assign level-related data
        this.instantiateFish = instantiateFish; // Assign fish instantiation logic
    }

    /// <summary>
    /// Reads the current fish data.
    /// </summary>
    /// <param name="fishData">Optional parameter to override the current fish data.</param>
    /// <returns>The current fish data.</returns>
    public FishData ReadFishData(FishData fishData = null)
    {
        return fishData = this.fishData; // Return the fish identity data
    }

    /// <summary>
    /// Reads the instantiation data of the fish.
    /// </summary>
    /// <param name="instantiateFish">Optional parameter to override the instantiation logic.</param>
    /// <returns>The instantiation logic of the fish.</returns>
    public InstantiateFish ReadInstantiateFish(InstantiateFish instantiateFish = null)
    {
        return instantiateFish = this.instantiateFish; // Return the fish instantiation logic
    }

    /// <summary>
    /// Reads the level information data.
    /// </summary>
    /// <param name="levelInformationData">Optional parameter to override the level data.</param>
    /// <returns>The current level information data.</returns>
    public LevelInformationData ReadLevelInformationData(LevelInformationData levelInformationData)
    {
        return levelInformationData = this.levelInformationData; // Return the level information data
    }

    /// <summary>
    /// Processes the damage taken by the fish and modifies its health accordingly.
    /// </summary>
    /// <param name="damage">The amount of damage the fish takes.</param>
    public void ProcessDamageClaim(float damage)
    {
        healthFish.EditHealth(damage); // Adjust the health of the fish
    }

    /// <summary>
    /// Handles the fish's death by destroying its game object.
    /// This method is called when the fish reaches zero health.
    /// </summary>
    public void Death()
    {
        GameManager.Instance.FishDeath(this.gameObject); // Notify the game manager about the fish's death
    }
}