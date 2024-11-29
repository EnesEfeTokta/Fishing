using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    // Reference to the TextMeshPro text field that displays the timer value on the UI.
    [SerializeField] private TMP_Text timer;

    private bool isGameFinished = false;

    // Stores the elapsed time in seconds.
    float time;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame to keep track of the time and update the timer display.
    void Update()
    {
        if (isGameFinished) return;

        // Increment the elapsed time by the time passed since the last frame (delta time).
        time += Time.deltaTime;

        // Convert the elapsed time to an integer to display whole seconds.
        // Update the timer text on the UI with the converted value.
        timer.text = Convert.ToInt32(time).ToString();
    }

    /// <summary>
    /// Returns the current elapsed time as a float value.
    /// </summary>
    /// <returns>The elapsed time in seconds.</returns>
    public float InstantTime()
    {
        return time;
    }

    public void GameFinished()
    {
        isGameFinished = true;
        timer.text = "Game Finished!";
        time = 0;
    }
}