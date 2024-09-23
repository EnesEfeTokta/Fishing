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
        playerControls = new PlayerControls();
        playerControls.Player.Enable();

        playerControls.Player.SpearThrowing.performed += SpearThrowingInput;
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
        Vector3 mousePos = playerControls.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !throwing)
        {
            Transform spear = GetAvailableSpear();
            if (spear != null)
            {
                spear.gameObject.SetActive(true);

                if (mousePos.x < 960)
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
        foreach (Transform spear in spearPool)
        {
            if (!spear.gameObject.activeInHierarchy)
            {
                return spear;
            }
        }
        return null;
    }

    IEnumerator SpearThrow(Transform spear, Vector3 targetPosition, RaycastHit hit)
    {
        while (Vector3.Distance(spear.position, targetPosition) > 0.1f)
        {
            spear.position = Vector3.MoveTowards(spear.position, targetPosition, speed * Time.deltaTime);
            spear.LookAt(targetPosition);

            cameraShake.CameraShakeStart(0.01f, 0.01f, 0.04f);

            yield return null;
        }

        GameObject hitObject = hit.collider.gameObject;
        if (hitObject != null)
        {
            Debug.Log($"hish: {hitObject}");

            foreach (Emoji emoji in fishs)
            {
                if (emoji != null)
                {
                    emoji.ShowEmoji();
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