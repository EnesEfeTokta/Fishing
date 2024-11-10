using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementScreen : MonoBehaviour
{
    // Singleton instance of AchievementScreen
    public static AchievementScreen Instance;

    [Header("UI")]
    [SerializeField] private TMP_Text totalScoreText;    // Text component for displaying total score
    [SerializeField] private TMP_Text totalMoneyText;    // Text component for displaying total money
    [SerializeField] private TMP_Text totalFishText;     // Text component for displaying total fish killed
    [SerializeField] private TMP_Text levelNameText;     // Text component for displaying the level name

    [Header("Image")]
    [SerializeField] private List<Image> starImages;     // List of star images used for achievement rating

    [Header("Panel")]
    [SerializeField] private GameObject achievementScreenPanel; // Panel for the achievement screen

    void Awake()
    {
        // Singleton pattern: Ensure only one instance of AchievementScreen exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any extra instances if already one exists
        }
    }

    public void StartAchievementScreen()
    {
        // Show the achievement panel
        achievementScreenPanel.SetActive(true);

        // Display achievements and stars sequentially
        StartCoroutine(DisplayAchievementsSequentially());
        StartCoroutine(StarEnlargement(starImages, ValueCalculation().Item4, 0.5f));
    }

    // Coroutine for displaying achievements one by one
    IEnumerator DisplayAchievementsSequentially()
    {
        var results = ValueCalculation(); // Calculate score, money, and other metrics

        // Show the score with a gradual increase
        yield return StartCoroutine(SlowNumberIncrease(totalScoreText, results.Item1, 0.5f));

        // Show money with a gradual increase
        yield return StartCoroutine(SlowNumberIncrease(totalMoneyText, results.Item2, 0.5f));

        // Show total fish killed with a gradual increase
        yield return StartCoroutine(SlowNumberIncrease(totalFishText, results.Item3, 0.5f));
    }

    // Coroutine to smoothly increase a displayed number over time
    IEnumerator SlowNumberIncrease(TMP_Text text, int targetValue, float duration)
    {
        float elapsedTime = 0f;
        int startValue = 0;

        // Increase value from 0 to target over specified duration
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, elapsedTime / duration));
            text.text = currentValue.ToString();
            yield return null;
        }

        text.text = targetValue.ToString(); // Set final value after reaching target
    }

    // Calculate score, money, total fish killed, and level success type
    (int, int, int, LevelSuccessType) ValueCalculation()
    {
        // Retrieve lists of created and dead fish from GameManager
        List<GameObject> createdFishs = GameManager.Instance.ReadFishCreatAndDeadList().Item1;
        List<FishData> deadFishs = GameManager.Instance.ReadFishCreatAndDeadList().Item2;

        int createdFishsCount = createdFishs.Count;
        int deadFishsCount = deadFishs.Count;

        int score = 0; // Initialize score
        int money = 0; // Initialize money

        // Retrieve level time and elapsed time from the game manager and timer
        float levelTime = GameManager.Instance.ReadLevelInformationData().levelTime;
        float elapsedTime = Timer.Instance.InstantTime();

        // Define time slices for scoring
        float firstQuarter = levelTime / 4;
        float secondQuarter = levelTime / 2;
        float thirdQuarter = 3 * levelTime / 4;

        LevelSuccessType levelSuccessType = LevelSuccessType.Good; // Default success type

        // Calculate rewards based on fish killed and elapsed time
        foreach (FishData fishData in deadFishs)
        {
            if (elapsedTime <= firstQuarter)
            {
                score += fishData.defaultScore;
                money += fishData.defaultMoney;
                levelSuccessType = LevelSuccessType.Wonderful;
            }
            else if (elapsedTime <= secondQuarter)
            {
                score += fishData.defaultScore / 2;
                money += fishData.defaultMoney / 2;
                levelSuccessType = LevelSuccessType.VeryGood;
            }
            else if (elapsedTime <= thirdQuarter)
            {
                score += fishData.defaultScore / 4;
                money += fishData.defaultMoney / 4;
                levelSuccessType = LevelSuccessType.Good;
            }
            else
            {
                score += fishData.defaultScore / 4;
                money += fishData.defaultMoney / 4;
                levelSuccessType = LevelSuccessType.Good;
            }
        }

        // Return calculated values (score, money, fish count, level success type)
        return (score, money, deadFishsCount, levelSuccessType);
    }

    // Coroutine to enlarge stars based on the level success type
    IEnumerator StarEnlargement(List<Image> images, LevelSuccessType levelSuccessType, float duration)
    {
        int starsToEnlarge = 0;

        // Determine number of stars to enlarge based on level success type
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

        // Set all star images to zero scale initially
        foreach (Image image in images)
        {
            image.transform.localScale = Vector3.zero;
        }

        // Gradually enlarge each star up to the required count
        for (int i = 0; i < starsToEnlarge; i++)
        {
            Image image = images[i];
            float elapsedTime = 0f;
            float startValue = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(startValue, 1, elapsedTime / duration);
                image.transform.localScale = Vector3.one * currentValue;
                yield return null;
            }

            image.transform.localScale = Vector3.one; // Set final scale after enlargement
        }
    }

    // Enum to represent level success categories
    enum LevelSuccessType
    {
        Good,
        VeryGood,
        Wonderful
    }
}