using UnityEngine;

public class MoneyLoadingPanel : MonoBehaviour
{
    [Header("Money Loading Data")]
    [SerializeField] private MoneyLoadingData moneyLoadingData;

    [Header("Cell")]
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject moneyOptionCellPrefab;

    [Header("Panel")]
    [SerializeField] private GameObject moneyLoadingPanel;

    public void PanelOpenClose(bool isOpen)
    {
        moneyLoadingPanel.SetActive(isOpen);

        if(isOpen)
        {
            SaleCorversAreProduced();
        }
    }

    void SaleCorversAreProduced()
    {
        foreach (MoneyOption moneyOption in moneyLoadingData.moneyOptions)
        {
            MoneyOptionCell moneyOptionCell = Instantiate(moneyOptionCellPrefab, parent).GetComponent<MoneyOptionCell>();
            moneyOptionCell.ShowMoneyOption(moneyOption);
        }
    }
}
