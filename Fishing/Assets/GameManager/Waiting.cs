using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Waiting : MonoBehaviour
{
    [Header("UI")]
    // UI elements for the waiting bar value, the waiting time text, and the full waiting bar.
    [SerializeField] private Image waitBarValue;
    [SerializeField] private TMP_Text waitTime;
    [SerializeField] private Image waitBar;

    void Start()
    {
        // Initially, the wait bar is hidden and the waiting time text is cleared.
        waitBar.gameObject.SetActive(false);
        waitTime.text = "";
    }

    // Public method to trigger the waiting process.
    // Takes in a target value for the wait bar and the duration of the wait.
    public void TriggerWait(float value, float duration)
    {
        // Start the coroutine to fill the wait bar over time.
        StartCoroutine(WaitLerp(value, duration));
    }

    // Coroutine to smoothly fill the wait bar over a specified duration.
    IEnumerator WaitLerp(float targetValue, float duration)
    {
        // Activate the wait bar UI.
        waitBar.gameObject.SetActive(true);

        // Reset the wait bar and text values to the starting state.
        waitBarValue.fillAmount = 0;
        waitBarValue.color = Color.red;
        waitTime.text = "0";

        // Store the initial value of the wait bar (should be 0 at the start).
        float startValue = waitBarValue.fillAmount;

        // Track the elapsed time for the lerp.
        float elapsedTime = 0;

        // While the elapsed time is less than the total duration, update the wait bar and text.
        while (elapsedTime < duration)
        {
            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            // Update the waiting time text to display the elapsed seconds.
            waitTime.text = Convert.ToInt32(elapsedTime).ToString();

            // Smoothly interpolate the fill amount of the wait bar.
            waitBarValue.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            // Change the color of the wait bar and time text based on the current fill amount.
            if (waitBarValue.fillAmount < 0.3f)
            {
                waitBarValue.color = Color.red;
                waitTime.color = Color.red;
            }
            else if (waitBarValue.fillAmount < 0.7f)
            {
                waitBarValue.color = Color.yellow;
                waitTime.color = Color.yellow;
            }
            else
            {
                waitBarValue.color = Color.green;
                waitTime.color = Color.green;
            }

            // Wait for the next frame before continuing.
            yield return null;
        }

        // Once the waiting is complete, hide the wait bar and clear the waiting time text.
        waitBar.gameObject.SetActive(false);
        waitTime.text = "";
    }
}