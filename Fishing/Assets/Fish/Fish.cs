using UnityEngine;

// The required components for the fish object are added.
// These components include properties like health, movement points, and collision.
[RequireComponent(typeof(HealthFish))]
[RequireComponent(typeof(CreatePoint))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Emoji))]
[RequireComponent(typeof(FishDamage))]

public class Fish : MonoBehaviour
{
    // The identity information of the fish will be received here.
    [HideInInspector] public FishData fishData;

    // Assigning the CreatePoint component that generates the movement points for the fish.
    private CreatePoint createPoint;

    // The duration of the movement between two points for the fish.
    [SerializeField] private float journeyTime = 2f;
    // The start time of the movement.
    private float startTime;

    [Header("Times")]
    // The minimum and maximum time intervals for the fish's movement repetition.
    [SerializeField] private float minRepeatTime = 1f;
    [SerializeField] private float maxRepeatTime = 5f;
    // The time for movement repetition.
    private float repeatTime;

    [Header("Particles/VFX")]
    // The particle system used to play visual effects (e.g., info or indicator particles) during the fish's movement.
    [SerializeField] private ParticleSystem info;
    // The color of the particle system.
    [SerializeField] private Color infoColor;

    // Controls whether the particle effect has been triggered or not.
    private bool hasTriggeredInfo = false;

    //The identity information of the fish will be run after entering.
    public void StartFish(FishData fishData)
    {
        // It is checked whether the identity information has been received.
        if (fishData == null)
        {
            return;
        }

        this.fishData = fishData;

        // The CreatePoint component is retrieved, which determines the two points where the fish will move.
        createPoint = GetComponent<CreatePoint>();

        // Main settings of the particle system are retrieved and the color of the particles is set.
        var main = info.main;
        main.startColor = infoColor;
        
        // The start time of the movement is recorded.
        startTime = Time.time; 

        GetFishMaxHealth(fishData.maxHealth);
    }

    void GetFishMaxHealth(float value)
    {
        HealthFish healthFish = GetComponent<HealthFish>();
        healthFish.HealthValueAssignment(value);
    }

    // To read the identity of the fish.
    public FishData ReadFishData()
    {
        return fishData;
    }

    void Update()
    {
        // As time progresses, the time value 't' is calculated for movement (a value between 0 and 1).
        float t = (Time.time - startTime) / journeyTime;

        // If the fish has not reached its target yet (t <= repeatTime), parabolic movement is calculated.
        if (t <= repeatTime)
        {
            // The current position of the fish is determined by the parabolic movement function.
            Vector3 currentPos = ParabolicMovement(createPoint.startPoint, createPoint.andPoint, t);
            transform.position = currentPos;

            // After 30% of the fish's movement is complete, the info particle effect is triggered.
            if (t >= 0.3f && !hasTriggeredInfo)
            {
                // The particle system is activated.
                info.Play();
                hasTriggeredInfo = true;
            }
        }
        else
        {
            // Once the movement is complete, a new repeat time is set, and the fish is assigned a new target point.
            repeatTime = Random.Range(minRepeatTime, maxRepeatTime);
            startTime = Time.time;
            createPoint.PointMaker();
            hasTriggeredInfo = false;
        }

        // The fish is always oriented to face the target point where it is moving.
        transform.LookAt(createPoint.andPoint);
    }

    // A parabolic movement function between two points. This creates the effect of the fish jumping as it moves.
    Vector3 ParabolicMovement(Vector3 startPoint, Vector3 andPoint, float t)
    {
        // The direction vector between the target point and the starting point is calculated.
        Vector3 direction = andPoint - startPoint;

        // Height for the parabolic movement is set.
        float height = 2.0f;

        // A linear movement position is calculated.
        Vector3 newPos = Vector3.Lerp(startPoint, andPoint, t);

        // The Y-component of the parabolic movement is calculated and added to the new position.
        float parabola = 4 * height * t * (1 - t);

        // The Y-component of the parabola is added to the position.
        newPos.y += parabola;

        // The new position is returned.
        return newPos;
    }
}