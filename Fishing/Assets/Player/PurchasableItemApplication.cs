using UnityEngine;
using System.Collections.Generic;

public class PurchasableItemApplication : MonoBehaviour
{
    // Reference to the player's progress data.
    private PlayerProgressData playerProgressData;

    [Header("Spear Throwing")]
    // Prefab of the spear object.  This will be instantiated.
    [SerializeField] private Transform spearPrefab;
    // Number of spears to create in the pool.
    private int poolSize = 10;


    void Start()
    {
        // Initialize purchased items and apply their properties.
        StartApplyingElements();
        // Initialize the spear pool and pass it to the SpearThrowing script.
        SpearThrowing.Instance.SetSpearTranformPool(InitializeSpearPool());
    }

    // Reads player progress data from the GameManager.
    void StartApplyingElements()
    {
        playerProgressData = GameManager.Instance.ReadPlayerProgressData(playerProgressData);
    }

    /// <summary>
    /// Creates a pool of spear objects to improve performance.
    /// </summary>
    /// <returns>A list of instantiated spear Transforms.</returns>
    List<Transform> InitializeSpearPool()
    {
        // List to hold the pooled spear instances.
        List<Transform> spearPool = new List<Transform>();

        // Get the selected spear prefab from the player's progress data.
        Transform spearTranformPrefab = playerProgressData.selectSpearObject.transform;

        // Instantiate the specified number of spears.
        for (int i = 0; i < poolSize; i++)
        {
            // Instantiate a new spear and set its initial position and rotation.
            Transform spear = Instantiate(spearTranformPrefab, Vector3.zero, Quaternion.identity);
            // Deactivate the spear until it is needed.
            spear.gameObject.SetActive(false);
            // Add the spear to the pool.
            spearPool.Add(spear);
        }

        // Apply the selected material to all spears in the pool.
        SpearDressing(spearPool);

        // Return the list of pooled spears.
        return spearPool;
    }

    /// <summary>
    /// Applies the selected spear material to all spears in the pool.
    /// </summary>
    /// <param name="dresses">The list of spear transforms.</param>
    void SpearDressing(List<Transform> dresses)
    {
        // Iterate through each spear in the list.
        foreach (Transform spear in dresses)
        {
            spear.GetComponent<SpearDressing>().StartSpearDressing(playerProgressData.spearDress.mesh, playerProgressData.spearDress.materials);
        }
    }
}