using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class SuccessScreen : MonoBehaviour
{
    #region  Variables
    [Header("Texts")]
    [SerializeField] private TMP_Text levelNameText;
    [Space]
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text totalFishText;
    [SerializeField] private TMP_Text totalTimeText;
    [SerializeField] private TMP_Text totalMoneyText;

    [Header("Buttons")]
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button goToHomeButton;

    [Header("Image")]
    [SerializeField] private List<Image> starImages;     // List of star images used for achievement rating.

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip timeSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip fishSound;
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip starSound;

    [Header("Settings")]
    [SerializeField] private int waitingTime = 5;
    [SerializeField] private int writeTime = 1;

    private GameEndStatus gameEndStatus;
    #endregion





    #region Start Panel
    public void ShowSuccessScreen(bool show)
    {
        gameObject.SetActive(show);
    }

    public void SetSuccessScreen(GameEndStatus gameEndStatus, string levelName, int totalScore, int totalFish, int totalTime, int totalMoney, LevelSuccessType levelSuccessType)
    {
        this.gameEndStatus = gameEndStatus;

        levelNameText.text = levelName; // Display level name.

        ScaleModeAnimateButton(playAgainButton.gameObject);
        MoveModeAnimateButton(goToHomeButton.gameObject);

        StartCoroutine(ShowStatisticsOrder(totalScore, totalFish, totalTime, totalMoney, levelSuccessType));
    }
    #endregion





    #region  Show Statistics
    private IEnumerator ShowStatisticsOrder(int totalScore, int totalFish, int totalTime, int totalMoney, LevelSuccessType levelSuccessType)
    {
        yield return new WaitForSeconds(waitingTime); // Wait for a few seconds before displaying the results.

        // Set achievement rating based on level success type.
        StartCoroutine(StarEnlargement(starImages, levelSuccessType, 0.7f));

        // Show the level name with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalTimeText, totalTime, Mathf.Clamp(totalTime / 100f * writeTime, 0.5f, 5f), timeSound));

        // Show the score with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalScoreText, totalScore, Mathf.Clamp(totalScore / 100f * writeTime, 0.5f, 5f), scoreSound));

        // Show money with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalFishText, totalFish, Mathf.Clamp(totalFish / 100f * writeTime, 0.5f, 5f), fishSound));

        // Show money with a gradual increase.
        yield return StartCoroutine(SlowNumberIncrease(totalMoneyText, totalMoney, Mathf.Clamp(totalMoney / 100f * writeTime, 0.5f, 5f), moneySound));
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




    #region Star Enlargement
    // Coroutine to enlarge stars based on the level success type.
    private IEnumerator StarEnlargement(List<Image> images, LevelSuccessType levelSuccessType, float duration)
    {
        int starsToEnlarge = 0;

        // Determine number of stars to enlarge based on level success type.
        switch (levelSuccessType)
        {
            case LevelSuccessType.Good:
                starsToEnlarge = 1;
                break;
            case LevelSuccessType.VeryGood:
                starsToEnlarge = 2;
                break;
            case LevelSuccessType.Wonderful:
                starsToEnlarge = 3;
                break;
        }

        // Set all star images to zero scale initially.
        foreach (Image image in images)
        {
            image.transform.localScale = Vector3.zero;
        }

        // Gradually enlarge each star up to the required count.
        for (int i = 0; i < starsToEnlarge; i++)
        {
            Image image = images[i];
            float elapsedTime = 0f;
            float startValue = 0;

            GameManager.Instance.PlaySound(starSound); // Oyant your star sound.

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(startValue, 1, elapsedTime / duration);
                image.transform.localScale = Vector3.one * currentValue;

                yield return null;
            }

            image.transform.localScale = Vector3.one; // Set final scale after enlargement.
        }
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