using UnityEngine;

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
}