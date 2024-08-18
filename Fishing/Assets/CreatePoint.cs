using UnityEditor;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{
    [Header("Area Settings")]
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeigth;

    [HideInInspector] public Vector3 startPoint = Vector3.zero;
    [HideInInspector] public Vector3 andPoint = Vector3.zero;

    void Start()
    {
        startPoint = transform.position;
        PointMaker();
    }

    public void PointMaker()
    {
        startPoint = andPoint;
        andPoint = Vector3.zero;

        float x = Random.Range(minWidth, maxWidth);
        float z = Random.Range(minHeight, maxHeigth);

        andPoint = new Vector3(x, 0, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPoint, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(andPoint, 0.5f);
    }
}
