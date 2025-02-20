using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public static Message Instance; // Singleton instance to allow global access to this class.

    [Header("UI")]
    [SerializeField] private Image messageIcon; // The icon displayed in the message.
    [SerializeField] private TMP_Text messageName; // The title or name of the message.
    [SerializeField] private TMP_Text messageDescription; // The description or body text of the message.
    [SerializeField] private Image messageBackground; // The background image of the message that contains the name and description text.

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

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource; // Audio source to play sounds.
    [SerializeField] private AudioClip lessImportantSound; // Sound for less important messages.
    [SerializeField] private AudioClip middleImportantSound; // Sound for moderately important messages.
    [SerializeField] private AudioClip veryImportantSound; // Sound for very important messages.

    private Queue<(string name, string description, MessageStatus status, float time)> messageQueue = new Queue<(string, string, MessageStatus, float)>(); // Queue to hold messages
    private bool isMessageShowing = false; // Flag to check if a message is currently being shown.

    void Awake()
    {
        // Singleton pattern: Ensure there is only one instance of Message.
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        animator = messagePanel.GetComponent<Animator>(); // Get the animator component.
    }

    // Method to add a new message to the queue.
    public void NewMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        messageQueue.Enqueue((messageName, messageDescription, messageStatus, showTime)); // Add the message to the queue.
        if (!isMessageShowing)
        {
            StartCoroutine(ShowMessageCoroutine()); // Start showing messages if not currently showing.
        }
    }

    // Coroutine to manage the display of messages from the queue.
    IEnumerator ShowMessageCoroutine()
    {
        isMessageShowing = true; // Set the flag indicating a message is being shown.

        while (messageQueue.Count > 0)
        {
            var (name, description, status, time) = messageQueue.Dequeue(); // Get the next message from the queue.
            yield return StartCoroutine(ShowMessage(name, description, status, time)); // Show the message.
        }
        isMessageShowing = false; // Reset the flag after processing all messages.
    }

    // Coroutine to display a message.
    IEnumerator ShowMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        messagePanel.SetActive(true); // Show the message panel.
        switch (messageStatus)
        {
            case MessageStatus.LessImportant:
                this.messageIcon.sprite = lessImportantSprite; // Set the appropriate icon.
                break;
            case MessageStatus.MiddleImportant:
                this.messageIcon.sprite = middleImportantSprite; // Set the appropriate icon.
                break;
            case MessageStatus.VeryImportant:
                this.messageIcon.sprite = veryImportantSprite; // Set the appropriate icon.
                
                break;
        }

        ColorAdjusting(messageStatus); // Adjust colors.

        PlaySound(messageStatus); // Play the appropriate sound.

        this.messageName.text = messageName; // Set the message title.
        this.messageDescription.text = messageDescription; // Set the message description.

        animator.SetBool("isOpen", true); // Play the open animation.
        yield return new WaitForSeconds(showTime); // Wait for the message display time.

        animator.SetBool("isOpen", false); // Play the close animation.
        yield return new WaitForSeconds(1.5f); // Wait for a short time to ensure the animation is completed.

        messagePanel.SetActive(false); // Hide the message panel.
    }

    // Auxiliary function for setting the message name, description text and background colors.
    void ColorAdjusting(MessageStatus messageStatus)
    {
        switch (messageStatus)
        {
            case MessageStatus.LessImportant:
                messageName.color = lessImportantColor[0]; // Set the less important name text color.
                messageDescription.color = lessImportantColor[1]; // Set the less important description text color.
                messageBackground.color = lessImportantColor[2]; // Set the less important background color.
                break;
            case MessageStatus.MiddleImportant:
                messageName.color = middleImportantColor[0]; // Set the moderately important name text color.
                messageDescription.color = middleImportantColor[1]; // Set the moderately important description text color.
                messageBackground.color = middleImportantColor[2]; // Set the moderately important background color.
                break;
            case MessageStatus.VeryImportant:
                messageName.color = veryImportantColor[0]; // Set the very important name text color.
                messageDescription.color = veryImportantColor[1]; // Set the very important description text color.
                messageBackground.color = veryImportantColor[2]; // Set the very important background color.
                break;
        }
    }

    // Method to play the appropriate sound based on the message's importance level.
    void PlaySound(MessageStatus messageStatus)
    {
        switch (messageStatus)
        {
            case MessageStatus.LessImportant:
                audioSource.PlayOneShot(lessImportantSound); // Play the less important sound.
                break;
            case MessageStatus.MiddleImportant:
                audioSource.PlayOneShot(middleImportantSound); // Play the moderately important sound.
                break;
            case MessageStatus.VeryImportant:
                audioSource.PlayOneShot(veryImportantSound); // Play the very important sound.
                break;
        }
    }
}

// Enum to categorize the message's importance level.
public enum MessageStatus
{
    LessImportant,
    MiddleImportant,
    VeryImportant
}