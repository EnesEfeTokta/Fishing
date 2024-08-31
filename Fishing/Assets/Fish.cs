using UnityEngine;

[RequireComponent(typeof(HealthFish))]
[RequireComponent(typeof(CreatePoint))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Fish : MonoBehaviour
{
    private CreatePoint createPoint;
    [SerializeField] private float journeyTime = 2f;
    private float startTime;

    [Header("Times")]
    [SerializeField] private float minRepeatTime = 1f;
    [SerializeField] private float maxRepeatTime = 5f;
    private float repeatTime;

    [Header("Particles/VFX")]
    [SerializeField] private ParticleSystem info;
    [SerializeField] private Color infoColor;

    private bool hasTriggeredInfo = false;

    void Start()
    {
        createPoint = GetComponent<CreatePoint>();

        var main = info.main;
        main.startColor = infoColor;
        
        startTime = Time.time; 
    }

    void Update()
    {
        float t = (Time.time - startTime) / journeyTime;
        if (t <= repeatTime)
        {
            Vector3 currentPos = ParabolicMovement(createPoint.startPoint, createPoint.andPoint, t);
            transform.position = currentPos;

            if (t >= 0.3f && !hasTriggeredInfo)
            {
                info.Play();
                hasTriggeredInfo = true;
            }
        }
        else
        {
            repeatTime = Random.Range(minRepeatTime, maxRepeatTime);
            startTime = Time.time;
            createPoint.PointMaker();
            hasTriggeredInfo = false;
        }

        transform.LookAt(createPoint.andPoint);
    }

    Vector3 ParabolicMovement(Vector3 startPoint, Vector3 andPoint, float t)
    {
        Vector3 direction = andPoint - startPoint;

        float height = 2.0f;

        Vector3 newPos = Vector3.Lerp(startPoint, andPoint, t);

        float parabola = 4 * height * t * (1 - t);

        newPos.y += parabola;

        return newPos;
    }
}