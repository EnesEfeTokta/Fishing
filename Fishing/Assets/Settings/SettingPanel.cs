using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingPanel : MonoBehaviour
{
    [Header("Ambient Music")]
    [SerializeField] private Slider ambientMusicBar; // Slider used for adjusting ambient music volume.
    [SerializeField] private TMP_Text ambientMusicPercentage; // Text showing the current ambient music volume in percentage.
    private float ambientMusicValue; // Internal variable to store the ambient music volume value.

    [Header("Ambient Sounds")]
    [SerializeField] private Slider ambientSoundsBar; // Slider used for adjusting ambient sound effects volume.
    [SerializeField] private TMP_Text ambientSoundsPercentage; // Text showing the current ambient sound effects volume in percentage.
    private float ambientSoundsValue; // Internal variable to store the ambient sound effects volume value.

    [Header("Post-Processing")]
    [SerializeField] private Image postProcessingStatusButton; // UI button to toggle post-processing effects (visual effects).
    [SerializeField] private Color activeColor; // Color for the button when post-processing is active.
    [SerializeField] private Color passiveColor; // Color for the button when post-processing is inactive.
    [SerializeField] private TMP_Text postProcessingStatus; // Text indicating whether post-processing is "On" or "Off".

    [Header("Languages")]
    [SerializeField] private TMP_Dropdown languagesOptions; // Dropdown to select the game language.

    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityOptions; // Dropdown to select the graphics quality level.

    [Header("Settings Data")]
    [SerializeField] private SettingsData settingsData; // Stores user settings like sound volume, quality, language, etc.

    [Header("GameObjects")]
    [SerializeField] private GameObject settingsPanel; // The settings panel GameObject to show/hide settings UI.
    [SerializeField] private RectTransform settingIcon; // UI image representing the setting icon.
    private bool isOpenPanel = false; // Tracks whether the settings panel is open or closed.

    // Function to open or close the settings panel.
    public void PanelOpenClose()
    {
        isOpenPanel = !isOpenPanel; // Updates the internal flag based on the panel state (open/close).

        settingsPanel.SetActive(isOpenPanel); // Toggles the visibility of the settings panel.

        AnimateSettingIcon(); // Animate the setting icon to rotate.

        if (isOpenPanel)
        {
            // Load and display the current values of music and sound sliders from settingsData.
            SliderDataFilling(
                new Slider[] { ambientMusicBar, ambientSoundsBar }, 
                new TMP_Text[] { ambientMusicPercentage, ambientSoundsPercentage }, 
                new float[] { settingsData.musicValue, settingsData.soundValue }
            );

            AnimatePanelOpen(); // Animate the panel opening.
            
            SetDropdowns(); // Initialize dropdowns for language and quality settings.
            SetPostProcessing(); // Apply the post-processing status to the UI.
        }
        else
        {
            // Save the settings when the panel is closed.
            SaveSlidersData(); // Save the current slider values (music/sound) to the settingsData.
        }
    }

    // Method to animate the panel opening.
    void AnimatePanelOpen()
    {
        RectTransform rectTransform = settingsPanel.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1, 1); // Set pivot to top-left corner.
        rectTransform.localScale = new Vector3(0, 0, 0); // Start from scale 0.
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack); // Animate to scale 1.
    }

    // Animate the setting icon to rotate.
    void AnimateSettingIcon()
    {
        settingIcon.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.LocalAxisAdd);
    }

    // Update is called once per frame. It's used here to continuously update the percentage texts for sliders.
    void Update()
    {
        if (isOpenPanel) // Only update when the settings panel is open.
        {
            // Update the text for the ambient music slider to show the rounded value in percentage.
            ambientMusicPercentage.text = $"%{Mathf.RoundToInt(ambientMusicBar.value * 100)}";
            
            // Update the text for the ambient sounds slider to show the rounded value in percentage.
            ambientSoundsPercentage.text = $"%{Mathf.RoundToInt(ambientSoundsBar.value * 100)}";
        }
    }

    // Save the current slider values (music and sound) into the settings data when the panel is closed.
    void SaveSlidersData()
    {
        settingsData.musicValue = ambientMusicBar.value * 100; // Convert the normalized slider value (0-1) to percentage (0-100).
        settingsData.soundValue = ambientSoundsBar.value * 100; // Same conversion for sound effects volume.
    }

    // Function to fill slider and text UI elements with values from settingsData.
    void SliderDataFilling(Slider[] sliders, TMP_Text[] texts, float[] values)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            values[i] = Mathf.RoundToInt(values[i]); // Round the values to the nearest integer.
            sliders[i].value = values[i] / 100; // Set the slider's value as a normalized number (between 0 and 1).
            texts[i].text = $"%{values[i]}"; // Display the rounded value as a percentage in the associated text element.
        }
    }

    // Initializes the dropdown options for languages and graphics quality.
    void SetDropdowns()
    {
        // Add event listeners to handle changes in dropdown values.
        languagesOptions.onValueChanged.AddListener(LanguagesOptionsValueChanged);
        qualityOptions.onValueChanged.AddListener(QualityOptionsValueChanged);

        languagesOptions.ClearOptions(); // Clear any existing options in the language dropdown.
        qualityOptions.ClearOptions(); // Clear any existing options in the quality dropdown.

        // Get the list of language options from the OptionLanguages enum.
        List<string> languagesOptionsList = Enum.GetNames(typeof(OptionLanguages)).ToList();
        
        // Get the list of quality options from the RendererQualityOptions enum.
        List<string> qualityOptionsList = Enum.GetNames(typeof(RendererQualityOptions)).ToList();

        // Add the language and quality options to their respective dropdowns.
        languagesOptions.AddOptions(languagesOptionsList);
        qualityOptions.AddOptions(qualityOptionsList);

        // Set the dropdown values based on the current settings stored in settingsData.
        languagesOptions.value = (int)settingsData.optionLanguages; // Set the language dropdown to the current language.
        qualityOptions.value = (int)settingsData.rendererQualityOptions; // Set the quality dropdown to the current quality level.
    }

    // Called when the language dropdown value is changed by the user.
    void LanguagesOptionsValueChanged(int index)
    {
        settingsData.optionLanguages = (OptionLanguages)index; // Save the selected language in settingsData.
    }

    // Called when the quality dropdown value is changed by the user.
    void QualityOptionsValueChanged(int index)
    {
        settingsData.rendererQualityOptions = (RendererQualityOptions)index; // Save the selected quality level in settingsData.
    }

    // Updates the post-processing button to reflect the current state (on/off).
    void SetPostProcessing()
    {
        if (settingsData.isPostProcessing)
        {
            postProcessingStatusButton.color = activeColor; // Change the button color to indicate post-processing is active.
            postProcessingStatus.text = "On"; // Update the text to indicate post-processing is on.
        }
        else
        {
            postProcessingStatusButton.color = passiveColor; // Change the button color to indicate post-processing is inactive.
            postProcessingStatus.text = "Off"; // Update the text to indicate post-processing is off.
        }
    }

    // Toggles the post-processing effect when the user presses the button.
    public void ChangePostProcessing()
    {
        settingsData.isPostProcessing = !settingsData.isPostProcessing; // Toggle the post-processing state.
        SetPostProcessing(); // Update the UI to reflect the new post-processing state.
    }

    // Placeholder function for applying the selected language (implementation can be added).
    public void ApplyTheNewLanguage()
    {
        Debug.Log("Game language is changing..."); // Log the action of changing the game language.
        // Additional logic for changing the game's language can be implemented here.
    }
}