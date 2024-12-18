using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftItemCell : MonoBehaviour
{
    public Image giftItemImage;
    public TMP_Text giftItemCountText;

    public void SetGiftItem(GiftItem giftItem)
    {
        giftItemImage.sprite = giftItem.sprite;
        giftItemCountText.text = giftItem.giftCount.ToString();
    }
}
