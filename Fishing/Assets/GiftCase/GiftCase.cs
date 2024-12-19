using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GiftCase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private GiftCaseItemData giftCaseItemData;

    [Header("Gift Cell")]
    [SerializeField] private GameObject giftCellPrefab;
    [SerializeField] private Transform giftCellParent;
    [SerializeField] private int giftCellCount = 5;
    
    private List<GiftItem> giftItems = new List<GiftItem>();
    private List<Transform> giftCells = new List<Transform>();

    private GiftCaseAnim giftCaseAnim;

    void Start()
    {
        giftCaseAnim = GetComponent<GiftCaseAnim>();
    }

    public void OpenGiftCase()
    {
        CreateGiftItemCell();
    }

void CreateGiftItemCell()
{
    // Runtime'da kullanılacak geçici bir liste oluştur
    giftItems = new List<GiftItem>(giftCaseItemData.GiftItems);

    for (int i = 0; i < giftCellCount; i++)
    {
        // Yeni bir GiftCell oluştur
        GameObject giftCell = Instantiate(giftCellPrefab, giftCellParent);
        GiftItemCell giftItemCell = giftCell.GetComponent<GiftItemCell>();

        // giftItems listesinden rastgele bir eleman seç
        int index = Random.Range(0, giftItems.Count);
        GiftItem giftItem = giftItems[index];

        // GiftCell'e bu elemanı ata
        giftItemCell.SetGiftItem(giftItem);

        // GiftCell'i listeye ekle
        giftCells.Add(giftCell.transform);
    }

    // Animasyonu başlat
    StartCoroutine(ReturnGiftCells());
}

    IEnumerator ReturnGiftCells()
    {
        foreach (Transform giftCell in giftCells)
        {
            Quaternion startRotation = giftCell.rotation;
            Quaternion endRotation = Quaternion.Euler(0, 0, 0);
            float elapsedTime = 0;
            float duration = 0.5f; // Animation duration in seconds

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                giftCell.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
                yield return null;
            }

            // Ensure the final rotation is set to the end rotation
            giftCell.rotation = endRotation;
        }
    }
}