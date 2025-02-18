using System.Collections.Generic;
using UnityEngine;

public class LevelSorter : MonoBehaviour
{
    [Header("Level Groups")]
    [SerializeField] private List<LevelGroupData> levelGroupDatas = new List<LevelGroupData>();

    [Header("Prefabs")]
    [SerializeField] private GameObject levelGroupCellPrefab;
    [SerializeField] private GameObject levelCellPrefab;

    [Header("Content")]
    [SerializeField] private Transform content;

    [Header("Panel")]
    [SerializeField] private GameObject levelsPanel;

    // Function to open or close the settings panel.
    public void PanelOpenClose(bool isOpen)
    {
        levelsPanel.SetActive(isOpen); // Toggles the visibility of the settings panel.

        if (isOpen)
        {
            SortLevels(); // Sort the levels based on their difficulty levels.
        }
        else
        {
            // Clear the content of the panel.
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void SortLevels()
    {
        foreach (LevelGroupData levelGroupData in levelGroupDatas)
        {
            LevelGroupCell levelGroupCell = Instantiate(levelGroupCellPrefab, content).GetComponent<LevelGroupCell>();

            levelGroupCell.SetLevelGroup(levelGroupData);
            levelGroupCell.CreateLevelCell(levelCellPrefab);

            AdjustGroupHeight(levelGroupCell.transform as RectTransform, levelGroupData.levelInformationDatas.Count);
        }
    }
    
    void AdjustGroupHeight(RectTransform groupTransform, int levelCount)
    {
        // Set a height for each level cell (for example, 100F unit height).
        float levelHeight = 100f;

        // Take into account the gap between the levels (for example, 10F unit space).
        float spacing = 40f;

        // Calculate the total group height.
        float totalHeight = (levelCount * levelHeight) + ((levelCount - 1) * spacing);

        // Update the height of Rectransform.
        Vector2 sizeDelta = groupTransform.sizeDelta;
        sizeDelta.x = totalHeight;
        groupTransform.sizeDelta = sizeDelta;
    }
}
