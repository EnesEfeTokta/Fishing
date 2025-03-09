using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class PowerUpCell : MonoBehaviour
{
    // List to store the data for each power-up associated with this cell.
    public List<PowerUpsData> powerUpDatas = new List<PowerUpsData>();

    [Header("UI")]
    // Reference to the TextMeshPro component that displays the number of available power-ups.
    [SerializeField] private TMP_Text powerUpCountText;
    // Reference to the Image component that displays the icon of the power-up.
    [SerializeField] private Image powerUpIconImage;
    // Reference to the Image component used as a lock icon when the power-up is unavailable.  This is likely an image that fills up over time.
    [SerializeField] private Image powerUpLockIconImage;
    // Reference to the Button component that triggers the power-up activation.
    [SerializeField] private Button powerUpButton;

    [Header("Animation")]
    [SerializeField] private GameObject animationPrefab;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private float animationDuration = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip powerUpSound;

    // Dictionary to store default fish damages
    private Dictionary<GameObject, float> defaultFishDamages = new Dictionary<GameObject, float>();

    /// <summary>
    /// Initializes the power-up cell with the provided data and sprite.
    /// </summary>
    /// <param name="powerUpDatas">List of PowerUpsData objects representing the power-ups.</param>
    /// <param name="sprite">The sprite to be displayed as the power-up icon.</param>
    public void SetPowerUpData(List<PowerUpsData> powerUpDatas)
    {
        this.powerUpDatas = powerUpDatas;
        Debug.Log(powerUpDatas[0].powerUpImage.name);
        StartCell(powerUpDatas[0].powerUpImage);
    }

    /// <summary>
    /// Sets up the initial state of the power-up cell UI.
    /// </summary>
    /// <param name="sprite">The sprite to display for the power-up.</param>
    private void StartCell(Sprite sprite)
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
            if (powerUpDatas[0].isAnimationEnabled)
            {
                // Play the animation to move the power-up cell to a target position.
                StartCoroutine(PowerUpAnimation());
            }

            // Implement the power-up effect.
            ImplementPowerUp(powerUpDatas[0].powerUpType);

            // Remove the used power-up from the list.
            powerUpDatas.RemoveAt(0);

            // Update the power-up count text.
            powerUpCountText.text = powerUpDatas.Count.ToString();

            // If no power-ups are left, deactivate the button.
            if (powerUpDatas.Count == 0)
            {
                SetButtonInactive();
            }
            else
            {
                // Start the cooldown animation if there are more power-ups available.
                StartCoroutine(RefreshButtonActive(powerUpDatas[0].powerUpDuration));
            }
        }
    }

    /// <summary>
    /// Animates the power-up button cooldown.  Fills up the Lock image and makes the button non-interactable.
    /// </summary>
    /// <param name="duration">The duration of the cooldown.</param>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator RefreshButtonActive(float duration)
    {
        powerUpButton.interactable = false;
        powerUpLockIconImage.gameObject.SetActive(true);
        powerUpLockIconImage.fillAmount = 0;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            powerUpLockIconImage.fillAmount = Mathf.Lerp(0, 1, elapsedTime / duration); // Animate the filling of the image
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
    private void SetButtonInactive()
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
    private void ImplementPowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                StartCoroutine(SpearThrowingRaiseSpeed((int)powerUpDatas[0].powerUpValue, powerUpDatas[0].powerUpDuration));
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

    private IEnumerator SpearThrowingRaiseSpeed(int newSpeed, float time)
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

    private IEnumerator IncreasingFishDamage(float newDamage, float time)
    {
        // Get the list of currently active fish.
        List<GameObject> fishsCreated = GameManager.Instance.ReadFishCreatAndDeadList().Item1;

        // Store default damages if not already stored
        if (defaultFishDamages.Count == 0)
        {
            foreach (GameObject fish in fishsCreated)
            {
                defaultFishDamages[fish] = fish.GetComponent<FishDamage>().GetDamage();
            }
        }

        // Increase the damage of each fish by the power-up value.
        foreach (GameObject fish in fishsCreated)
        {
            fish.GetComponent<FishDamage>().SetDamage(newDamage + defaultFishDamages[fish]);
        }

        // Wait for the duration of the power-up.
        yield return new WaitForSeconds(time);

        // Reset the damage of each fish to its default value.
        foreach (GameObject fish in fishsCreated)
        {
            fish.GetComponent<FishDamage>().SetDamage(defaultFishDamages[fish]);
        }
    }

    private IEnumerator ReducingThrowingWaitTime(float newWaitTime, float time)
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

    private IEnumerator PowerUpAnimation()
    {
        // Instantiate the power-up animation prefab at the cell's position.
        GameObject animation = Instantiate(animationPrefab, startPosition, Quaternion.identity);

        // Set the sprite of the animation object.
        Sprite sprite = powerUpDatas[0].powerUpImage;
        animation.GetComponent<SpriteRenderer>().sprite = sprite;

        // Animate the movement to the center of the screen.
        animation.transform.DOMove(targetPosition, animationDuration);

        // Animate the scale from 0 to targetScale.
        animation.transform.DOScale(targetScale, animationDuration * 1.5f);

        // Play the sound effect for the power-up being collected.
        GameManager.Instance.PlaySound(powerUpSound);

        // Wait for the animation to complete.
        yield return new WaitForSeconds(animationDuration * 1.5f);

        // Animate the movement to the center of the screen.
        animation.transform.DOMove(startPosition, animationDuration);

        // Animate the scale from 0 to targetScale.
        animation.transform.DOScale(Vector3.zero, animationDuration);

        // Wait for the animation to complete.
        yield return new WaitForSeconds(animationDuration * 1.5f);

        // Destroy the animation object after it reaches the center.
        Destroy(animation);
    }
}