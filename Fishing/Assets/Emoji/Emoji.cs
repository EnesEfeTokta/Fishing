using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour
{
    // Holds references to the various lists of emoji sprites (e.g., happy, stressed, angry).
    [SerializeField] private EmojiSpriteData emojiSpriteData;

    // Reference to the UI Image component where the emoji will be shown.
    [SerializeField] private Image emojiUI;

    // Reference to the UI panel that contains the emoji image.
    [SerializeField] private GameObject emojiPanel;

    void Start()
    {
        // Close the emoji panel by default when the game starts.
        CloseEmojiPanel();
    }

    // Displays an emoji based on the specified type and display mode (random or specific index).
    public void ShowEmoji(EmojiType emojiType, bool isRandomSprite = true, int index = 0)
    {
        // Make the emoji panel visible on the UI.
        emojiPanel.SetActive(true);

        // Choose the emoji sprite based on the type and display it in the UI.
        switch (emojiType)
        {
            case EmojiType.Happy:
                emojiUI.sprite = SpriteRandomSelect(emojiSpriteData.happyEmojis, isRandomSprite, index);
                break;
            case EmojiType.Stressed:
                emojiUI.sprite = SpriteRandomSelect(emojiSpriteData.stressedEmojis, isRandomSprite, index);
                break;
           case EmojiType.Angry:
                emojiUI.sprite = SpriteRandomSelect(emojiSpriteData.angryEmojis, isRandomSprite, index);
                break;
        }

        // Close the emoji panel automatically after 2 seconds.
        Invoke("CloseEmojiPanel", 2f);
    }

    // Selects a sprite either randomly or by specific index based on input parameters.
    Sprite SpriteRandomSelect(List<Sprite> sprites, bool isRandomSprite = true, int index = 0)
    {
        if (isRandomSprite)
        {
            // Select a random emoji from the list.
            int randomIndex = RandomEmojiIndex(sprites.Count);
            Sprite selectEmoji = sprites[randomIndex];
            return selectEmoji;
        }
        else
        {
            // Select a specific emoji from the list based on the provided index.
            Sprite selectEmoji = sprites[index];
            return selectEmoji;
        }
    }

    // Generates a random index within the list count to select a random emoji.
    int RandomEmojiIndex(int listCount)
    {
        return Random.Range(0, listCount);
    }

    // Hides the emoji panel.
    void CloseEmojiPanel()
    {
        emojiPanel.SetActive(false);
    }
}