using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PastPlayManager))]
[RequireComponent(typeof(SettingPanel))]
[RequireComponent(typeof(ShopPanel))]
[RequireComponent(typeof(Profile))]
[RequireComponent(typeof(Message))]
[RequireComponent(typeof(MoneyLoadingPanel))]
[RequireComponent(typeof(MoneyOptionDetails))]
[RequireComponent(typeof(LevelInformationController))]
[RequireComponent(typeof(LevelSorter))]
[RequireComponent(typeof(Play))]
public class HomeManager : MonoBehaviour
{
    public static HomeManager Instance;
    [SerializeField] private AudioSource audioSource; // The audio source component for playing sounds.

    void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of HomeManager exists.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    /// <summary>
    /// Plays a sound using the specified audio clip and settings.
    /// </summary>
    /// <param name="clip">The audio clip to be played.</param>
    /// <param name="volume">The volume level at which the clip should be played. Default is 1f.</param>
    /// <param name="priority">The priority level of the audio source. Default is 128.</param>
    /// <param name="pitch">The pitch level at which the clip should be played. Default is 1f.</param>
    public void PlaySound(AudioClip clip, float volume = 1f, int priority = 128, float pitch = 1f)
    {
        audioSource.Stop();

        audioSource.clip = clip;

        audioSource.volume = volume;
        audioSource.priority = priority;
        audioSource.pitch = pitch;

        audioSource.Play();
    }
}