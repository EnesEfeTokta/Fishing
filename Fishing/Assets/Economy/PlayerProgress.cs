using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private PlayerProgressData playerProgressData;

    [SerializeField] private  int totalPlayerScore;
    [SerializeField] private  int totalPlayerMoney;
    [SerializeField] private  int totalPlayerFish;

    void Start()
    {
        totalPlayerScore = playerProgressData.totalPlayerScore;
        totalPlayerMoney = playerProgressData.totalPlayerMoney;
        totalPlayerFish = playerProgressData.totalPlayerFish;
    }

    public void AddMoney(int count)
    {
        playerProgressData.totalPlayerMoney += count;
    }

    public void DecreasingMoney(int count)
    {
        playerProgressData.totalPlayerMoney -= count;
    }

    public void AddScore(int count)
    {
        playerProgressData.totalPlayerScore += count;
    }

    public void AddFish(int count)
    {
        playerProgressData.totalPlayerFish += count;
    }
}