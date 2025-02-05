using UnityEngine;
using System.Collections;

public class GiftCaseAnim : MonoBehaviour
{
    // Summary: Handles the gift case animation, including rotation, shaking, lid opening/closing, and visual effects.

    [Header("GameObjects")]
    [SerializeField] private GameObject animationObject; // The parent object of the animation, activated during the animation.
    [SerializeField] private GameObject __MainPanel__; // The main panel object, deactivated during the animation.

    [Header("Animation")]
    [SerializeField] private float animationTime = 5; // Total time the animation runs.
    private bool isAnimating = false; // Tracks whether the animation is currently running.

    [Header("Background")]
    [SerializeField] private MeshRenderer backgroundMeshRenderer; // Renderer for the background to enable scrolling texture animation.
    [SerializeField] private float backgroundSpeed = 0.5f; // Speed of the background texture scrolling.

    [Header("Case")]
    [SerializeField] private Transform caseTransform; // Transform of the case object.
    [SerializeField] private Transform caseLidTransform; // Transform of the case lid object.

    [Space]

    [SerializeField] private float caseRotateDuration = 0; // Duration of the case rotation animation.
    [SerializeField] private float numberCaseRotations = 3; // Number of full rotations during the animation.

    [Space]

    [SerializeField] private float caseLidOpenDuration = 0; // Duration of the lid opening animation.
    [SerializeField] private float caseLidClosedDuration = 0; // Duration of the lid closing animation.
    [SerializeField] private float actionTime; // Time within the shake animation to trigger effects.

    [Header("Shake")]
    [SerializeField] private float caseShakeDuration = 0; // Duration of the case shaking animation.
    [SerializeField] private float shakeValue = 0; // Magnitude of the case shake.

    [Header("Effects")]
    [SerializeField] private Transform starEffectTransform; // Transform for the star effect.
    [SerializeField] private Transform redFireEffectTransform; // Transform for the red fire effect.

    [Space]
    [SerializeField] private float starEffectDuration = 1; // Duration of the star effect animation.
    [SerializeField] private float redFireEffectDuration = 1; // Duration of the red fire effect animation.

    [Space]
    [SerializeField] private Vector3 starEndEffectScale = new Vector3(0.4f, 0.4f, 0.4f); // Final scale of the star effect.
    [SerializeField] private Vector3 redFireEndEffectScale = new Vector3(1.25f, 1.25f, 1.25f); // Final scale of the red fire effect.

    [Header("Audio")]
    [SerializeField] private AudioClip effectSound;
    [SerializeField] private AudioClip giftCaseOpenCloseSound;

    private GiftCase giftCase; // Reference to the gift case object being animated.

    void Start()
    {
        // Initialize by deactivating the animation object.
        animationObject.SetActive(false);
    }

    public void StartAnimation(GiftCase giftCase)
    {
        // Starts the animation sequence for the gift case.
        if (isAnimating)
        {
            Debug.LogWarning("Animation is already running!");
            return;
        }

        animationObject.SetActive(true); // Enable animation object.
        ResetAnimation(); // Reset transforms and effects to initial states.
        StartCoroutine(AnimationManager()); // Begin the animation sequence.
        this.giftCase = giftCase; // Assign the gift case reference.

        __MainPanel__.SetActive(false); // Initial panel is disabled.
    }

    void ResetAnimation()
    {
        // Resets all animated elements to their initial states for reuse.
        caseTransform.rotation = Quaternion.Euler(0, 180, 0);
        caseLidTransform.localRotation = Quaternion.Euler(0, 0, 0);
        starEffectTransform.localScale = Vector3.zero;
        redFireEffectTransform.localScale = Vector3.zero;
        caseTransform.localPosition = Vector3.zero;

        starEffectTransform.gameObject.SetActive(true);
        redFireEffectTransform.gameObject.SetActive(true);
    }

    void Update()
    {
        // Handles background texture animation.
        BackgroundAnimation();
    }

    void BackgroundAnimation()
    {
        // Scrolls the background texture vertically.
        backgroundMeshRenderer.material.mainTextureOffset += Vector2.up * Time.deltaTime * backgroundSpeed;
    }

