using System.Collections;
using System.Collections.Generic;
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

        InitializeSpearPool();
    }

    private void SpearThrowingInput(InputAction.CallbackContext context)
    {
        Vector3 mousePos = playerControls.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !throwing)
        {
            Transform spear = GetAvailableSpear();
            if (spear != null)
            {
                if (mousePos.x < 960)
                {
                    spear.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
                else
                {
                    spear.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }

                spear.gameObject.SetActive(true);

                Vector3 targetPosition = hit.point;
                StartCoroutine(SpearThrow(spear, targetPosition));

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

    IEnumerator SpearThrow(Transform spear, Vector3 targetPosition)
    {
        while (Vector3.Distance(spear.position, targetPosition) > 0.1f)
        {
            spear.position = Vector3.MoveTowards(spear.position, targetPosition, speed * Time.deltaTime);
            spear.LookAt(targetPosition);
            yield return null;
        }

        spear.gameObject.SetActive(false);
    }

    void ThrowingPreparation()
    {
        throwing = false;
    }
}