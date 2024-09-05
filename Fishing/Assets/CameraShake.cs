using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float shakeDuration = 0f;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1f;

    private Vector3 originalPos;

    void Start()
    {
        cameraTransform = transform;
    }

	void Update()
	{
		if (shakeDuration > 0)
		{
			cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			cameraTransform.localPosition = originalPos;
		}
	}
}
