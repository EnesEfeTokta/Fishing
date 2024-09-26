using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthFish : MonoBehaviour
{
    [Header("Materials")]
    // Reference to the material that will be applied when the fish takes damage.
    [SerializeField] private Material damageMaterial;
    
    // Reference to the original material to revert after damage.
    [SerializeField] private Material originalMaterial;

    // Reference to the SkinnedMeshRenderer component to change the material.
    [SerializeField] private SkinnedMeshRenderer rdr;

    [Header("Health")]
    // The fish's initial health value.
    [SerializeField] private float health = 100;
    
    // Internal variable to track the current health value.
    private float healthValue;

    // Reference to the health bar UI element to visually represent health.
    [SerializeField] private Image healthBarValue;

    [Header("Particles/VFX")]
    // Particle system to simulate blood effects when the fish takes damage.
    [SerializeField] private ParticleSystem blood;

    void Start()
    {
        // Initialize the current health to the default starting value.
        healthValue = health;

        // Set the fish's material to the original material at the start.
        rdr.material = originalMaterial;
    }

    void Update()
    {
        // Test code to simulate damage when the space key is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Cause 50 damage for "testing".
            CauseDamage(50);
        }
    }

    // Method to reduce the fish's health by a specified damage amount.
    public void CauseDamage(float damage)
    {
        // Decrease the health value.
        healthValue -= damage;

        // Update the health bar UI based on the new health value.
        healthBarValue.fillAmount = healthValue / 100f;

        // Start the coroutine to briefly change the material and trigger particle effects.
        StartCoroutine(MaterialChange());

        // Check if the fish's health has dropped to zero or below.
        if (healthValue <= 0)
        {
            // Trigger the success icon animation when the fish dies.
            FindFirstObjectByType<FishIconMovement>().ShowSuccessIcon(transform.position);

            // Call the death method to destroy the fish object.
            Death();
        }
    }

    // Coroutine to temporarily change the fish's material and play the blood particle effect.
    IEnumerator MaterialChange()
    {
        // Change the material to the damage material.
        rdr.material = damageMaterial;

        // Play the blood particle effect.
        blood.Play();

        // Wait for 0.2 seconds before changing the material back.
        yield return new WaitForSeconds(0.2f);

        // Revert the material back to the original.
        rdr.material = originalMaterial;
    }

    // Method to handle the fish's death by destroying the game object.
    void Death()
    {
        Destroy(this.gameObject);
    }

    // Detect collisions with other objects.
    void OnCollisionEnter(Collision collision)
    {
        // If the fish collides with a spear (identified by the tag "Spear"), it takes damage.
        if (collision.gameObject.CompareTag("Spear"))
        {
            // Cause 20 damage when hit by a spear.
            CauseDamage(20);
        }
    }
}