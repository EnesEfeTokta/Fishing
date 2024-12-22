using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using TMPro;

public class GiftCase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private GiftCaseItemData giftCaseItemData;

    [Header("Animation")]
    [SerializeField] private GameObject caseAnimation;
    private GiftCaseAnim giftCaseAnim;

    [Header("Gift Cell")]
    [SerializeField] private GameObject giftCellPrefab;
    [SerializeField] private Transform giftCellParent;
    [SerializeField] private int giftCellCount = 5;

    [Header("Gift Panel")]
    [SerializeField] private GameObject giftPanel;
    [SerializeField] private Button saveAndCloseButton;
    [SerializeField] private Image saveAndCloseImage;
    [SerializeField] private Sprite accelerateSprite;
    [SerializeField] private Sprite closeSprite;

    [Header("Audio")]
    [SerializeField] private AudioClip giftCardAudio;
    [SerializeField] private AudioClip coinSound;

    [Header("Gift Case Button and Count")]
    [SerializeField] private Button giftCaseButton;
    [SerializeField] private TMP_Text giftCaseCountText;

    [Header("GameObjects")]
    [SerializeField] private GameObject environment;

    [Header("Gift Type Icon")]
    [SerializeField] private GameObject giftTypeIconPrefab;
    [SerializeField] private Transform giftTypeIconParent;
    [SerializeField] private RectTransform targetProfileTransform;

    private List<GiftItem> giftItemDatas = new List<GiftItem>();
    private List<GiftItem> giftItems = new List<GiftItem>();
    private List<GameObject> giftCells = new List<GameObject>();
    private List<Sprite> giftTypeIcons = new List<Sprite>();

    private Tween saveAndCloseButtonTween;
    private bool isQuickTransitionAnimation = false;

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        environment.SetActive(true);
        giftPanel.SetActive(false);

        giftCaseAnim = GetComponent<GiftCaseAnim>();

        giftCaseButton.onClick.AddListener(StartGiftCaseAnimation);
        saveAndCloseButton.onClick.AddListener(SaveAndClose);
        saveAndCloseButton.onClick.AddListener(QuickTransitionAnimation);

        UpdateGiftCaseCount();
    }

    void ResetAnimationsAndCoroutines()
    {
        DOTween.KillAll(); // Stop Dotween animations.
        isQuickTransitionAnimation = false; // Reset the fast switch flag.
    }

    void StartGiftCaseAnimation()
    {
        ResetAnimationsAndCoroutines(); // Reset for a new start.

        UpdateSaveAndCloseButtonUI(accelerateSprite);

        environment.SetActive(false);
        giftCaseAnim.StartAnimation(this);
    }

    public void OpenGiftCase()
    {
        ResetAnimationsAndCoroutines(); // Animation and Coroutine Reset.
        ClearExistingGiftCells(); // Clean existing cells.
        InitializeGiftItems(); // Restart the award list.

        giftPanel.SetActive(true);
        saveAndCloseButton.interactable = true;
        StartSaveAndCloseButtonAnimation();
        CreateGiftItemCells();
    }

    void CreateGiftItemCells()
    {
        ClearExistingGiftCells();
        InitializeGiftItems();

        for (int i = 0; i < giftCellCount; i++)
        {
            var giftCell = InstantiateGiftCell();
            giftCells.Add(giftCell);
        }

        StartCoroutine(RevealGiftCells());
    }

    void InitializeGiftItems()
    {
        giftItemDatas = new List<GiftItem>(giftCaseItemData.GiftItems);
    }

    void ClearExistingGiftCells()
    {
        foreach (Transform child in giftCellParent)
        {
            Destroy(child.gameObject);
        }
        giftCells.Clear(); // Reset the list.
    }

    GameObject InstantiateGiftCell()
    {
        var giftCell = Instantiate(giftCellPrefab, giftCellParent);
        var giftItemCell = giftCell.GetComponent<GiftItemCell>();
        var randomGiftItem = GetRandomGiftItemByRarity();
        giftItemCell.SetGiftItem(randomGiftItem);

        // Reset the transparency value.
        var canvasGroup = giftCell.GetComponent<GiftItemCell>().GetFrontImage().GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1; // Return the Alpha value.
        }

        return giftCell;
    }

    GiftItem GetRandomGiftItemByRarity()
    {
        var rarityWeights = new Dictionary<GiftCaseRarenessType, int>
        {
            { GiftCaseRarenessType.Basic, 60 },
            { GiftCaseRarenessType.Rare, 25 },
            { GiftCaseRarenessType.Epic, 10 },
            { GiftCaseRarenessType.Legendary, 5 }
        };

        var weightedList = new List<GiftItem>();
        foreach (var giftItem in giftItemDatas)
        {
            var weight = rarityWeights[giftItem.giftCaseRarenessType];
            weightedList.AddRange(Enumerable.Repeat(giftItem, weight));
        }

        // A random award is selected.
        GiftItem selectedGiftItem = weightedList[Random.Range(0, weightedList.Count)];

        // We add the selected award to the `gifTITEFTEMS 'list.
        giftItems.Add(selectedGiftItem);

        return selectedGiftItem;
    }

    IEnumerator RevealGiftCells()
    {
        foreach (var giftCell in giftCells)
        {
            var canvasGroup = giftCell.GetComponent<GiftItemCell>().GetFrontImage().GetComponent<CanvasGroup>();
            yield return FadeOut(canvasGroup, giftCardAudio.length / 2.5f);
        }

        FinalizeGiftReveal();
    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        yield return new WaitForSeconds(1f); // HomeManager.Instance.PlaySound() delay.

        HomeManager.Instance.PlaySound(giftCardAudio);

        float elapsedTime = 0f;
        while (elapsedTime < duration && !isQuickTransitionAnimation)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    void FinalizeGiftReveal()
    {
        UpdateSaveAndCloseButtonUI(closeSprite);
        saveAndCloseButton.interactable = true;
    }

    void QuickTransitionAnimation()
    {
        if (isQuickTransitionAnimation) return;

        isQuickTransitionAnimation = true;

        DOTween.CompleteAll();

        foreach (var giftCell in giftCells)
        {
            var canvasGroup = giftCell.GetComponent<GiftItemCell>().GetFrontImage().GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        FinalizeGiftReveal();
    }

    void SaveAndClose()
    {
        if (!isQuickTransitionAnimation) return;

        SavePlayerProgress();
        CloseGiftPanel();
    }

    /// <summary>
    /// Saves the player's progress by updating their resources and inventory based on the collected gift items.
    /// </summary>
    void SavePlayerProgress()
    {
        UpdateGiftCaseCount();

        foreach (GiftItem giftItem in giftItems)
        {
            switch (giftItem.giftCaseType)
            {
                case GiftCaseType.Money:
                    playerProgressData.totalPlayerMoney += giftItem.moneyAmount * giftItem.giftCount;
                    giftTypeIcons.Add(giftItem.giftTypeIcon);
                    break;

                case GiftCaseType.PowerUps:
                    for (int i = 0; i < giftItem.giftCount; i++)
                    {
                        playerProgressData.powerUpsDatas.Add(giftItem.powerUpsData);
                        giftTypeIcons.Add(giftItem.giftTypeIcon);
                    }
                    break;

                case GiftCaseType.SpearCoating:
                    for (int i = 0; i < giftItem.giftCount; i++)
                    {
                        playerProgressData.materialDatas.Add(giftItem.spearCoatingMaterial);
                        giftTypeIcons.Add(giftItem.giftTypeIcon);
                    }
                    break;

                case GiftCaseType.SpearObject:
                    for (int i = 0; i < giftItem.giftCount; i++)
                    {
                        playerProgressData.spearObjectDatas.Add(giftItem.spearObject);
                        giftTypeIcons.Add(giftItem.giftTypeIcon);
                    }
                    break;
            }
        }

        playerProgressData.giftCaseItemDatas.Remove(giftCaseItemData);
    }


    void CloseGiftPanel()
    {
        giftPanel.SetActive(false);
        caseAnimation.SetActive(false);
        environment.SetActive(true);
        isQuickTransitionAnimation = false;

        StartCoroutine(CreateGiftTypeIcon());
    }

    void StartSaveAndCloseButtonAnimation()
    {
        saveAndCloseButtonTween?.Kill();

        var animation = isQuickTransitionAnimation
            ? saveAndCloseButton.transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo)
            : saveAndCloseButton.transform.DOMoveX(25f, 1f).SetRelative().SetLoops(-1, LoopType.Yoyo);

        saveAndCloseButtonTween = animation;
    }

    void UpdateGiftCaseCount()
    {
        int giftCaseCount = playerProgressData.giftCaseItemDatas.Count;
        if (giftCaseCount > 0)
        {
            giftCaseCountText.text = giftCaseCount.ToString();
            GiftCaseCountAnimation();
        }
        else
        {
            giftCaseCountText.text = "0";
        }
    }

    void UpdateSaveAndCloseButtonUI(Sprite sprite)
    {
        saveAndCloseImage.sprite = sprite;
    }

    void GiftCaseCountAnimation()
    {
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Append(giftCaseCountText.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.5f, RotateMode.FastBeyond360))
                        .Append(giftCaseCountText.transform.DOLocalRotate(new Vector3(0, 0, -20), 0.5f, RotateMode.FastBeyond360))
                        .SetLoops(-1, LoopType.Yoyo) // Infinite loop and return.
                        .SetEase(Ease.Linear);
    }

    IEnumerator CreateGiftTypeIcon()
    {
        foreach (Sprite sprite in giftTypeIcons)
        {
            RectTransform newIcon = Instantiate(giftTypeIconPrefab, giftTypeIconParent).GetComponent<RectTransform>();
            newIcon.GetComponent<Image>().sprite = sprite;

            // Duration of the movement animation.
            float duration = 0.5f;

            // Start position of the icon.
            Vector3 startPosition = giftCaseButton.transform.position;

            // Elapsed time for the movement.
            float elapsedTime = 0;

            // While the movement hasn't completed (elapsedTime < duration), continue moving the icon.
            while (elapsedTime < duration)
            {
                // Linearly interpolate between the start and end position based on the elapsed time.
                newIcon.position = Vector3.Lerp(startPosition, targetProfileTransform.position, elapsedTime / duration);

                // Increment elapsed time by the time passed since last frame.
                elapsedTime += Time.deltaTime;

                // Wait until the next frame to continue the loop.
                yield return null;
            }

            HomeManager.Instance.PlaySound(coinSound);

            // Ensure the icon reaches the exact final position.
            newIcon.position = targetProfileTransform.position;

            // Destroy the icon GameObject after it reaches the target.
            Destroy(newIcon.gameObject);
        }
    }
}