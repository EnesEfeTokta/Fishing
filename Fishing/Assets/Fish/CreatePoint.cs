using UnityEngine;

public class CreatePoint : MonoBehaviour
{
    [Header("Area Settings")]
    [SerializeField] private float minWidth; // Minimum width for random point generation.
    [SerializeField] private float maxWidth; // Maximum width for random point generation.

    [SerializeField] private float minHeight; // Minimum height for random point generation.
    [SerializeField] private float maxHeigth; // Maximum height for random point generation.

    [HideInInspector] public Vector3 startPoint = Vector3.zero; // Start point in the area, initially set to zero.
    [HideInInspector] public Vector3 andPoint = Vector3.zero; // End point in the area, initially set to zero.

    void Start()
    {
        startPoint = transform.position; // Set the start point to the object's initial position.
        PointMaker(); // Generate the initial random end point.
    }

    // Method to generate a new random point within the specified area.
    public void PointMaker()
    {
        startPoint = andPoint; // Set the current end point as the new start point.
        andPoint = Vector3.zero; // Reset the end point to zero.

        // Generate random x and z coordinates within the defined range.
        float x = Random.Range(minWidth, maxWidth);
        float z = Random.Range(minHeight, maxHeigth);

        // Set the new end point based on random x and z values, y is 0.
        andPoint = new Vector3(x, 0, z);
    }
}
