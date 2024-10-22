using System.Collections.Generic;
using UnityEngine;

public class PastPlayRecorder : MonoBehaviour
{
    private PastPlaysData pastPlaysData;

    private GameManager gameManager;

    [SerializeField] private List<Sprite> pastPlayIcons = new List<Sprite>();

    void Start()
    {
        gameManager = GetComponent<GameManager>();

        if (pastPlaysData == null && pastPlayIcons == null)
        {
            return;
        }

        SetPastPlaysData();
    }

    void SetPastPlaysData()
    {
        pastPlaysData = gameManager.ReadPastPlaysData(pastPlaysData);
    }

    public void AddPastPlayData(string name, 
                                int levelIndex, 

                                float scoreValue, 
                                float fishValue, 
                                float moneyValue, 

                                LevelInformationData levelInformationData
                                )
    {

        PastPlayData newPastPlay = new PastPlayData();
        newPastPlay = CreatePastPlay(newPastPlay, name, levelIndex, scoreValue, fishValue, moneyValue, levelInformationData);

        pastPlaysData.pastPlayDatas.Add(newPastPlay);
    }

    PastPlayData CreatePastPlay(PastPlayData pastPlayData,
    
                                string name, 
                                int levelIndex, 

                                float scoreValue, 
                                float fishValue, 
                                float moneyValue, 

                                LevelInformationData levelInformationData
                                )
    {
        pastPlayData.icon = RandomIcon(pastPlayIcons);
        pastPlayData.name = name;
        pastPlayData.levelIndex = levelIndex;

        pastPlayData.scoreValue = scoreValue;
        pastPlayData.fishValue = fishValue;
        pastPlayData.moneyValue = moneyValue;

        pastPlayData.maxScoreValue = levelInformationData.maxScoreCount;
        pastPlayData.maxFishValue = levelInformationData.totalFishCount;
        pastPlayData.maxMoneyValue = levelInformationData.maxMoneyCount;

        return pastPlayData;
    }

    Sprite RandomIcon(List<Sprite> sprites)
    {
        int randomIndex = Random.Range(0, sprites.Count);
        return sprites[randomIndex];
    }
}
