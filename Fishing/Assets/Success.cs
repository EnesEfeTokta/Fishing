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
        successPanel.SetActive(false);
    }

    public void SuccessShow(SuccessData successData)
    {
        successPanel.SetActive(true);

        successName.text = successData.name;
        successDescription.text = successData.description;
        successImage.sprite = successData.successImage;

        if (animator != null)
        {
            animator.SetTrigger("SuccessShow");
        }

        Invoke("StopSuccessShow", 4.3f);
    }

    void StopSuccessShow()
    {
        successPanel.SetActive(false);
    }
}
