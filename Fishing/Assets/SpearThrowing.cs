using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrowing : MonoBehaviour
{
    [SerializeField] private Transform spearPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private float speed = 10f;
    private Camera mainCamera;
    private List<Transform> spearPool;

    void Start()
    {
        mainCamera = Camera.main;
        InitializeSpearPool();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform spear = GetAvailableSpear();
                if (spear != null)
                {
                    if (Input.mousePosition.x < 960)
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
                }
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
}