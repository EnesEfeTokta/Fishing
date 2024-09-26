using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text successName;
    [SerializeField] private TMP_Text successDescription;
    [SerializeField] private Image successImage;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Object")]
    [SerializeField] private GameObject successPanel;

    void Start()
    {
        successPanel.SetActive(false); // Hide the success panel when the game starts.
    }

    // Method to show the success panel with relevant data.
    public void SuccessShow(SuccessData successData)
    {
        successPanel.SetActive(true); // Activate the success panel.

        // Set the UI text and image based on the success data.
        successName.text = successData.name; 
        successDescription.text = successData.description;
        successImage.sprite = successData.successImage;

        // Trigger the success animation if the animator is assigned.
        if (animator != null)
        {
            animator.SetTrigger("SuccessShow"); 
        }

        // Call StopSuccessShow after 4.3 seconds to hide the panel.
        Invoke("StopSuccessShow", 4.3f);
    }

    // Method to hide the success panel.
    void StopSuccessShow()
    {
        successPanel.SetActive(false);
    }
}
