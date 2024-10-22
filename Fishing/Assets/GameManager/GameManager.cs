using UnityEngine;


[RequireComponent(typeof(PastPlayRecorder))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private PastPlaysData pastPlaysData;
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private LevelInformationData levelInformationData;
    [SerializeField] private SettingsData settingsData;

    public PastPlaysData ReadPastPlaysData(PastPlaysData pastPlaysData)
    {
        return pastPlaysData = this.pastPlaysData;
    }

    public PlayerProgressData ReadPlayerProgressData(PlayerProgressData playerProgressData)
    {
        return playerProgressData = this.playerProgressData;
    }

    public LevelInformationData ReadLevelInformationData(LevelInformationData levelInformationData)
    {
        return levelInformationData = this.levelInformationData;
    }

    public SettingsData ReadSettingsData(SettingsData settingsData)
    {
        return settingsData = this.settingsData;
    }
}
