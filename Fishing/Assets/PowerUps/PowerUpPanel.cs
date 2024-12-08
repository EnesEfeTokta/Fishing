using System.Collections.Generic;
using UnityEngine;

public class PowerUpPanel : MonoBehaviour
{
    [Header("Datas")]
    private List<PowerUpsData> powerUpsDatas = new List<PowerUpsData>(); // list of power ups datas.

    [Header("Prefabs")]
    [SerializeField] private GameObject powerUpCellPrefab; // prefab for power up.

    [Header("Content")]
    [SerializeField] private Transform content; // transform for content.

    private List<GameObject> powerUpCells = new List<GameObject>(); // list of power up cells.

    void Start()
    {
        SetPowerUpsDatas();
    }

    void SetPowerUpsDatas()
    {
        PlayerProgressData playerProgressData = GameManager.Instance.ReadPlayerProgressData();
        powerUpsDatas = playerProgressData.powerUpsDatas;

        CreatePowerUpCells();
    }

    void CreatePowerUpCells()
    {
        // Create separate lists for each power-up type
        List<PowerUpsData> powerUpsData_Speeds = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_HeavyAttacks = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_AddTime = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_UnlimitedThrowing = new List<PowerUpsData>();

        // Categorize power-ups by type
        foreach (PowerUpsData powerUpsData in powerUpsDatas)
        {
            switch (powerUpsData.powerUpType)
            {
                case PowerUpType.Speed:
                    powerUpsData_Speeds.Add(powerUpsData);
                    break;
                case PowerUpType.HeavyAttack:
                    powerUpsData_HeavyAttacks.Add(powerUpsData);
                    break;
                case PowerUpType.AddTime:
                    powerUpsData_AddTime.Add(powerUpsData);
                    break;
                case PowerUpType.UnlimitedThrowing:
                    powerUpsData_UnlimitedThrowing.Add(powerUpsData);
                    break;
            }
        }

        // Instantiate cells for each category
        InstantiatePowerUpCells(powerUpsData_Speeds);
        InstantiatePowerUpCells(powerUpsData_HeavyAttacks);
        InstantiatePowerUpCells(powerUpsData_AddTime);
        InstantiatePowerUpCells(powerUpsData_UnlimitedThrowing);
    }

    void InstantiatePowerUpCells(List<PowerUpsData> powerUpsList)
    {
        GameObject cell = Instantiate(powerUpCellPrefab, content);
        powerUpCells.Add(cell);
        PowerUpCell powerUpCell = cell.GetComponent<PowerUpCell>();
        powerUpCell.SetPowerUpData(powerUpsList, powerUpsList[0].powerUpImage);
    }

    void OnApplicationQuit()
    {
        UpdatePowerUpDatas();
    }

    void UpdatePowerUpDatas()
    {
        // Update power-up datas based on player progress
        PlayerProgressData playerProgressData = GameManager.Instance.ReadPlayerProgressData();
        playerProgressData.powerUpsDatas.Clear();

        foreach (GameObject powerUpsCell in powerUpCells)
        {
            playerProgressData.powerUpsDatas.AddRange(powerUpsCell.GetComponent<PowerUpCell>().ReadPowerUpDatas());
        }
    }
}
