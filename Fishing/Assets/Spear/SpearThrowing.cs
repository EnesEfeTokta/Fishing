using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearThrowing : MonoBehaviour
{
    [SerializeField] private Transform spearPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float throwingTime = 1;
    private Camera mainCamera;
    private List<Transform> spearPool;
    private PlayerControls playerControls;
    private Waiting waiting;
    private bool throwing = false;
    private CameraShake cameraShake;
    private List<Emoji> fishs = new List<Emoji>();

    void Awake()
    {
        // Initializes player controls and enables them.
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        // Listens for spear throwing input.
        playerControls.Player.SpearThrowing.performed += SpearThrowingInput;
    }

    void Start()
    {
        // Finds the main camera in the scene.
        mainCamera = Camera.main;

        // Finds the Waiting component in the scene.
        waiting = FindAnyObjectByType<Waiting>();

        // Retrieves the CameraShake component from the object.
        cameraShake = GetComponent<CameraShake>();

        // Initializes the spear pool and adds spears to the list.
        InitializeSpearPool();

        // Lists all fish with an Emoji component in the scene.
        fishs = FindObjectsOfType<Emoji>().ToList();
    }

    // Handles spear throw input using the new input system.
    void SpearThrowingInput(InputAction.CallbackContext context)
    {
        // Gets the mouse position on the screen.
        Vector3 mousePos = playerControls.Player.MousePosition.ReadValue<Vector2>();

        // Casts a ray from the camera to the point clicked by the mouse.
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        // If the ray hits an object and no spear is currently being thrown, initiate the spear throw.
        if (Physics.Raycast(ray, out hit) && !throwing)
        {
            // Selects an available spear from the spear pool.
            Transform spear = GetAvailableSpear();
            if (spear != null)
            {
                // Activates the selected spear.
                spear.gameObject.SetActive(true);

                // Positions the spear depending on whether the mouse is on the left or right side of the screen.
                if (mousePos.x < Screen.width / 2)
                {
                    spear.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
                else
                {
                    spear.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }

                // Starts the spear throwing Coroutine towards the target position.
                Vector3 targetPosition = hit.point;
                StartCoroutine(SpearThrow(spear, targetPosition, hit));

                // Triggers a wait time for the spear throw process.
                waiting.TriggerWait(1, throwingTime);
                throwing = true;

                // Prepares the spear for the next throw after the specified time.
                Invoke("ThrowingPreparation", throwingTime);
            }
        }
    }

    // Initializes the spear pool and deactivates all spears.
    void InitializeSpearPool()
    {
        spearPool = new List<Transform>();

        for (int i = 0; i < poolSize; i++)
        {
            // Instantiates the spear prefab in the scene.
            Transform spear = Instantiate(spearPrefab);
            spear.gameObject.SetActive(false); // Initially deactivated.
            spearPool.Add(spear);
        }
    }

    // Finds an available spear from the pool.
    Transform GetAvailableSpear()
    {
        foreach (Transform spear in spearPool)
        {
            if (!spear.gameObject.activeInHierarchy)
            {
                return spear; // Returns a deactivated spear if found.
            }
        }
        return null; // Returns null if no spear is available.
    }

    // Coroutine to throw the spear towards the target.
    IEnumerator SpearThrow(Transform spear, Vector3 targetPosition, RaycastHit hit)
    {
        // Moves the spear until it reaches the target position.
        while (Vector3.Distance(spear.position, targetPosition) > 0.01f)
        {
            // Moves the spear towards the target position.
            spear.position = Vector3.MoveTowards(spear.position, targetPosition, speed * Time.deltaTime);

            // Rotates the spear to face the target.
            spear.LookAt(targetPosition);

            // Triggers the camera shake effect.
            cameraShake.CameraShakeStart(0.01f, 0.01f, 0.04f);

            yield return null;
        }

        // Gets the object hit by the ray and checks if it's valid.
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject != null)
        {
            // If the spear hits an object, shows an emoji for each fish in the list.
            foreach (Emoji emoji in fishs)
            {
                if (emoji != null)
                {
                    emoji.ShowEmoji(EmojiType.Happy); // Shows emoji for each fish.
                }
            }
        }

        // Deactivates the spear to return it to the pool.
        spear.gameObject.SetActive(false);
    }

    // Resets the spear throwing state to allow the next throw.
    void ThrowingPreparation()
    {
        throwing = false;
    }
}