using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpPanel : MonoBehaviour
{
    [Header("Datas")]
    private List<PowerUpsData> powerUpDataList = new List<PowerUpsData>(); // list of power ups datas.

    [Header("Prefabs")]
    [SerializeField] private GameObject powerUpCellPrefab; // prefab for power up.

    [Header("Content")]
    [SerializeField] private Transform content; // transform for content.

    [Header("GameObjects")]
    [SerializeField] private GameObject powerUpCellsPanel; // The power ups panel GameObject to show/hide power ups UI.
    [SerializeField] private RectTransform openCloseIcon; // The open close icon for the power ups panel.


    private List<PowerUpCell> powerUpCells = new List<PowerUpCell>(); // list of power up cells.

    private bool isPanelOpen = false; // Tracks whether the power ups panel is open or closed.

    // Function to open or close the power ups panel.
    public void PanelOpenClose()
    {
        isPanelOpen = !isPanelOpen;
        powerUpCellsPanel.SetActive(isPanelOpen);

        if (isPanelOpen)
        {
            SetPowerUpsDatas();
        }
        else
        {
            UpdatePowerUpDatas();
        }

        AnimatePanel();
    }

    // Set power ups datas.
    private void SetPowerUpsDatas()
    {
        PlayerProgressData playerProgressData = GameManager.Instance.ReadPlayerProgressData();
        powerUpDataList = playerProgressData.powerUpsDatas;

        CreatePowerUpCells();
    }

    // Create power up cells.
    private void CreatePowerUpCells()
    {
        ClearPowerUpCells();

        // Create separate lists for each power-up type
        List<PowerUpsData> powerUpsData_Speeds = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_HeavyAttacks = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_AddTime = new List<PowerUpsData>();
        List<PowerUpsData> powerUpsData_UnlimitedThrowing = new List<PowerUpsData>();

        // Categorize power-ups by type
        foreach (PowerUpsData powerUpsData in powerUpDataList)
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
        if (powerUpsData_Speeds.Count > 0)
            InstantiatePowerUpCells(powerUpsData_Speeds);
        if (powerUpsData_HeavyAttacks.Count > 0)
            InstantiatePowerUpCells(powerUpsData_HeavyAttacks);
        if (powerUpsData_AddTime.Count > 0)
            InstantiatePowerUpCells(powerUpsData_AddTime);
        if (powerUpsData_UnlimitedThrowing.Count > 0)
            InstantiatePowerUpCells(powerUpsData_UnlimitedThrowing);
    }

    // Instantiate power up cells for given list of power-up data.
    private void InstantiatePowerUpCells(List<PowerUpsData> powerUpsList)
    {
        PowerUpCell cell = Instantiate(powerUpCellPrefab, content).GetComponent<PowerUpCell>();
        powerUpCells.Add(cell);
        cell.SetPowerUpData(powerUpsList);
    }

    // Clear all power up cells and destroy them.
    private void ClearPowerUpCells()
    {
        foreach (PowerUpCell powerUpCell in powerUpCells)
        {
            Destroy(powerUpCell.gameObject);
        }
        powerUpCells.Clear(); // Reset the list.
    }

    // Save power up datas when the application quits.
    private void OnApplicationQuit()
    {
        UpdatePowerUpDatas();
    }

    // Update power up datas based on player progress.
    private void UpdatePowerUpDatas()
    {
        // Update power-up datas based on player progress
        PlayerProgressData playerProgressData = GameManager.Instance.ReadPlayerProgressData();
        playerProgressData.powerUpsDatas.Clear();

        foreach (PowerUpCell powerUpsCell in powerUpCells)
        {
            playerProgressData.powerUpsDatas.AddRange(powerUpsCell.ReadPowerUpDatas());
        }
    }

    // Animate the power ups panel open and close.
    private void AnimatePanel()
    {
        openCloseIcon.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.LocalAxisAdd);

        RectTransform rectTransform = powerUpCellsPanel.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1, 1); // Set pivot to top-left corner.

        if (isPanelOpen)
        {
            rectTransform.localScale = new Vector3(0, 0, 0); // Start from scale 0.
            rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack); // Animate to scale 1.
        }
        else
        {
            rectTransform.localScale = new Vector3(1, 1, 1); // Start from scale 1.
            rectTransform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InBack); // Animate to scale 0.
        }
    }
}