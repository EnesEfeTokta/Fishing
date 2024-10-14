using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    // Singleton instance to allow global access to this class.
    public static Message Instance;

    [Header("UI")]
    [SerializeField] private Image messageIcon; // The icon displayed in the message.
    [SerializeField] private TMP_Text messageName; // The title or name of the message.
    [SerializeField] private TMP_Text messageDescription; // The description or body text of the message.

    [Header("Message Panel")]
    [SerializeField] private GameObject messagePanel; // The panel that contains the message UI elements.
    private Animator animator; // Animator to handle the message panel's animations.

    [Header("Message Status Images")]
    [SerializeField] private Sprite lessImportantSprite; // Sprite for less important messages.
    [SerializeField] private Sprite middleImportantSprite; // Sprite for moderately important messages.
    [SerializeField] private Sprite veryImportantSprite; // Sprite for very important messages.

    [Header("Colors")]
    [SerializeField] private Color[] lessImportantColor; // Colors for less important messages (name and description text).
    [SerializeField] private Color[] middleImportantColor; // Colors for moderately important messages.
    [SerializeField] private Color[] veryImportantColor; // Colors for very important messages.

    private bool isMessageShow = false; // A flag to check if a message is already being displayed.

    void Awake()
    {
        // Singleton pattern: Ensure there is only one instance of Message.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any extra instances.
        }
    }

    void Start()
    {
        // Initially, the message panel is hidden.
        messagePanel.SetActive(false);
        // Get the animator component from the message panel.
        animator = messagePanel.GetComponent<Animator>();
    }

    // Static method to show a new message globally.
    public static void NewMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        if (Instance != null)
        {
            // Call the non-static ShowMessage() method on the singleton instance.
            Instance.ShowMessage(messageName, messageDescription, messageStatus, showTime);
        }
    }

    // Method to handle displaying a message with the specified status and timing.
    void ShowMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        // Check if a message is already being displayed. If yes, return.
        if (!isMessageShow)
        {
            isMessageShow = true; // Set the flag to indicate a message is being shown.
        }
        else
        {
            return;
        }

        // Determine message appearance based on its importance status.
        switch (messageStatus)
        {
            case MessageStatus.LessImportant:
                this.messageIcon.sprite = lessImportantSprite; // Set appropriate icon.
                TextColorAdjusting(lessImportantColor[0], lessImportantColor[1]); // Adjust text colors.
                break;

            case MessageStatus.MiddleImportant:
                this.messageIcon.sprite = middleImportantSprite;
                TextColorAdjusting(middleImportantColor[0], middleImportantColor[1]);
                break;

            case MessageStatus.VeryImportant:
                this.messageIcon.sprite = veryImportantSprite;
                TextColorAdjusting(veryImportantColor[0], veryImportantColor[1]);
                break;
        }

        // Set the message title and description.
        this.messageName.text = messageName;
        this.messageDescription.text = messageDescription;

        // Start the coroutine to handle the message display timing.
        StartCoroutine(MessageActivityStatus(showTime));
    }

    // Coroutine to control how long the message is shown and handle the animation.
    IEnumerator MessageActivityStatus(float time)
    {
        messagePanel.SetActive(true); // Show the message panel.
        animator.SetBool("isOpen", true); // Play open animation.

        // Wait for the specified time before hiding the message.
        yield return new WaitForSeconds(time);

        // Play close animation.
        animator.SetBool("isOpen", false);

        // Wait for the closing animation to finish before fully hiding the panel.
        yield return new WaitForSeconds(2);

        messagePanel.SetActive(false); // Hide the message panel.

        // Reset the flag so that new messages can be displayed.
        isMessageShow = false;
    }

    // Helper function to adjust the colors of the message's name and description text.
    void TextColorAdjusting(Color color1, Color color2)
    {
        messageName.color = color1;
        messageDescription.color = color2;
    }
}

// Enum to categorize the message's importance level.
public enum MessageStatus
{
    LessImportant,
    MiddleImportant,
    VeryImportant,
    Error, // Optional: Can be used for error messages.
    Warning, // Optional: Can be used for warning messages.
    Positive // Optional: Can be used for positive confirmation messages.
}