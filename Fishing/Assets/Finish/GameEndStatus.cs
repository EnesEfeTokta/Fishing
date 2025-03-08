using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameEndStatus : MonoBehaviour
{
    public static GameEndStatus Instance;

    [Header("Panels")]
    public GameObject successPanel;
    public GameObject failedPanel;
    public GameObject statusPanel;
    private FailedScreen failedScreen;
    private SuccessScreen successScreen;

    public Dictionary<string, int> levelResults = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;

        failedScreen = failedPanel.GetComponent<FailedScreen>();
        successScreen = successPanel.GetComponent<SuccessScreen>();
    }

    public void EndGame(ValueCalculationType type)
    {
        statusPanel.SetActive(true);

        var results = ValueCalculation(type);
        switch (type)
        {
            case ValueCalculationType.Failed:
                FailedScreen(results.Item1, results.Item2, results.Item3, results.Item4);
                break;
            case ValueCalculationType.Success:
                SuccessScreen(results.Item1, results.Item2, results.Item3, results.Item4, results.Item5, results.Item6);
                break;
        }

        levelResults.Add("TotalScore", results.Item2);
        levelResults.Add("TotalFish", results.Item3);
        levelResults.Add("TotalTime", results.Item4);
        levelResults.Add("TotalMoney", results.Item5);
    }

    public (string, int, int, int, int, LevelSuccessType) ValueCalculation(ValueCalculationType type)
    {
        string levelName = GameManager.Instance.ReadLevelInformationData().levelName;

        int score = 0;
        int money = 0;
        float levelTime = GameManager.Instance.ReadLevelInformationData().levelTime;

        List<FishData> deadFishs = GameManager.Instance.ReadFishCreatAndDeadList().Item2;
        float elapsedTime = Timer.Instance.InstantTime();

        int time = Mathf.RoundToInt(elapsedTime);

        float firstQuarter = levelTime / 4;
        float secondQuarter = levelTime / 2;
        float thirdQuarter = 3 * levelTime / 4;

        switch (type)
        {
            case ValueCalculationType.Failed:

                foreach (FishData fishData in deadFishs)
                {
                    if (elapsedTime <= firstQuarter)
                        score += fishData.defaultScore;
                    else if (elapsedTime <= secondQuarter)
                        score += fishData.defaultScore / 2;
                    else if (elapsedTime <= thirdQuarter)
                        score += fishData.defaultScore / 4;
                    else
                        score += fishData.defaultScore / 4;
                }

                return (levelName, score, GameManager.Instance.fishsDeath.Count, time, 0, LevelSuccessType.None); // Return default values for failed calculation.

            case ValueCalculationType.Success:
                LevelSuccessType levelSuccessType = LevelSuccessType.Good; // Default success type.

                // Calculate rewards based on fish killed and elapsed time.
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
            
                return (levelName, score, GameManager.Instance.fishsDeath.Count, time, money, levelSuccessType);
        }

        return (string.Empty, 0, 0, 0, 0, LevelSuccessType.None);
    }

    private void FailedScreen(string levelName, int totalScore, int totalFish, int totalTime, int totalMoney = 0, LevelSuccessType levelSuccessType = LevelSuccessType.None)
    {
        failedScreen.ShowFailedScreen(true);
        failedScreen.SetFailedScreen(this, levelName, totalScore, totalFish, totalTime);
    }

    private void SuccessScreen(string levelName, int totalScore, int totalFish, int totalTime, int totalMoney, LevelSuccessType levelSuccessType)
    {
        successScreen.ShowSuccessScreen(true);
        successScreen.SetSuccessScreen(this, levelName, totalScore, totalFish, totalTime, totalMoney, levelSuccessType);
    }
}

public enum ValueCalculationType
{
    Failed,
    Success
}

public enum LevelSuccessType
{
    None,
    Good,
    VeryGood,
    Wonderful
}