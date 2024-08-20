using UnityEngine;

public class Fish : MonoBehaviour
{
    private CreatePoint createPoint;
    [SerializeField] private float journeyTime = 2f;
    private float startTime;

    [SerializeField] private float minRepeatTime = 1f;
    [SerializeField] private float maxRepeatTime = 5f;
    private float repeatTime;

    void Start()
    {
        createPoint = GetComponent<CreatePoint>();
        startTime = Time.time; 
    }

    void Update()
    {
        float t = (Time.time - startTime) / journeyTime;
        if (t <= repeatTime)
        {
            Vector3 currentPos = ParabolicMovement(createPoint.startPoint, createPoint.andPoint, t);
            transform.position = currentPos;
        }
        else
        {
            repeatTime = Random.Range(minRepeatTime, maxRepeatTime);

            startTime = Time.time;
            createPoint.PointMaker();
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