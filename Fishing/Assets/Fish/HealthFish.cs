using UnityEngine;

public class HealthFish : MonoBehaviour
{
    // Reference to the FishData ScriptableObject containing fish attributes.
    private FishData fishData;
    
    // Reference to the FishHealthBarUI component for updating the health bar UI.
    private FishHealthBarUI fishHealthBarUI;

    // Reference to the Fish component that handles fish behavior.
    private Fish fish;

    // Current and maximum health values for the fish.
    private float health = 0;
    private float maxHealth = 0;

    // Called once at the start of the game to initialize the fish's data and health.
    void Start()
    {
        // Get the Fish component from the GameObject.
        fish = GetComponent<Fish>();

        // Get the FishHealthBarUI component for controlling the health bar.
        fishHealthBarUI = GetComponent<FishHealthBarUI>();

        // Set up the fish's data and health.
        SetFishData();
    }

    // Retrieves and sets the fish's data from the Fish component.
    void SetFishData()
    {
        // Retrieve the FishData from the Fish component.
        fishData = fish.ReadFishData(fishData);

        // Set the maximum health for the fish.
        SetFishMaxHealth();
    }

    // Sets the fish's maximum health and initializes the current health.
    void SetFishMaxHealth()
    {
        // Assign the maximum health from the FishData.
        maxHealth = fishData.maxHealth;

        // Initialize the current health to the maximum health.
        health = maxHealth;
    }

    /// <summary>
    /// Reduces the fish's health by a specified amount and updates the health bar. 
    /// If the health drops to 0 or below, it triggers the death sequence.
    /// </summary>
    /// <param name="damage">The amount of damage to subtract from the fish's health.</param>
    public void EditHealth(float damage)
    {
        // Subtract the specified damage from the current health.
        health -= damage;

        // Update the health bar UI to reflect the new health value.
        fishHealthBarUI.EditHealthBarValue(health, maxHealth);

        // Check if the fish's health has dropped to zero or below.
        if (health <= 0)
        {
            // Trigger the success icon animation when the fish dies.
            FindFirstObjectByType<FishIconMovement>().ShowSuccessIcon(transform.position);

            // Call the death method to destroy the fish object.
            fish.Death();
        }
    }
}