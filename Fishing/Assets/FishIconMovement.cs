using System.Collections;
using UnityEngine;

public class FishIconMovement : MonoBehaviour
{
    [SerializeField] private RectTransform scoreBar;
    [SerializeField] private GameObject successIconPrefab;
    [SerializeField] private Canvas canvas;

    public void ShowSuccessIcon(Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        GameObject successIcon = Instantiate(successIconPrefab, screenPosition, Quaternion.identity, canvas.transform);

        StartCoroutine(MoveIconToXPBar(successIcon.GetComponent<RectTransform>()));
    }

    private IEnumerator MoveIconToXPBar(RectTransform icon)
    {
        float duration = 1f;
        Vector3 startPosition = icon.position;
        Vector3 andPosition = scoreBar.position;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            icon.position = Vector3.Lerp(startPosition, andPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        icon.position = andPosition;

        Destroy(icon.gameObject);
    }
}
