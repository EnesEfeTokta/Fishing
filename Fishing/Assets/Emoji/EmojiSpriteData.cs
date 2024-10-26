using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmojiSpriteData", menuName = "ScriptableObject/EmojiSpriteData")]
public class EmojiSpriteData : ScriptableObject
{
    // List of  happy emoji sprites that can be displayed.
    public List<Sprite> happyEmojis = new List<Sprite>();

    [Space]

    // List of angry emoji sprites that can be displayed.
    public List<Sprite> angryEmojis = new List<Sprite>();

    [Space]

    // List of stressed emoji sprites that can be displayed.
    public List<Sprite> stressedEmojis = new List<Sprite>();
}
