using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PowerUpCell : MonoBehaviour
{
    // List to store the data for each power-up associated with this cell.
    private List<PowerUpsData> powerUpDatas = new List<PowerUpsData>();

    [Header("UI")]
    // Reference to the TextMeshPro component that displays the number of available power-ups.
    [SerializeField] private TMP_Text powerUpCountText;
    // Reference to the Image component that displays the icon of the power-up.
    [SerializeField] private Image powerUpIconImage;
    // Reference to the Image component used as a lock icon when the power-up is unavailable.  This is likely an image that fills up over time.
    [SerializeField] private Image powerUpLockIconImage;
    // Reference to the Button component that triggers the power-up activation.
    [SerializeField] private Button powerUpButton;

    /// <summary>
    /// Initializes the power-up cell with the provided data and sprite.
    /// </summary>
    /// <param name="powerUpDatas">List of PowerUpsData objects representing the power-ups.</param>
    /// <param name="sprite">The sprite to be displayed as the power-up icon.</param>
    public void SetPowerUpData(List<PowerUpsData> powerUpDatas, Sprite sprite)
    {
        this.powerUpDatas = powerUpDatas;
        StartCell(sprite);
    }

    /// <summary>
    /// Sets up the initial state of the power-up cell UI.
    /// </summary>
    /// <param name="sprite">The sprite to display for the power-up.</param>
    void StartCell(Sprite sprite)
    {
        // Display the number of available power-ups.
        powerUpCountText.text = powerUpDatas.Count.ToString();
        // Set the power-up icon.
        powerUpIconImage.sprite = sprite;

        // Initially hide the lock icon.
        powerUpLockIconImage.gameObject.SetActive(false);

        // Add a listener to the button's click event.
        powerUpButton.onClick.AddListener(OnPowerUpButtonClicked);
    }

    /// <summary>
    /// Called when the power-up button is clicked.
    /// </summary>
    public void OnPowerUpButtonClicked()
    {
        // Check if any power-ups are available.
        if (powerUpDatas.Count > 0)
        {
            if (powerUpDatas.Count == 1)
            {
                // If only one power-up is left, remove it, update the count, and deactivate the button.
                powerUpDatas.RemoveAt(0);
                powerUpCountText.text = powerUpDatas.Count.ToString();
                SetButtonInactive();
            }
            else
            {
                // If multiple power-ups are available, start the cooldown animation, remove a power-up,
                // update the count, and implement the power-up effect.
                StartCoroutine(RefreshButtonActive(powerUpDatas[0].powerUpDuration));
                powerUpDatas.RemoveAt(powerUpDatas.Count - 1); //Removes from the end of the List which is probably unintended.
                powerUpCountText.text = powerUpDatas.Count.ToString();

                ImplementPowerUp(powerUpDatas[0].powerUpType);
            }
        }
    }

    /// <summary>
    /// Animates the power-up button cooldown.  Fills up the Lock image and makes the button non-interactable.
    /// </summary>
    /// <param name="duration">The duration of the cooldown.</param>
    /// <returns>IEnumerator for the coroutine.</returns>
    IEnumerator RefreshButtonActive(float duration)
    {
        powerUpButton.interactable = false;
        powerUpLockIconImage.gameObject.SetActive(true);
        powerUpLockIconImage.fillAmount = 0; 

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            powerUpLockIconImage.fillAmount = Mathf.Lerp(1, 0, elapsedTime / duration); // Animate the filling of the image, potentially backwards.
            yield return null;
        }

        // Re-enable the button only if there are more power-ups available.
        if (powerUpDatas.Count > 0)
        {
            powerUpButton.interactable = true;
            powerUpLockIconImage.gameObject.SetActive(false); 
        }
        else
        {
            SetButtonInactive();
        }
    }

    /// <summary>
    /// Deactivates the power-up button and displays the lock icon.
    /// </summary>
    void SetButtonInactive()
    {
        powerUpLockIconImage.fillAmount = 1; 
        powerUpLockIconImage.gameObject.SetActive(true);
        powerUpButton.interactable = false;
    }

    /// <summary>
    /// Returns the list of power-up data associated with this cell.
    /// </summary>
    /// <returns>List of PowerUpsData objects.</returns>
    public List<PowerUpsData> ReadPowerUpDatas()
    {
        return powerUpDatas;
    }

    /// <summary>
    /// Implements the effect of the power-up based on its type.
    /// </summary>
    /// <param name="powerUpType">The type of power-up to be implemented.</param>
    void ImplementPowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                StartCoroutine(SpearThrowingRaiseSpeed(powerUpDatas[0].powerUpValue, powerUpDatas[0].powerUpDuration));
                break;

            case PowerUpType.HeavyAttack:
                StartCoroutine(IncreasingFishDamage(powerUpDatas[0].powerUpValue, powerUpDatas[0].powerUpDuration));
                break;

            case PowerUpType.AddTime:
                Timer.Instance.SetFinishTime(powerUpDatas[0].powerUpValue);
                break;

            case PowerUpType.UnlimitedThrowing:
                StartCoroutine(ReducingThrowingWaitTime(powerUpDatas[0].powerUpValue, powerUpDatas[0].powerUpDuration));
                break;

            default:
                break;
        }
    }

    IEnumerator SpearThrowingRaiseSpeed(int newSpeed, float time)
    {
        // Get the default spear speed.
        float defaultSpeed = SpearThrowing.Instance.ReadSpearSpeed();

        // Increase the spear speed by the power-up value.
        SpearThrowing.Instance.SetSpearSpeed(newSpeed + defaultSpeed);

        // Wait for the duration of the power-up.
        yield return new WaitForSeconds(time);

        // Reset the spear speed to its default value.
        SpearThrowing.Instance.SetSpearSpeed(defaultSpeed);
    }

    IEnumerator IncreasingFishDamage(float newDamage, float time)
    {
        // Get the list of currently active fish.
        List<GameObject> fishsCreated = GameManager.Instance.ReadFishCreatAndDeadList().Item1;

        //BUG: This assumes all fish have the same default damage.  It should read from each fish individually
        float defaultDamage = fishsCreated[0].GetComponent<FishDamage>().GetDamage();

        // Increase the damage of each fish by the power-up value.
        foreach (GameObject fish in fishsCreated)
        {
            fish.GetComponent<FishDamage>().SetDamage(newDamage + defaultDamage);
        }

        // Wait for the duration of the power-up.
        yield return new WaitForSeconds(time);

        // Reset the damage of each fish to its default value.
        foreach (GameObject fish in fishsCreated)
        {
            fish.GetComponent<FishDamage>().SetDamage(defaultDamage);
        }
    }

    IEnumerator ReducingThrowingWaitTime(float newWaitTime, float time)
    {
        // Get the default throwing wait time.
        float defaultWaitTime = SpearThrowing.Instance.ReadThrowingTime();

        // Set the new throwing wait time, which should be a reduction from the default.
        SpearThrowing.Instance.SetThrowingTime(newWaitTime);

        // Wait for the duration of the power-up.
        yield return new WaitForSeconds(time);

        // Reset the throwing wait time to its default value.
        SpearThrowing.Instance.SetThrowingTime(defaultWaitTime);
    }
}