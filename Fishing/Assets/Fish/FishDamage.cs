using UnityEngine;

public class FishDamage : MonoBehaviour
{
    private HealthFish healthFish;  // Reference to the HealthFish component that manages the fish's health.
    private FishData fishData; // Reference to the FishData ScriptableObject containing fish attributes.
    public float damage; // Amount of damage the fish can take.

    // Initialize references and variables when the game starts.
    void Start()
    {
        // Get the HealthFish component attached to the same GameObject.
        healthFish = GetComponent<HealthFish>();

        // Get the FishData from the Fish component, which contains the fish’s attributes.
        fishData = GetComponent<Fish>().ReadFishData();

        // Store the damage value from the fish's data.
        damage = fishData.damageUnit;
    }

    // Detect collisions with other objects.
    void OnCollisionEnter(Collision collision)
    {
        // If the fish collides with an object tagged as "Spear", apply damage.
        if (collision.gameObject.CompareTag("Spear"))
        {
            // Call the CauseDamage method on the HealthFish component to reduce health.
            healthFish.CauseDamage(damage);
        }
    }
}