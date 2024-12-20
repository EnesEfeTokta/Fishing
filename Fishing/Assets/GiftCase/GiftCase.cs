using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GiftCase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private GiftCaseItemData giftCaseItemData;

    [Header("Gift Cell")]
    [SerializeField] private GameObject giftCellPrefab;
    [SerializeField] private Transform giftCellParent;
    [SerializeField] private int giftCellCount = 5;

    [Header("Gift Panel")]
    [SerializeField] private GameObject giftPanel;
    [SerializeField] private Button saveAndcloseButton;
    [SerializeField] private Image saveAndcloseImage;
    [SerializeField] Sprite accelerateSprite;
    [SerializeField] Sprite closeSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip giftCardAudio;

    private List<GiftItem> giftItems = new List<GiftItem>();
    private List<GameObject> giftCells = new List<GameObject>();

    private bool isQuickTransitionAnimation = false;

    void Start()
    {
        giftPanel.SetActive(false);
        saveAndcloseButton.onClick.AddListener(() => SaveAndClose());
        saveAndcloseButton.onClick.AddListener(() => QuickTransitionAnimation());
    }

    public void OpenGiftCase()
    {
        giftPanel.SetActive(true);
        saveAndcloseButton.interactable = true;
        CreateGiftItemCell();
    }

    void CreateGiftItemCell()
    {
        // Create a temporary list to be used in Runtime.
        giftItems = new List<GiftItem>(giftCaseItemData.GiftItems);

        foreach (var item in giftCellParent.GetComponentsInChildren<Transform>())
        {
            if (item != giftCellParent)
            {
                Destroy(item.gameObject);
            }
        }

        for (int i = 0; i < giftCellCount; i++)
        {
            // Create a new GiftCell.
            GameObject giftCell = Instantiate(giftCellPrefab, giftCellParent);
            GiftItemCell giftItemCell = giftCell.GetComponent<GiftItemCell>();

            // Select a random item based on rarity weight.
            GiftItem giftItem = GetRandomGiftItemByRarity();

            // Assign to the GiftCell.
            giftItemCell.SetGiftItem(giftItem);

            // Add GiftCell to the list.
            giftCells.Add(giftCell);
        }

        // Start the animation.
        StartCoroutine(TransparentGiftCells());
    }

    GiftItem GetRandomGiftItemByRarity()
    {
        // Assign weight values for each rarity type.
        Dictionary<GiftCaseRarenessType, int> rarityWeights = new Dictionary<GiftCaseRarenessType, int>
    {
        { GiftCaseRarenessType.Basic, 60 },
        { GiftCaseRarenessType.Rare, 25 },
        { GiftCaseRarenessType.Epic, 10 },
        { GiftCaseRarenessType.Legendary, 5 }
    };

        // Create a weighted list of items.
        List<GiftItem> weightedList = new List<GiftItem>();
        foreach (GiftItem giftItem in giftItems)
        {
            int weight = rarityWeights[giftItem.giftCaseRarenessType];
            for (int j = 0; j < weight; j++)
            {
                weightedList.Add(giftItem);
            }
        }

        // Randomly select an item from the weighted list.
        int randomIndex = Random.Range(0, weightedList.Count);
        GiftItem selectedItem = weightedList[randomIndex];

        return selectedItem;
    }

    IEnumerator TransparentGiftCells()
    {
        foreach (GameObject giftCell in giftCells)
        {
            Image giftFrontImage = giftCell.GetComponent<GiftItemCell>().GetFrontImage();
            CanvasGroup canvasGroup = giftFrontImage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            float elapsedTime = 0;
            float duration = giftCardAudio.length; // Animation duration in seconds.

            HomeManager.Instance.PlaySound(giftCardAudio);

            while (elapsedTime < duration && !isQuickTransitionAnimation)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
                yield return null;
            }

            canvasGroup.alpha = 0; // Make the visibility exactly 1.
        }

        saveAndcloseImage.sprite = closeSprite;

        saveAndcloseButton.interactable = true;
    }

    void QuickTransitionAnimation()
    {
        if (!isQuickTransitionAnimation)
        {
            isQuickTransitionAnimation = true;
        }
        else
        {
            return;
        }

        StopCoroutine(TransparentGiftCells());

        foreach (GameObject giftCell in giftCells)
        {
            Image giftFrontImage = giftCell.GetComponent<GiftItemCell>().GetFrontImage();
            CanvasGroup canvasGroup = giftFrontImage.GetComponent<CanvasGroup>();

            HomeManager.Instance.PlaySound(giftCardAudio);

            canvasGroup.alpha = 0; // Make the visibility exactly 1.
        }

        saveAndcloseImage.sprite = closeSprite;

        saveAndcloseButton.interactable = true;
    }

    void SaveAndClose()
    {
        if (!isQuickTransitionAnimation)
        {
            return;
        }

        // The gifts obtained are saved to PlayerProgress.
        Debug.Log("Gifts saved to PlayerProgress.");

        // Close the gift panel.
        giftPanel.SetActive(false);
    }
}