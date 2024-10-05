using System.Collections;
using UnityEngine;

public class FishIconMovement : MonoBehaviour
{
    // Reference to the RectTransform of the XP bar where the icon will move towards.
    [SerializeField] private RectTransform scoreBar;

    // Prefab of the success icon that will be instantiated when the player achieves success.
    [SerializeField] private GameObject successIconPrefab;

    // Reference to the canvas where the success icon will be displayed.
    [SerializeField] private Canvas canvas;

    // Reference to the Score script to increase the player's score.
    private Score score;

    void Start()
    {
        // Getting the Score component attached to this GameObject.
        score = GetComponent<Score>();
    }

    // This method shows a success icon at the world position where a fish was killed.
    public void ShowSuccessIcon(Vector3 worldPosition)
    {
        // Convert the world position of the fish to screen position.
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Instantiate the success icon at the screen position and parent it to the canvas.
        GameObject successIcon = Instantiate(successIconPrefab, screenPosition, Quaternion.identity, canvas.transform);

        // Start the coroutine to smoothly move the success icon towards the XP bar.
        StartCoroutine(MoveIconToXPBar(successIcon.GetComponent<RectTransform>()));
    }

    // This coroutine smoothly moves the success icon towards the XP bar.
    IEnumerator MoveIconToXPBar(RectTransform icon)
    {
        // Duration of the movement animation.
        float duration = 1f;

        // Start position of the icon.
        Vector3 startPosition = icon.position;

        // Target position is the XP bar's position.
        Vector3 andPosition = scoreBar.position;

        // Elapsed time for the movement.
        float elapsedTime = 0;

        // While the movement hasn't completed (elapsedTime < duration), continue moving the icon.
        while (elapsedTime < duration)
        {
            // Linearly interpolate between the start and end position based on the elapsed time.
            icon.position = Vector3.Lerp(startPosition, andPosition, elapsedTime / duration);

            // Increment elapsed time by the time passed since last frame.
            elapsedTime += Time.deltaTime;

            // Wait until the next frame to continue the loop.
            yield return null;
        }

        // Ensure the icon reaches the exact final position.
        icon.position = andPosition;

        // Increase the player's score by 1 after the movement.
        score.ScoreIncrease(1);

        // Destroy the icon GameObject after it reaches the target.
        Destroy(icon.gameObject);
    }
}
