using UnityEngine;
using System.Collections;

public class FishDamage : MonoBehaviour
{
    // Reference to the FishData ScriptableObject containing attributes of the fish (like health and materials).
    private FishData fishData;

    // Reference to the EmojiDetermination component associated with this GameObject.
    private EmojiDetermination emojiDetermination;
    
    // Reference to the Fish component associated with this GameObject.
    private Fish fish;

    private InstantiateFish instantiateFish;

    // The amount of damage the fish can take when hit.
    private float damage = 0;

    [Header("Materials")]
    // Material to be applied when the fish takes damage.
    [SerializeField] private Material damageMaterial;

    // The original material of the fish to revert to after damage.
    [SerializeField] private Material originalMaterial;

    // Reference to the SkinnedMeshRenderer component, which renders the fish model and allows material changes.
    [SerializeField] private SkinnedMeshRenderer rdr;

    [Header("Particles/VFX")]
    // Particle system used to simulate blood effects when the fish takes damage.
    [SerializeField] private ParticleSystem blood;

    // Called once at the start of the game to initialize components and set up fish data.
    void Start()
    {
        // Get the Fish component attached to this GameObject.
        fish = GetComponent<Fish>();

        // Get the EmojiDetermination component attached to this GameObject.
        emojiDetermination = GetComponent<EmojiDetermination>();

        // Set up the fish's data and initialize materials.
        SetFishData();
    }

    // Fetches the fish's data from the Fish component.
    void SetFishData()
    {
        // Retrieve the FishData ScriptableObject that contains the fish's attributes.
        fishData = fish.ReadFishData(fishData);

        // The material information of the fish is being collected.
        instantiateFish = fish.ReadInstantiateFish(instantiateFish);

        // Set the fish's damage value.
        SetDamage(fishData.damageUnit);

        // Set the materials for normal and damage states.
        SetMaterials();
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float newDamage)
    {
        // Store the damage value from the fish's data.
        damage = newDamage;
    }

    // Sets the original and damage materials for the fish.
    void SetMaterials()
    {
        // Assign the materials from the FishData ScriptableObject.
        damageMaterial = instantiateFish.damageMaterial;
        originalMaterial = instantiateFish.originalMaterial;

        // Set the fish's material to the original material at the start.
        rdr.material = originalMaterial;
    }

    // Applies damage to the fish and triggers a material change along with particle effects.
    void DamageClaim(float damage)
    {
        // Start the coroutine to change the material and play the blood particle effect.
        StartCoroutine(MaterialChange());

        // Notify the Fish component to process the received damage.
        fish.ProcessDamageClaim(damage);

        emojiDetermination.EmojiIdentify(EmojiType.Angry);
    }

    // Coroutine that temporarily changes the fish's material to indicate damage and plays the blood effect.
    IEnumerator MaterialChange()
    {
        // Change the fish's material to the damage material.
        rdr.material = damageMaterial;

        // Play the blood particle effect.
        blood.Play();

        // Wait for 0.2 seconds to allow the player to see the damage effect.
        yield return new WaitForSeconds(0.2f);

        // Revert the material back to the original after the delay.
        rdr.material = originalMaterial;
    }

    // Detects collisions with other objects. Applies damage if the object is tagged as "Spear".
    void OnCollisionEnter(Collision collision)
    {
        // If the fish collides with an object tagged as "Spear", apply damage.
        if (collision.gameObject.CompareTag("Spear"))
        {
            // Apply the specified damage amount to the fish.
            DamageClaim(damage);
        }
    }
}