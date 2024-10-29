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
    private FishData fishData;

    private LevelInformationData levelInformationData;

    private HealthFish healthFish;

    void Start()
    {
        healthFish = GetComponent<HealthFish>();
    }

    //The identity information of the fish will be run after entering.
    public void StartFish(FishData fishData, LevelInformationData levelInformationData)
    {
        // It is checked whether the identity information has been received.
        if (fishData == null && levelInformationData == null)
        {
            return;
        }

        this.fishData = fishData;
        this.levelInformationData = levelInformationData;

    }

    // To read the identity of the fish.
    public FishData ReadFishData(FishData fishData)
    {
        return fishData = this.fishData;
    }

    public LevelInformationData ReadLevelInformationData(LevelInformationData levelInformationData)
    {
        return levelInformationData = this.levelInformationData;
    }

    public void ProcessDamageClaim(float damage)
    {
        healthFish.EditHealth(damage);
    }

    // Method to handle the fish's death by destroying the game object.
    public void Death()
    {
        Destroy(this.gameObject);
    }
}