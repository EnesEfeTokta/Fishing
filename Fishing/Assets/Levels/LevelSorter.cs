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

    void Start()
    {
        SortLevels();
    }

    void SortLevels()
    {
        foreach (LevelGroupData levelGroupData in levelGroupDatas)
        {
            LevelGroupCell levelGroupCell = Instantiate(levelGroupCellPrefab, content).GetComponent<LevelGroupCell>();

            levelGroupCell.SetLevelGroup(levelGroupData);
            levelGroupCell.CreateLevelCell(levelCellPrefab);
        }
    }
}
