using UnityEngine;
using System.Collections;

public class GiftCaseAnim : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float animationTime = 5; // Total duration for the animation sequence.

    [Header("Background")]
    [SerializeField] private MeshRenderer backgroundMeshRenderer; // Reference to the MeshRenderer for the animated background.
    [SerializeField] private float backgroundSpeed = 0.5f; // Speed at which the background scrolls.

    [Header("Case")]
    [SerializeField] private Transform caseTransform; // Reference to the gift case Transform.
    [SerializeField] private Transform caseLidTranform; // Reference to the gift case lid Transform.
    [SerializeField] private float caseRotateDuration = 0; // Duration for rotating the case.
    [SerializeField] private float numberCaseRotations = 3; // Number of full rotations the case will perform.
    [SerializeField] private float caseLidOpenDuration = 0; // Duration for opening the case lid.
    [SerializeField] private float caseLidClosedDuration = 0; // Duration for closing the case lid.
    [SerializeField] private float actionTime; // Time at which effects and lid open animation start.

    [Header("Shake")]
    [SerializeField] private float caseShakeDuration = 0; // Duration for the case shaking animation.
    [SerializeField] private float shakeValue = 0; // Magnitude of the shaking effect.

    [Header("Effects")]
    [SerializeField] private Transform tropicEffectTransform; // Transform for the tropical effect scaling animation.
    [SerializeField] private Transform ligthEffectTransform; // Transform for the light effect scaling animation.
    [SerializeField] private Transform starEffectTransform; // Transform for the star effect.

    [Space]
    [SerializeField] private float tropicEffectDuration = 0; // Duration for the tropical effect scaling.
    [SerializeField] private float lightEffectDuration = 0; // Duration for the light effect scaling.

    [Space]
    [SerializeField] private Vector3 tropicEndEffectScale = new Vector3(0.5f, 0.5f, 0.5f); // Final scale for the tropical effect.
    [SerializeField] private Vector3 ligthEndEffectScale = new Vector3(0.15f, 0.25f, 0.1f); // Final scale for the light effect.

    // Start is called before the first frame update
    void Start()
    {
        // Begin the main animation sequence.
        StartCoroutine(AnimationManager());
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously animate the background.
        BackgroundAnimation();
    }
    
    // Animate the background texture to scroll upwards.
    void BackgroundAnimation()
    {
        backgroundMeshRenderer.material.mainTextureOffset += Vector2.up * Time.deltaTime * backgroundSpeed;
    }

    // Main animation sequence manager.
    IEnumerator AnimationManager()
    {
        // Start rotating the gift case.
        StartCoroutine(CaseRotateAnimation());

        // Wait for the total animation time.
        yield return new WaitForSeconds(animationTime);

        // Start closing the gift case lid.
        StartCoroutine(CaseCloseAnimation());
    }
    
    // Animate the gift case rotation.
    IEnumerator CaseRotateAnimation()
    {
        float totalRotationAngle = 360f * numberCaseRotations; // Calculate the total rotation angle.
        float elapsedTime = 0;

        while (elapsedTime < caseRotateDuration)
        {
            elapsedTime += Time.deltaTime;

            // Rotate the case smoothly over time.
            float rotationAmountThisFrame = (totalRotationAngle / caseRotateDuration) * Time.deltaTime;
            caseTransform.Rotate(Vector3.up * rotationAmountThisFrame);
            yield return null;
        }

        // Proceed to shake the case after rotation.
        StartCoroutine(CaseShakeAnimation());
    }

    // Animate the shaking of the gift case.
    IEnumerator CaseShakeAnimation()
    {
        float elapsedTime = 0;
        Vector3 startPosition = caseTransform.position; // Save the starting position.

        while (elapsedTime < caseShakeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Apply random position changes to simulate shaking.
            float x = Random.Range(-shakeValue, shakeValue);
            float y = Random.Range(-shakeValue, shakeValue);
            float z = Random.Range(-shakeValue, shakeValue);
            caseTransform.localPosition += new Vector3(x, y, z) * Time.deltaTime;

            // Start effects and lid open animation at the specified action time.
            if (elapsedTime < actionTime)
            {
                StartCoroutine(EffectScaleAnimation());
                StartCoroutine(CaseLidOpenAnimation());
                StartCoroutine(CaseLightAnimation());
            }

            yield return null;
        }
    }

    // Animate the scaling of the tropical effect.
    IEnumerator EffectScaleAnimation()
    {
        float elapsedTime = 0;
        
        while (elapsedTime < tropicEffectDuration)
        {
            elapsedTime += Time.deltaTime;
            tropicEffectTransform.localScale = Vector3.Lerp(Vector3.zero, tropicEndEffectScale, elapsedTime / tropicEffectDuration);
            yield return null;
        }

        tropicEffectTransform.localScale = tropicEndEffectScale; // Ensure the final scale is set.
    }

    // Animate the case lid opening.
    IEnumerator CaseLidOpenAnimation()
    {
        float elapsedTime = 0;
        Quaternion startRotation = caseLidTranform.localRotation; // Starting rotation of the lid.
        Quaternion targetRotation = Quaternion.Euler(180, 0, 0); // Target rotation to open the lid.

        while (elapsedTime < caseLidOpenDuration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the lid rotation.
            caseLidTranform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / caseLidOpenDuration);
            yield return null;
        }

        caseLidTranform.localRotation = targetRotation; // Ensure final target rotation.
    }

    // Animate the scaling of the light effect.
    IEnumerator CaseLightAnimation()
    {
        float elapsedTime = 0;
        
        while (elapsedTime < lightEffectDuration)
        {
            elapsedTime += Time.deltaTime;
            ligthEffectTransform.localScale = Vector3.Lerp(Vector3.zero, ligthEndEffectScale, elapsedTime / lightEffectDuration);
            yield return null;
        }

        ligthEffectTransform.localScale = ligthEndEffectScale; // Ensure final scale is set.
    }

    // Animate the case lid closing and disable effects.
    IEnumerator CaseCloseAnimation()
    {
        float elapsedTime = 0;
        Quaternion startRotation = caseLidTranform.localRotation; // Starting rotation of the lid.
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0); // Target rotation to close the lid.
        
        while (elapsedTime < caseLidClosedDuration)
        {
            elapsedTime += Time.deltaTime;
            caseLidTranform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / caseLidClosedDuration);
            yield return null;
        }
        
        caseLidTranform.localRotation = targetRotation; // Ensure the lid is fully closed.

        // Disable all effect objects after animation ends.
        tropicEffectTransform.gameObject.SetActive(false);
        ligthEffectTransform.gameObject.SetActive(false);
        starEffectTransform.gameObject.SetActive(false);
    }
}