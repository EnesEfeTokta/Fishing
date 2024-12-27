using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [SerializeField] private TMP_Text timer; // Oyun ekranındaki genel zamanlayıcı
    [SerializeField] private TMP_Text numberTimer; // Animasyonlu geri sayım için metin

    private float finishedTime = 0; // Oyunun bitiş zamanı
    private bool isGameFinished = false; // Oyunun bitip bitmediğini kontrol eden bayrak
    private bool countdownStarted = false; // Geri sayımın bir kez çalışmasını sağlamak için bayrak
    private float time; // Geçen süre

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
    }

    public void SetFinishTime(float time)
    {
        finishedTime += time;
    }

    void Update()
    {
        if (isGameFinished) return;

        // Oyunun bitiş zamanına ulaşıldığında
        if (time >= finishedTime)
        {
            GameFinished();
            return;
        }

        // Geçen süreyi artır
        time += Time.deltaTime;

        // Son 3 saniye kaldığında animasyonu başlat
        if (!countdownStarted && Mathf.FloorToInt(time) >= Mathf.FloorToInt(finishedTime) - 3)
        {
            countdownStarted = true; // Geri sayımı bir kez başlatmak için
            StartCoroutine(Countdown());
        }

        // Timer'ı güncelle
        timer.text = Mathf.FloorToInt(time).ToString();
    }

    public float InstantTime()
    {
        return time;
    }

    public void GameFinished()
    {
        isGameFinished = true;
        GameManager.Instance.IsLevelFinished();
        timer.text = "-x-";
        time = 0;
    }

    IEnumerator Countdown()
    {
        numberTimer.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--) // 3'ten geri sayım
        {
            numberTimer.text = i.ToString();
            numberTimer.transform.localScale = Vector3.zero;

            CanvasGroup canvasGroup = numberTimer.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = numberTimer.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 1;

            float elapsedTime = 0f;
            float duration = 1f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                numberTimer.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / duration);
                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);
                yield return null;
            }
        }

        numberTimer.gameObject.SetActive(false);
    }
}
