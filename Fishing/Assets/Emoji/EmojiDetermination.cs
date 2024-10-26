using UnityEngine;

public class EmojiDetermination : MonoBehaviour
{
    // Reference to the Emoji component that manages the emoji display.
    private Emoji emoji;

    void Start()
    {
        // Retrieve the Emoji component attached to the same GameObject.
        emoji = GetComponent<Emoji>();
    }

    // Displays a random emoji based on the given emoji type.
    public void EmojiIdentify(EmojiType emojiType)
    {
        emoji.ShowEmoji(emojiType);
    }

    // Displays a specific emoji by index within the given emoji type list.
    public void EmojiIdentify(EmojiType emojiType, int index)
    {
        emoji.ShowEmoji(emojiType, false, index);
    }
}

// Enum to define different types of emojis representing various emotions.
public enum EmojiType
{
    Happy,      // Represents emojis for happy reactions.
    Stressed,   // Represents emojis for stressed reactions.
    Angry       // Represents emojis for angry reactions.
}