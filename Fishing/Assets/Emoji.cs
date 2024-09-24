using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour
{
    // List of emoji sprites that can be displayed.
    [SerializeField] private List<Sprite> emojis = new List<Sprite>();

    // Reference to the UI image component where the emoji will be displayed.
    [SerializeField] private Image emojiUI;

    // Reference to the UI panel that holds the emoji.
    [SerializeField] private GameObject emojiPanel;

    void Start()
    {
        // Initially close the emoji panel at the start of the game.
        CloseEmojiPanel();
    }

    // This method displays a random emoji on the UI.
    public void ShowEmoji()
    {
        // Enable the emoji panel to make it visible.
        emojiPanel.SetActive(true);

        // Select a random emoji from the list.
        int randomIndex = Random.Range(0, emojis.Count);

        // The selected emoji sprite is assigned to the UI image.
        Sprite selectEmoji = emojis[randomIndex];

        // Display the selected emoji in the UI.
        emojiUI.sprite = selectEmoji;

        // After 2 seconds, hide the emoji panel again.
        Invoke("CloseEmojiPanel", 2f);
    }

    // This method hides the emoji panel.
    void CloseEmojiPanel()
    {
        emojiPanel.SetActive(false);
    }
}