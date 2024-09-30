using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("Ambient Music")]
    [SerializeField] private Slider AmbientMusicBar;
    [SerializeField] private TMP_Text ambientMusicPercentage;
    private float ambientMusicValue;

    [Header("Ambient Sounds")]
    [SerializeField] private Slider AmbientSoundsBar;
    [SerializeField] private TMP_Text ambientSoundsPercentage;
    private float ambientSoundsValue;

    [Header("Post-Processing")]
    [SerializeField] private Button postProcessingStatusButton;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color passiveColor;
    [SerializeField] private TMP_Text postProcessingStatus;

    [Header("Languages")]
    [SerializeField] private TMP_Dropdown languagesOptions;

    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityOptions;

    [Header("Settings Data")]
    [SerializeField] private SettingsData settingsData;
}