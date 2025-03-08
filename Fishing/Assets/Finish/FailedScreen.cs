using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class FailedScreen : MonoBehaviour
{
    //todo: Burası devam edecek...
    #region  Variables
    [Header("Texts")]
    [SerializeField] private TMP_Text levelNameText;
    [Space]
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text totalFishText;
    [SerializeField] private TMP_Text totalTimeText;

    [Header("Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button goToHomeButton;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip timeSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip fishSound;

    [Header("Settings")]
    [SerializeField] private int waitingTime = 5;
    [SerializeField] private int writeTime = 1;

    private GameEndStatus gameEndStatus;
    #endregion



    #region Start Panel
    public void ShowFailedScreen(bool show)
    {
        gameObject.SetActive(show);
    }

    public void SetFailedScreen(GameEndStatus gameEndStatus, string levelName, int totalScore, int totalFish, int totalTime)
    {
        this.gameEndStatus = gameEndStatus;

        levelNameText.text = levelName; // Display level name.

        ScaleModeAnimateButton(playAgainButton.gameObject);
        MoveModeAnimateButton(goToHomeButton.gameObject);

        StartCoroutine(ShowStatisticsOrder(totalScore, totalFish, totalTime));
    }
    #endregion




    #region  Show Statistics
    private IEnumerator ShowStatisticsOrder(int totalScore, int totalFish, int totalTime)
    {
        yield return new WaitForSeconds(waitingTime); // Wait for a few seconds before displaying the results.

        // Show the level name with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalTimeText, totalTime, Mathf.Clamp(totalTime / 100f * writeTime, 0.5f, 5f), timeSound));

        // Show the score with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalScoreText, totalScore, Mathf.Clamp(totalScore / 100f * writeTime, 0.5f, 5f), scoreSound));

        // Show money with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalFishText, totalFish, Mathf.Clamp(totalFish / 100f * writeTime, 0.5f, 5f), fishSound));
    }

    private IEnumerator SlowNumberIncrease(TMP_Text text, int targetValue, float duration, AudioClip sound)
    {
        float elapsedTime = 0f;
        int startValue = 0;

        audioSource.PlayOneShot(sound); // Play the sound effect.

        // Increase value from 0 to target over specified duration.
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, elapsedTime / duration));
            text.text = currentValue.ToString();
            yield return null;
        }

        text.text = targetValue.ToString(); // Set final value after reaching target.
    }
    #endregion




    #region Buttom Click Events
    private void ScaleModeAnimateButton(GameObject button)
    {
        button.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        button.transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
    }

    private void MoveModeAnimateButton(GameObject button)
    {
        button.transform.DOMoveX(25f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo);
        button.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnPlayAgainButtonClick() => GameManager.Instance.ResetGame();

    private void OnGoToHomeButtonClick() => GameManager.Instance.GoToHome();
    #endregion
}
