using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    // TMP_Text 's will be printed in player data.
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text fish;

    // Scriptableobject where the data to be printed will be withdrawn.
    private PlayerProgress playerProgress;

    void Start()
    {
        ReadAndPrintProfileData();
    }

    // Reads User Data and Prints the TMP_text Type Texts.
    public void ReadAndPrintProfileData()
    {
        // PlayerProgress compensate is assigned.
        playerProgress = GetComponent<PlayerProgress>();

        // The method is running to compensate for the data.
        playerProgress.TextPrintPlayerProgressData(score, money, fish);
    }
}