using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraLookAt : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
