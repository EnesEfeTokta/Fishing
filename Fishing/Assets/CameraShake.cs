using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// Variable to hold the camera transform.
    private Transform cameraTransform;

	// Variable to hold the original position of the camera.
    private Vector3 originalPos;

    void Start()
    {
		// Initially set camera transform and original position.
        cameraTransform = transform;
		originalPos = cameraTransform.localPosition;
    }

    // Method used to initialize camera shake.
    // shakeDuration: Duration of the shake.
    // shakeAmount: Shake intensity.
    // decreaseFactor: Decrease factor of the shaking.
	public void CameraShakeStart(float shakeDuration, float shakeAmount, float decreaseFactor)
	{
		// Invalid parameters check.
		if (shakeDuration < 0 || shakeAmount < 0 || decreaseFactor < 0) return;

		// Start Coroutine.
		StartCoroutine(CameraShakeEvent(shakeDuration, shakeAmount, decreaseFactor));
	}

	// Coroutine that performs camera shake.
	IEnumerator CameraShakeEvent(float shakeDuration, float shakeAmount, float decreaseFactor)
	{ 
		// Continue until the shaking time is over.
		while (shakeDuration > 0)
		{
			// Set the camera position to an arbitrary point.
			cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			// Reduce the duration of shaking.
			shakeDuration -= Time.deltaTime * decreaseFactor;

			yield return null;
		}

		// Return the camera to its original position after the shaking stops.
		transform.localPosition = originalPos;
	}
}
