using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishHealthBarUI : MonoBehaviour
{
    // Reference to the UI Image component that visually represents the fish's health.
    [SerializeField] private Image bar;

    /// <summary>
    /// Updates the health bar value based on the current health and maximum health of the fish.
    /// </summary>
    /// <param name="health">The current health value of the fish.</param>
    /// <param name="maxHealth">The maximum health value of the fish.</param>
    public void EditHealthBarValue(float health, float maxHealth)
    {
        // Calculate the normalized health value (a value between 0 and 1).
        float healthBarValue = health / maxHealth;

        // Start the coroutine to smoothly animate the health bar change.
        StartCoroutine(SlowHealthBar(healthBarValue, 0.5f));
    }

    // Smoothly transitions the health bar to the target value over the specified duration.
    IEnumerator SlowHealthBar(float targetValue, float duration)
    {
        // Store the initial fill amount of the health bar.
        float startValue = bar.fillAmount;

        // Initialize the elapsed time to zero.
        float elapsedTime = 0f;

        // Gradually update the health bar's fill amount over the given duration.
        while (elapsedTime < duration)
        {
            // Increase the elapsed time by the amount of time passed since the last frame.
            elapsedTime += Time.deltaTime;

            // Calculate the new fill amount using linear interpolation (Lerp).
            bar.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            // Wait until the next frame before continuing the loop.
            yield return null;
        }

        // Ensure the health bar reaches the exact target value at the end of the animation.
        bar.fillAmount = targetValue;
    }
}