using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [Header("Data")]
    // Scriptable object where the data to be printed will be withdrawn.
    [SerializeField] private PlayerProgressData playerProgressData;

    [Header("UI")]
    // TMP_Text 's will be printed in player data.
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text fish;

    void Start()
    {
        ReadTheData();
    }

    // Reads User Data and Prints the TMP_text Type Texts.
    public void ReadTheData()
    {
        ReadPlayerProgressData(score, money, fish);
    }

    // Reads User Data and Prints the TMP_text Type Texts.
    void ReadPlayerProgressData(TMP_Text score, TMP_Text money, TMP_Text fish)
    {
        score.text = playerProgressData.totalPlayerScore.ToString();
        money.text = playerProgressData.totalPlayerMoney.ToString();
        fish.text = playerProgressData.totalPlayerFish.ToString();
    }
}