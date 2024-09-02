using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emoji : MonoBehaviour
{
    [SerializeField] private List<Sprite> emojis = new List<Sprite>();
    [SerializeField] private Image emojiUI;
    [SerializeField] private GameObject emojiPanel;

    public void ShowEmoji()
    {
        emojiPanel.SetActive(true);

        int randomIndex = Random.Range(0, emojis.Count);

        Sprite selectEmoji = emojis[randomIndex];

        emojiUI.sprite = selectEmoji;
    }

    void CloseEmojiPanel()
    {
        emojiPanel.SetActive(false);
    }
}