    IEnumerator AnimationManager()
    {
        // Manages the sequence of animations for the gift case.
        isAnimating = true;

        yield return StartCoroutine(CaseRotateAnimation()); // Rotate the case.

        yield return new WaitForSeconds(animationTime); // Wait for the animation duration.

        yield return StartCoroutine(CaseLidCloseAnimation()); // Close the case lid.

        isAnimating = false;
    }

    IEnumerator CaseRotateAnimation()
    {
        // Rotates the case for a specified duration and number of rotations.
        float totalRotationAngle = 360f * numberCaseRotations;
        float elapsedTime = 0;

        while (elapsedTime < caseRotateDuration)
        {
            elapsedTime += Time.deltaTime;
            float rotationAmountThisFrame = (totalRotationAngle / caseRotateDuration) * Time.deltaTime;
            caseTransform.Rotate(Vector3.up * rotationAmountThisFrame);
            yield return null;
        }

        yield return StartCoroutine(CaseShakeAnimation());
    }

    IEnumerator CaseShakeAnimation()
    {
        // Adds a shaking effect to the case.
        float elapsedTime = 0;
        Vector3 startPosition = caseTransform.position;

        while (elapsedTime < caseShakeDuration)
        {
            elapsedTime += Time.deltaTime;

            float x = Random.Range(-shakeValue, shakeValue);
            float y = Random.Range(-shakeValue, shakeValue);
            float z = Random.Range(-shakeValue, shakeValue);
            caseTransform.localPosition += new Vector3(x, y, z) * Time.deltaTime;

            if (elapsedTime > actionTime)
            {
                HomeManager.Instance.PlaySound(effectSound); // Play the effect sound.

                StartCoroutine(EffectScaleAnimation(starEffectTransform, Vector2.zero, starEndEffectScale, starEffectDuration));
                StartCoroutine(CaseLidOpenAnimation());
                StartCoroutine(EffectScaleAnimation(redFireEffectTransform, Vector2.zero, redFireEndEffectScale, redFireEffectDuration));
                break;
            }

            yield return null;
        }

        giftCase.OpenGiftCase(); // Triggers the gift case opening logic.
    }

    IEnumerator EffectScaleAnimation(Transform targetTransform, Vector3 start, Vector3 end, float duration)
    {
        // Smoothly scales the target effect transform over time.
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            targetTransform.localScale = Vector3.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }

        targetTransform.localScale = end;
    }

    IEnumerator CaseLidOpenAnimation()
    {
        yield return new WaitForSeconds(1f);
        
        HomeManager.Instance.PlaySound(giftCaseOpenCloseSound); // Play the case open sound.

        // Animates the case lid opening.
        float elapsedTime = 0;
        Quaternion startRotation = caseLidTransform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(180, 0, 0);

        while (elapsedTime < caseLidOpenDuration)
        {
            elapsedTime += Time.deltaTime;
            caseLidTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / caseLidOpenDuration);
            yield return null;
        }

        caseLidTransform.localRotation = targetRotation;
    }

    IEnumerator CaseLidCloseAnimation()
    {
        yield return new WaitForSeconds(1f);
        
        HomeManager.Instance.PlaySound(giftCaseOpenCloseSound); // Play the case open sound.

        // Animates the case lid closing and deactivates effects.
        float elapsedTime = 0;
        Quaternion startRotation = caseLidTransform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        StartCoroutine(EffectScaleAnimation(starEffectTransform, starEndEffectScale, Vector2.zero, starEffectDuration));
        StartCoroutine(EffectScaleAnimation(redFireEffectTransform, redFireEndEffectScale, Vector2.zero, redFireEffectDuration));

        while (elapsedTime < caseLidClosedDuration)
        {
            elapsedTime += Time.deltaTime;
            caseLidTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / caseLidClosedDuration);
            yield return null;
        }

        caseLidTransform.localRotation = targetRotation;

        starEffectTransform.gameObject.SetActive(false);
        redFireEffectTransform.gameObject.SetActive(false);

        __MainPanel__.SetActive(true);
    }

    public bool IsAnimating()
    {
        // Returns whether the animation is currently active.
        return isAnimating;
    }
}