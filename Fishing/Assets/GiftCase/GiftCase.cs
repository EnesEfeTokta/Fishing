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

    private GiftCaseAnim giftCaseAnim;

    private bool isQuickTransitionAnimation = false;

    void Start()
    {
        giftCaseAnim = GetComponent<GiftCaseAnim>();
        giftPanel.SetActive(false);
        saveAndcloseButton.onClick.AddListener(() => QuickTransitionAnimation());
        saveAndcloseButton.onClick.AddListener(() => SaveAndClose());
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
            // Create a new Giftcell.
            GameObject giftCell = Instantiate(giftCellPrefab, giftCellParent);
            GiftItemCell giftItemCell = giftCell.GetComponent<GiftItemCell>();

            // Select a random element from the gifTItems list.
            int index = Random.Range(0, giftItems.Count);
            GiftItem giftItem = giftItems[index];

            // Ata to Giftcell.
            giftItemCell.SetGiftItem(giftItem);

            // Add Giftcell to the list.
            giftCells.Add(giftCell);
        }

        // Start the animation.
        StartCoroutine(TransparentGiftCells());
    }

    IEnumerator TransparentGiftCells()
    {
        foreach (GameObject giftCell in giftCells)
        {
            Image giftFrontImage = giftCell.GetComponent<GiftItemCell>().GetFrontImage();
            CanvasGroup canvasGroup = giftFrontImage.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            float elapsedTime = 0;
            float duration = giftCardAudio.length; // Animation duration in seconds

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