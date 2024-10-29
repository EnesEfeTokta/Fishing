using UnityEngine;

public class FishMovement : MonoBehaviour
{
    // Data related to fish identity and properties
    private FishData fishData;

    // Data related to the level's environment dimensions
    private LevelInformationData levelInformationData;

    // Fish component reference for data retrieval
    private Fish fish;

    // Base speed for fish movement, will adjust based on FishSpeed
    private float speed = 1;

    // Timestamp marking the start of fish movement
    private float startTime;

    // Randomized interval before next movement cycle
    private float repeatTime;

    // Minimum and maximum values for movement repetition interval
    private float minRepeatTime;
    private float maxRepeatTime;

    // Time taken for fish to travel between two points
    private float journeyTime;

    // Flag to track if info particle effect has been triggered
    private bool hasTriggeredInfo = false;

    // Width and height boundaries for random point generation in area
    private float minWidth;
    private float maxWidth;
    private float minHeight;
    private float maxHeigth;

    [Header("Particles/VFX")]
    [SerializeField] private ParticleSystem info; // Particle system for visual effects during movement

    // Start and end points for movement, initially set to zero
    private Vector3 startPoint = Vector3.zero;
    private Vector3 andPoint = Vector3.zero;

    void Start()
    {
        // Initialize fish component reference
        fish = GetComponent<Fish>();

        // Record the start time for movement
        startTime = Time.time;

        // Read data from fish and level configurations
        ReadFishData();
        ReadLevelInformationData();

        // Generate initial random points within the defined boundaries
        CreatePoints(minWidth, maxWidth, minHeight, maxHeigth);
    }

    // Reads fish data and sets speed and movement-related variables accordingly
    void ReadFishData()
    {
        fishData = fish.ReadFishData(fishData);

        // Adjust speed based on the FishSpeed setting
        FishSpeed fishSpeed = fishData.fishSpeed;
        switch (fishSpeed)
        {
            case FishSpeed.Fast:
                speed *= 1.5f;
                break;
            case FishSpeed.Middle:
                speed *= 1;
                break;
            case FishSpeed.Slow:
                speed *= 0.5f;
                break;
        }

        // Set repeat and journey times for movement cycles
        minRepeatTime = fishData.minRepeatTime;
        maxRepeatTime = fishData.maxRepeatTime;
        journeyTime = fishData.journeyTime / speed;
    }

    // Reads level boundary data to define movement area for the fish
    void ReadLevelInformationData()
    {
        levelInformationData = fish.ReadLevelInformationData(levelInformationData);

        minWidth = levelInformationData.minWidth;
        maxWidth = levelInformationData.maxWidth;
        minHeight = levelInformationData.minHeight;
        maxHeigth = levelInformationData.maxHeigth;
    }

    void Update()
    {
        // Calculate normalized movement progress (t) between 0 and 1
        float t = (Time.time - startTime) / journeyTime;

        // If within the repeat time, update fish's position using parabolic movement
        if (t <= repeatTime)
        {
            Vector3 currentPos = ParabolicMovement(startPoint, andPoint, t);
            transform.position = currentPos;

            // Trigger particle effect if 30% of the journey is completed
            if (t >= 0.3f && !hasTriggeredInfo)
            {
                //info.Play(); // Uncomment to activate particle effect
                hasTriggeredInfo = true;
            }
        }
        else
        {
            // Set new random interval for next cycle and assign a new target point
            repeatTime = Random.Range(minRepeatTime, maxRepeatTime);
            startTime = Time.time;
            CreatePoints(minWidth, maxWidth, minHeight, maxHeigth);
            hasTriggeredInfo = false;
        }

        // Rotate fish to face its target destination
        transform.LookAt(andPoint);
    }

    // Calculates parabolic movement between two points for a jumping effect
    Vector3 ParabolicMovement(Vector3 startPoint, Vector3 andPoint, float t)
    {
        // Height for the parabolic arc
        float height = 2.0f;

        // Linear position interpolated between start and end points
        Vector3 newPos = Vector3.Lerp(startPoint, andPoint, t);

        // Adjust Y-coordinate for parabolic arc effect
        float parabola = 4 * height * t * (1 - t);
        newPos.y += parabola;

        return newPos;
    }

    // Generates random start and end points within specified area boundaries
    public void CreatePoints(float minWidth, float maxWidth, float minHeight, float maxHeigth)
    {
        startPoint = andPoint; // Set current end point as new start point
        andPoint = Vector3.zero; // Reset end point

        // Generate random x and z coordinates within defined boundaries
        float x = Random.Range(minWidth, maxWidth);
        float z = Random.Range(minHeight, maxHeigth);

        // Set the new end point, keeping y-axis at zero
        andPoint = new Vector3(x, 0, z);
    }
}