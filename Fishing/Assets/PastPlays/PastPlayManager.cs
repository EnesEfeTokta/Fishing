using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOTween namespace.

public class PastPlayManager : MonoBehaviour
{
    [Header("Past Plays Data")]
    [SerializeField] private PastPlaysData pastPlaysData; // Serialized field to link the Past Plays data container.

    private List<PastPlayData> pastPlayDatas = new List<PastPlayData>(); // List to store individual past play data entries.

    [Header("Past Play Object")]
    [SerializeField] private GameObject PastPlayPrefab; // Prefab for displaying past plays.
    [SerializeField] private Transform parent; // Parent transform under which past play objects will be instantiated.
    [SerializeField] private GameObject pastPlayPanel; // Panel to display past plays.

    private bool isOpen = false;

    void Start()
    {
        pastPlayDatas = pastPlaysData.pastPlayDatas; // Initialize the list with data from the serialized container.
        pastPlayDatas.Reverse(); // Reverse the list to change the order of display.

        PastPlaySort(); // Sort and display past plays.

        pastPlayPanel.SetActive(false); // Hide the past play panel.
    }

    public void OpenPastPlayPanel()
    {
        isOpen = !isOpen; // Toggle the isOpen flag.
        if (isOpen)
        {
            pastPlayPanel.SetActive(true); // Show the past play panel.
            AnimatePanelOpen(); // Animate the panel opening.
        }
        else
        {
            pastPlayPanel.SetActive(false); // Hide the past play panel.
        }
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

    // Method to animate the panel opening.
    void AnimatePanelOpen()
    {
        RectTransform rectTransform = pastPlayPanel.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0, 1); // Set pivot to top-left corner.
        rectTransform.localScale = new Vector3(0, 0, 0); // Start from scale 0.
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack); // Animate to scale 1.
    }

    // Method called when the application is quitting.
    void OnApplicationQuit()
    {
        pastPlayDatas.Reverse(); // Reverse the list back to its original order before the application quits.

        new DBSave(pastPlaysData);
    }
}