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
        giftItems.Clear();
        giftItems = giftCaseItemData.giftItems;

        for (int i = 0; i < giftCellCount; i++)
        {
            GameObject giftCell = Instantiate(giftCellPrefab, giftCellParent);
            GiftItemCell giftItemCell = giftCell.GetComponent<GiftItemCell>();

            int index = Random.Range(0, giftItems.Count);
            GiftItem giftItem = giftItems[index];

            giftItemCell.SetGiftItem(giftItem);

            giftCells.Add(giftCell.transform);
        }

        giftCaseItemData.giftItems = giftItems;
        StartCoroutine(ReturnGiftCells());
    }

    IEnumerator ReturnGiftCells()
    {
        foreach (Transform giftCell in giftCells)
        {

            float elapsedTime = 0;
            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime;
                giftCell.Rotate(Vector3.up * Time.deltaTime);
                yield return null;
            }
        }
    }
}