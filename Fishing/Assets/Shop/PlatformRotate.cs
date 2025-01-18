using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformRotate : MonoBehaviour
{
    [SerializeField] private Transform platformTransform;

    private float rotationSpeed = 100f; // Rotation speed factor
    private float smoothTime = 0.2f; // Smooth time factor

    private float currentRotationSpeed;
    private float rotationVelocity;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        float targetRotationSpeed = 0f;

        // Check for mouse input
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        if (lookInput != Vector2.zero)
        {
            targetRotationSpeed = -lookInput.x * rotationSpeed;
        }

        // Check for touch input
        Vector2 touchInput = inputActions.Player.Touch.ReadValue<Vector2>();
        if (touchInput != Vector2.zero)
        {
            targetRotationSpeed = -touchInput.x * rotationSpeed;
        }

        // Smoothly interpolate the rotation speed
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeed, ref rotationVelocity, smoothTime);

        // Apply the rotation
        platformTransform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
    }
}