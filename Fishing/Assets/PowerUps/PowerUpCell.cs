using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PowerUpCell : MonoBehaviour
{
    private List<PowerUpsData> powerUpDatas = new List<PowerUpsData>();

    [Header("UI")]
    [SerializeField] private TMP_Text powerUpCountText;
    [SerializeField] private Image powerUpIconImage;
    [SerializeField] private Image powerUpLockIconImage;
    [SerializeField] private Button powerUpButton;

    public void SetPowerUpData(List<PowerUpsData> powerUpDatas, Sprite sprite)
    {
        this.powerUpDatas = powerUpDatas;
        StartCell(sprite);
    }

    void StartCell(Sprite sprite)
    {
        // Set up the UI elements for the power-up cell.
        powerUpCountText.text = powerUpDatas.Count.ToString();
        powerUpIconImage.sprite = sprite;

        powerUpLockIconImage.gameObject.SetActive(false);

        // Set up the button event listener.
        powerUpButton.onClick.AddListener(OnPowerUpButtonClicked);
    }

    public void OnPowerUpButtonClicked()
    {
        if (powerUpDatas.Count > 0)
        {
            if (powerUpDatas.Count == 1)
            {
                // If count is 1, directly set lock icon visible and disable button.
                powerUpDatas.RemoveAt(0);
                powerUpCountText.text = powerUpDatas.Count.ToString();
                SetButtonInactive();
            }
            else
            {
                // If count > 1, proceed with regeneration animation.
                StartCoroutine(RefreshButtonActive(powerUpDatas[0].powerUpRegenerationTime));
                powerUpDatas.RemoveAt(powerUpDatas.Count - 1);
                powerUpCountText.text = powerUpDatas.Count.ToString();
            }
        }
    }

    IEnumerator RefreshButtonActive(float duration)
    {
        powerUpButton.interactable = false;
        powerUpLockIconImage.gameObject.SetActive(true);
        powerUpLockIconImage.fillAmount = 0; // Start fill amount at 0.

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            powerUpLockIconImage.fillAmount = Mathf.Lerp(1, 0, elapsedTime / duration); // Lerp to 0 fill.
            yield return null;
        }

        // Only enable button if count > 0.
        if (powerUpDatas.Count > 0)
        {
            powerUpButton.interactable = true;
            powerUpLockIconImage.gameObject.SetActive(false); // Hide lock image.
        }
        else
        {
            SetButtonInactive();
        }
    }

    void SetButtonInactive()
    {
        // Set lock icon fully visible and disable the button.
        powerUpLockIconImage.fillAmount = 1; // Fully filled (visible).
        powerUpLockIconImage.gameObject.SetActive(true);
        powerUpButton.interactable = false;
    }

    public List<PowerUpsData> ReadPowerUpDatas()
    {
        return powerUpDatas;
    }
}