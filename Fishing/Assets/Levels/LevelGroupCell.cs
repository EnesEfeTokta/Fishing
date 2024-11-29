
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGroupCell : MonoBehaviour
{
    private LevelGroupData levelGroupData;

    [Header("UI")]
    [SerializeField] private TMP_Text groupTitleText;
    [SerializeField] private Image groupHeadBgImage;
    [SerializeField] private Image groupBodyBgImage;

    private List<LevelInformationData> levelCells = new List<LevelInformationData>();

    [Header("Content")]
    [SerializeField] private Transform content;

    /// <summary>
    /// Sets the level group data and updates the UI elements accordingly.
    /// </summary>
    /// <param name="data">The data representing the level group, including title and color information.</param>
    public void SetLevelGroup(LevelGroupData data)
    {
        levelGroupData = data;

        SetGroupTitle(data.groupTitle);
        SetGroupIndexColor(data.levelTypes.color);
    }


    /// <summary>
    /// Updates the group title text in the UI with the specified title.
    /// </summary>
    /// <param name="title">The title to be displayed as the group title.</param>
    void SetGroupTitle(string title)
    {
        groupTitleText.text = title;
    }


    void SetGroupIndexColor(Color color)
    {
        groupHeadBgImage.color = color;
        groupBodyBgImage.color = color;
    }

    /// <summary>
    /// Creates and initializes level cells based on the level information data.
    /// </summary>
    /// <param name="levelCellPrefab">The prefab to be instantiated for each level cell.</param>
    public void CreateLevelCell(GameObject levelCellPrefab)
    {
        // Get the level information data from the level group data.
        List<LevelInformationData> levelCells = levelGroupData.levelInformationDatas;

        // Iterate through each level information data.
        foreach (LevelInformationData levelInformationData in levelCells)
        {
            // Instantiate the level cell prefab and get its LevelCell component.
            LevelCell newLevelCell = Instantiate(levelCellPrefab, content).GetComponent<LevelCell>();

            // Set the properties of the new level cell.
            newLevelCell.SetLevelCell(levelInformationData, levelGroupData.levelTypes.color);

            // Add the level information data to the list of level cells.
            this.levelCells.Add(levelInformationData);
        }
    }

}
