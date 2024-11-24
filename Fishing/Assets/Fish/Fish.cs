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

    private InstantiateFish instantiateFish;

    void Start()
    {
        healthFish = GetComponent<HealthFish>();
    }

    //The identity information of the fish will be run after entering.
    public void StartFish(FishData fishData, LevelInformationData levelInformationData, InstantiateFish instantiateFish)
    {
        // It is checked whether the identity information has been received.
        if (fishData == null && levelInformationData == null)
        {
            return;
        }

        this.fishData = fishData;
        this.levelInformationData = levelInformationData;
        this.instantiateFish = instantiateFish;
    }

    // To read the identity of the fish.
    public FishData ReadFishData(FishData fishData = null)
    {
        return fishData = this.fishData;
    }

    public InstantiateFish ReadInstantiateFish(InstantiateFish instantiateFish = null)
    {
        return instantiateFish = this.instantiateFish;;
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
        GameManager.Instance.FishDeath(this.gameObject);
    }
}