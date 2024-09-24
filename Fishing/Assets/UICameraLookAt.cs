using UnityEngine;

public class UICameraLookAt : MonoBehaviour
{
    // Reference to the main camera's transform.
    private Transform cameraTransform;

    void Start()
    {
        // Get the main camera's transform on start.
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Make the object this script is attached to face the camera every frame.
        transform.LookAt(cameraTransform);
    }
}