using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftItemCell : MonoBehaviour
{
    public Image giftItemImage;
    public Image giftFrontImage;
    public TMP_Text giftItemCountText;

    public Image GetFrontImage()
    {
        return giftFrontImage;
    }

    public void SetGiftItem(GiftItem giftItem)
    {
        giftItemImage.sprite = giftItem.sprite;
        giftItemCountText.text = $"{giftItem.giftCount}X";
    }
}
