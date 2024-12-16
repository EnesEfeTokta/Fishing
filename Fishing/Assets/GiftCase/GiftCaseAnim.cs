using UnityEngine;
using System.Collections;

public class GiftCaseAnim : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private MeshRenderer backgroundMeshRenderer;
    [SerializeField] private float backgroundSpeed = 0.5f;

    [Header("Case")]
    [SerializeField] private Transform caseTransform;
    [SerializeField] private Transform caseLidTranform;
    [SerializeField] private float caseRotateDuration = 0;
    [SerializeField] private float caseLidOpenDuration = 0;

    [Header("Shake")]
    [SerializeField] private float numberCaseRotations = 3;
    [SerializeField] private float caseShakeDuration = 0;
    [SerializeField] private float shakeValue = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip caseOpenClip;

    [Header("Effect")]
    [SerializeField] private Transform effectTransform;
    [SerializeField] private float effectScaleDuration = 0;
    [SerializeField] private Vector3 endEffectScale = new Vector3(0.5f, 0.5f, 0.5f);


    void Start()
    {
        StartCoroutine(CaseRotateAnimation());
    }

    void Update()
    {
        BackgroundAnimation();
    }
    
    void BackgroundAnimation()
    {
        backgroundMeshRenderer.material.mainTextureOffset += Vector2.up * Time.deltaTime * backgroundSpeed;
    }
    
    IEnumerator CaseRotateAnimation()
    {
        // Pre-calculate the total rotation angle
        float totalRotationAngle = 360f * numberCaseRotations;

        float elapsedTime = 0;
        while (elapsedTime < caseRotateDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the rotation amount for this frame
            float rotationAmountThisFrame = (totalRotationAngle / caseRotateDuration) * Time.deltaTime;

            caseTransform.Rotate(Vector3.up * rotationAmountThisFrame);
            yield return null;
        }

        StartCoroutine(CaseShakeAnimation());
    }

    IEnumerator CaseShakeAnimation()
    {
        float elapsedTime = 0;
        Vector3 startPosition = caseTransform.position;

        while (elapsedTime < caseShakeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Random position changes.
            float x = Random.Range(-shakeValue, shakeValue);
            float y = Random.Range(-shakeValue, shakeValue);
            float z = Random.Range(-shakeValue, shakeValue);

            caseTransform.localPosition += new Vector3(x, y, z) * Time.deltaTime;

            if (elapsedTime < caseShakeDuration / 1.1f)
            {
                StartCoroutine(EffectScaleAnimation());
                StartCoroutine(CaseLidOpenAnimastion());
            }

            yield return null;
        }
    }

    IEnumerator EffectScaleAnimation()
    {
        float elapsedTime = 0;
        
        while (elapsedTime < effectScaleDuration)
        {
            elapsedTime += Time.deltaTime;
            effectTransform.localScale = Vector3.Lerp(Vector3.zero, endEffectScale, elapsedTime / effectScaleDuration);
            yield return null;
        }

        effectTransform.localScale = endEffectScale;
    }

    IEnumerator CaseLidOpenAnimastion()
    {
        float elapsedTime = 0;
        while (elapsedTime < caseLidOpenDuration)
        {
            elapsedTime += Time.deltaTime;
            caseLidTranform.localRotation = Quaternion.Lerp(caseLidTranform.localRotation, Quaternion.Euler(180, 0, 0), elapsedTime / caseShakeDuration);
            yield return null;
        }
    }
}
