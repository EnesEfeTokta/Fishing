using System.Collections.Generic;
using UnityEngine;

public class PastPlayManager : MonoBehaviour
{
    [Header("Past Plays Data")]
    [SerializeField] private PastPlaysData pastPlaysData; // Serialized field to link the Past Plays data container.

    private List<PastPlayData> pastPlayDatas = new List<PastPlayData>(); // List to store individual past play data entries.

    [Header("Past Play Object")]
    [SerializeField] private GameObject PastPlayPrefab; // Prefab for displaying past plays.
    [SerializeField] private Transform parent; // Parent transform under which past play objects will be instantiated.

    void Start()
    {
        pastPlayDatas = pastPlaysData.pastPlayDatas; // Initialize the list with data from the serialized container.
        pastPlayDatas.Reverse(); // Reverse the list to change the order of display.

        PastPlaySort(); // Sort and display past plays.
    }

    // Method to sort and instantiate past play UI elements.
    void PastPlaySort()
    {
        foreach (PastPlayData pastPlayData in pastPlayDatas) // Loop through each past play data.
        {
            // Instantiate a new past play UI element as a child of the specified parent.
            PastPlay pastPlay = Instantiate(PastPlayPrefab.GetComponent<PastPlay>(), parent);

            // Initialize the UI element with its respective data.
            pastPlay.ShowPastPlayFeatures(pastPlayData);
        }
    }

    // Method called when the application is quitting.
    void OnApplicationQuit()
    {
        pastPlayDatas.Reverse(); // Reverse the list back to its original order before the application quits.
    }
}