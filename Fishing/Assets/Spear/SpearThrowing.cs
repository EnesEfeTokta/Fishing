using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearThrowing : MonoBehaviour
{
    [Header("Spear Throwing")]
    [SerializeField] private Transform spearPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float throwingTime = 1;
    [SerializeField] private List<AudioClip> throwingClips = new List<AudioClip>();
    private Camera mainCamera;
    private List<Transform> spearPool;
    private PlayerControls playerControls;
    private Waiting waiting;
    private bool throwing = false;
    private CameraShake cameraShake;
    private List<Emoji> fishs = new List<Emoji>();

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable(); // Enable input actions
    }

    void OnEnable()
    {
        // Input events bağlanıyor
        playerControls.Player.SpearThrowing.performed += SpearThrowingInput;
    }

    void OnDisable()
    {
        // Input events kaldırılıyor
        playerControls.Player.SpearThrowing.performed -= SpearThrowingInput;
        playerControls.Player.Disable(); // Disable input actions to prevent leaks
    }

    void OnDestroy()
    {
        // Ekstra güvenlik için, objeyi yok ederken tüm input'u devre dışı bırak
        playerControls.Player.Disable();
    }

    void Start()
    {
        mainCamera = Camera.main;

        waiting = FindAnyObjectByType<Waiting>();
        cameraShake = GetComponent<CameraShake>();

        InitializeSpearPool();
        fishs = FindObjectsOfType<Emoji>().ToList();
    }

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

    void InitializeSpearPool()
    {
        spearPool = new List<Transform>();

        for (int i = 0; i < poolSize; i++)
        {
            Transform spear = Instantiate(spearPrefab);
            spear.gameObject.SetActive(false);
            spearPool.Add(spear);
        }
    }

    Transform GetAvailableSpear()
    {
        return spearPool.FirstOrDefault(spear => !spear.gameObject.activeInHierarchy);
    }

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

    void ThrowingPreparation()
    {
        throwing = false;
    }
}