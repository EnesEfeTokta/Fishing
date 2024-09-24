using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Reference to the TextMeshPro text that will display the timer value.
    [SerializeField] private TMP_Text timer;

    // Variable to store the elapsed time.
    private float time;

    void Update()
    {
        // Increment the time by the time passed in the current frame.
        time += Time.deltaTime;

        // Convert the time to an integer and update the text display.
        timer.text = Convert.ToInt32(time).ToString();
    }
}
