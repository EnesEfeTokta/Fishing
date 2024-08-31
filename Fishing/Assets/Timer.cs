using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    private float time;

    void Update()
    {
        time += Time.deltaTime;
        timer.text = Convert.ToInt32(time).ToString();
    }
}
