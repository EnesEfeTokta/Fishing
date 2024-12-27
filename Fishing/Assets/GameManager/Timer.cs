using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] private TMP_Text timer; // The general timer on the game screen.
    [SerializeField] private TMP_Text numberTimer; // Text for the census with animated.

    private float finishedTime = 0; // The time of the game is the end.
    private bool isGameFinished = false; // Flag that controls whether the game is over.
    private bool countdownStarted = false; // Flag to ensure that the countdown counts once.
    private float time; // Time time

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

    public void SetFinishTime(float time)
    {
        finishedTime += time;
    }

    void Update()
    {
        if (isGameFinished) return;

        // When the end of the game is reached.
        if (time >= finishedTime)
        {
            GameFinished();
            return;
        }

        // Increase the passing time.
        time += Time.deltaTime;

        // Start the animation when the last 3 seconds remain.
        if (!countdownStarted && Mathf.FloorToInt(time) >= Mathf.FloorToInt(finishedTime) - 3)
        {
            countdownStarted = true; // To start the countdown once.
            StartCoroutine(Countdown());
        }

        // Update Timer
        timer.text = Mathf.FloorToInt(time).ToString();
    }

    public float InstantTime()
    {
        return time;
    }

    public void GameFinished()
    {
        isGameFinished = true;
        GameManager.Instance.IsLevelFinished();
        timer.text = "-x-";
        time = 0;
    }

    IEnumerator Countdown()
    {
        numberTimer.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--) // Countdown from 3.
        {
            numberTimer.text = i.ToString();
            numberTimer.transform.localScale = Vector3.zero;

            CanvasGroup canvasGroup = numberTimer.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = numberTimer.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 1;

            float elapsedTime = 0f;
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                numberTimer.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / duration);
                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
                yield return null;
            }
        }

        numberTimer.gameObject.SetActive(false);
    }
}
