using UnityEngine;

public class FishSuccess : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform andPoint;

    [Header("Scale")]
    [SerializeField] private Vector3 startScale;
    [SerializeField] private Vector3 andScale;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        ScaleLerp();
    }

    void ScaleLerp()
    {
        transform.position = Vector3.Lerp(transform.position, andPoint.position, 2 * Time.deltaTime);
    }
}
