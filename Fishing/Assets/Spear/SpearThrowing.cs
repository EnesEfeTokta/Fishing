using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearThrowing : MonoBehaviour
{
    public static SpearThrowing Instance; // Singleton instance

    [Header("Spear Throwing")]
    [SerializeField] private float speed = 10f; // Speed of the spear
    [SerializeField] private float throwingTime = 1; // Cooldown time between throws
    [SerializeField] private List<AudioClip> throwingClips = new List<AudioClip>(); // List of throwing sound effects
    private Camera mainCamera; // Reference to the main camera
    private List<Transform> spearPool; // Pool of spear instances
    private PlayerControls playerControls; // Input actions
    private Waiting waiting; // Waiting UI controller
    private bool throwing = false; // Flag to prevent throwing while already throwing
    private CameraShake cameraShake; // Camera shake controller
    private List<Emoji> fishs = new List<Emoji>(); // List of fish emojis

    /// <summary>
    /// Singleton instance initialization in the Awake method.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerControls = new PlayerControls();
        playerControls.Player.Enable(); // Enable input actions for player
    }

    /// <summary>
    /// Gets the current spear speed.
    /// Used by the PowerUp system.
    /// </summary>
    public float ReadSpearSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Sets the speed of the spear.
    /// This method is used by the PowerUp system to modify the spear's speed.
    /// </summary>
    /// <param name="newSpeed">The new speed value to set for the spear. This value should be greater than 0.</param>
    public void SetSpearSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    /// <summary>
    /// Gets the current throwing time (cooldown between throws).
    /// Used by the PowerUp system.
    /// </summary>
    public float ReadThrowingTime()
    {
        return throwingTime;
    }

    /// <summary>
    /// Sets the cooldown time between spear throws.
    /// This method is used by the PowerUp system to modify the throwing cooldown.
    /// </summary>
    /// <param name="newTime">The new cooldown time to set for throwing spears. This value should be greater than 0.</param>
    public void SetThrowingTime(float newTime)
    {
        throwingTime = newTime;
    }

    /// <summary>
    /// Subscribes to the input action for spear throwing when the object is enabled.
    /// </summary>
    void OnEnable()
    {
        playerControls.Player.SpearThrowing.performed += SpearThrowingInput;
    }

    /// <summary>
    /// Unsubscribes from the input action for spear throwing when the object is disabled.
    /// </summary>
    void OnDisable()
    {
        playerControls.Player.SpearThrowing.performed -= SpearThrowingInput;
        playerControls.Player.Disable(); // Disable Player input
    }

    /// <summary>
    /// Disables player input when the object is destroyed.
    /// </summary>
    void OnDestroy()
    {
        playerControls.Player.Disable(); 
    }

    /// <summary>
    /// Initializes the main camera, waiting UI, camera shake controller, and spear pool.
    /// Also, it gathers all the fish emojis in the scene.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        waiting = FindAnyObjectByType<Waiting>();
        cameraShake = GetComponent<CameraShake>();
        
        fishs = FindObjectsOfType<Emoji>().ToList();
    }

    public void SetSpearTranformPool(List<Transform> transforms)
    {
        spearPool = transforms;
    }

    /// <summary>
    /// Handles the spear throwing input action.
    /// Casts a ray from the mouse position and throws a spear if valid.
    /// </summary>
    /// <param name="context">The input action callback context.</param>
    void SpearThrowingInput(InputAction.CallbackContext context)
    {
        if (mainCamera == null) return;

        Vector3 mousePos = playerControls.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !throwing)
        {
            int randomIndex = Random.Range(0, throwingClips.Count);
            GameManager.Instance.PlaySound(throwingClips[randomIndex]);

            Transform spear = GetAvailableSpear();
            if (spear != null)
            {
                spear.gameObject.SetActive(true);

                // Set spear position based on mouse position
                if (mousePos.x < Screen.width / 2)
                {
                    spear.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
                else
                {
                    spear.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }

                Vector3 targetPosition = hit.point;
                StartCoroutine(SpearThrow(spear, targetPosition, hit));

                waiting.TriggerWait(1, throwingTime);
                throwing = true;
                Invoke("ThrowingPreparation", throwingTime);
            }
        }
    }

    /// <summary>
    /// Retrieves an inactive spear from the pool.
    /// </summary>
    /// <returns>An inactive spear, or null if no inactive spears are available.</returns>
    Transform GetAvailableSpear()
    {
        return spearPool.FirstOrDefault(spear => !spear.gameObject.activeInHierarchy);
    }

    /// <summary>
    /// Coroutine that handles the movement of the spear towards the target position.
    /// Also, checks if the spear hit an object, and triggers related actions (e.g., showing happy emojis).
    /// </summary>
    /// <param name="spear">The spear being thrown.</param>
    /// <param name="targetPosition">The position where the spear should move towards.</param>
    /// <param name="hit">Information about the object that the spear collided with.</param>
    /// <returns>An enumerator for the coroutine.</returns>
    IEnumerator SpearThrow(Transform spear, Vector3 targetPosition, RaycastHit hit)
    {
        while (Vector3.Distance(spear.position, targetPosition) > 0.01f)
        {
            spear.position = Vector3.MoveTowards(spear.position, targetPosition, speed * Time.deltaTime);
            spear.LookAt(targetPosition);
            cameraShake.CameraShakeStart(0.01f, 0.01f, 0.04f);
            yield return null;
        }

        GameObject hitObject = hit.collider?.gameObject;
        if (hitObject != null)
        {
            // Show happy emoji on all fish
            foreach (Emoji emoji in fishs)
            {
                if (emoji != null)
                {
                    emoji.ShowEmoji(EmojiType.Happy);
                }
            }
        }

        spear.gameObject.SetActive(false);
    }

    /// <summary>
    /// Resets the throwing flag after the cooldown period.
    /// </summary>
    void ThrowingPreparation()
    {
        throwing = false;
    }
}