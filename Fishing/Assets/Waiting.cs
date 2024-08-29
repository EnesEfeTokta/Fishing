using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Waiting : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image waitBarValue;
    [SerializeField] private TMP_Text waitTime;
    [SerializeField] private Image waitBar;

    void Start()
    {
        waitBar.gameObject.SetActive(false);
        waitTime.text = "";
    }

    public void TriggerWait(float value, float duration)
    {
        StartCoroutine(WaitLerp(value, duration));
    }

    IEnumerator WaitLerp(float targetValue, float duration)
    {
        waitBar.gameObject.SetActive(true);

        waitBarValue.fillAmount = 0;
        waitBarValue.color = Color.red;
        waitTime.text = "0";

        float startValue = waitBarValue.fillAmount;
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            waitTime.text = Convert.ToInt32(elapsedTime).ToString();
            waitBarValue.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);

            if (waitBarValue.fillAmount < 0.3f)
            {
                waitBarValue.color = Color.red;
                waitTime.color = Color.red;
            }
            else if (waitBarValue.fillAmount < 0.7f)
            {
                waitBarValue.color = Color.yellow;
                waitTime.color = Color.yellow;
            }
            else
            {
                waitBarValue.color = Color.green;
                waitTime.color = Color.green;
            }
            yield return null;
        }

        waitBar.gameObject.SetActive(false);
        waitTime.text = "";
    }
}
