using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image messageIcon;
    [SerializeField] private TMP_Text messageName;
    [SerializeField] private TMP_Text messageDescription;

    [Header("Message Panel")]
    [SerializeField] private GameObject messagePanel;
    private Animator animator;

    [Header("Message Status Images")]
    [SerializeField] private Sprite lessImportantSprite;
    [SerializeField] private Sprite middleImportantSprite;
    [SerializeField] private Sprite veryImportantSprite;

    void Start()
    {
        messagePanel.SetActive(false);
        animator = messagePanel.GetComponent<Animator>();
    }

    public void ShowMessage(string messageName, string messageDescription, MessageStatus messageStatus, float showTime)
    {
        switch (messageStatus)
        {
            case MessageStatus.LessImportant:
                this.messageIcon.sprite = lessImportantSprite;
                TextColorAdjusting(Color.green, Color.green);
                break;

            case MessageStatus.MiddleImportant:
                this.messageIcon.sprite = middleImportantSprite;
                TextColorAdjusting(Color.yellow, Color.yellow);
                break;

            case MessageStatus.VeryImportant:
                this.messageIcon.sprite = veryImportantSprite;
                TextColorAdjusting(Color.red, Color.red);
                break;
        }

        this.messageName.text = messageName;
        this.messageDescription.text = messageDescription;

        StartCoroutine(MessageActivityStatus(showTime));
    }

    IEnumerator MessageActivityStatus(float time)
    {
        messagePanel.SetActive(true);
        animator.SetBool("isOpen", true);

        yield return new WaitForSeconds(time);

        animator.SetBool("isOpen", false);

        yield return new WaitForSeconds(2);

        messagePanel.SetActive(false);
    }

    void TextColorAdjusting(Color color1, Color color2)
    {
        messageName.color = color1;
        messageDescription.color = color2;
    }
}

public enum MessageStatus
{
    LessImportant,
    MiddleImportant,
    VeryImportant,
    Error, // It will be determined whether it will be used according to the need!
    Warning, // It will be determined whether it will be used according to the need!
    Positive // It will be determined whether it will be used according to the need!
}
