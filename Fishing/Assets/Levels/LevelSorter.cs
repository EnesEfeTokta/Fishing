using System.Collections.Generic;
using UnityEngine;

public class LevelSorter : MonoBehaviour
{
    [Header("Level Groups")]
    [SerializeField] private List<LevelGroupData> levelGroupDatas = new List<LevelGroupData>();

    [Header("Prefabs")]
    [SerializeField] private GameObject levelGroupCellPrefab;
    [SerializeField] private GameObject levelCellPrefab;
}
