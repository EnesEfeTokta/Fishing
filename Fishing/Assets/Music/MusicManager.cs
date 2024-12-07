using UnityEngine;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton control
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object not affected by the stage passage.
        }
        else
        {
            Destroy(gameObject); // Prevent more than one object.
            return;
        }

        // Take the Audiosource component.
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void SaveMusicTime()
    {
        PlayerPrefs.SetFloat("SavedMusicTime", audioSource.time); // Save the time of the song.
    }

    void LoadMusicTime()
    {
        float savedTime = PlayerPrefs.GetFloat("SavedMusicTime", 0);
        audioSource.time = savedTime; // Play the song from where it left off.

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}