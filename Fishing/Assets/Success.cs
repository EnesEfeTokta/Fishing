using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Success : MonoBehaviour
{
    [SerializeField] private TMP_Text successName;
    [SerializeField] private TMP_Text successDescription;
    [SerializeField] private Image successImage;

    public void SuccessShow(SuccessData successData)
    {
        successName.text = successData.name;
        successDescription.text = successData.description;
        successImage.sprite = successData.successImage;
    }
}
