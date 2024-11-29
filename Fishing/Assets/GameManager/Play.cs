using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;

public class Play : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image playIcon;
    [SerializeField] private TMP_Text playText;
    [SerializeField] private Button playButton;

    [Header("Animation Settings")]
    [SerializeField] private float waveSpeed = 2f; // Dalga hızını kontrol eder
    [SerializeField] private float waveAmplitude = 0.5f; // Renk dalgasının büyüklüğü
    [SerializeField] private Color startColor = Color.red; // Başlangıç rengi
    [SerializeField] private Color endColor = Color.blue; // Bitiş rengi

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);

        string levelName = PlayerPrefs.GetString("SelectedLevel");
        playText.text = $"Play with {levelName}...";

        PlayButtonAnimation(1.05f, 0.5f, Color.green);

        StartCoroutine(AnimateTextColors());
    }

    void PlayButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    void PlayButtonAnimation(float scale, float duration, Color color)
    {
        playButton.transform.DOScale(scale, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        playIcon.DOColor(color, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        //playText.DOColor(color, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    IEnumerator AnimateTextColors()
    {
        // TMP'nin karakter bilgilerini güncellemek için gerekli ayarlar
        playText.ForceMeshUpdate();
        TMP_TextInfo textInfo = playText.textInfo;
        Color32[] newVertexColors;

        while (true)
        {
            playText.ForceMeshUpdate(); // TMP bilgilerini güncelle
            textInfo = playText.textInfo;

            // Her bir karakter için renk ayarlarını yap
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue; // Görünmeyen karakterleri atla

                // Karakterin vertex (köşe) renklerini al
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                newVertexColors = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32;

                // Dalga etkisi için zamanla değişen renk
                float wave = Mathf.Sin(Time.time * waveSpeed + i * waveAmplitude);
                Color32 currentColor = Color32.Lerp(startColor, endColor, (wave + 1) / 2);

                // Her bir köşe için rengi ayarla
                newVertexColors[vertexIndex + 0] = currentColor;
                newVertexColors[vertexIndex + 1] = currentColor;
                newVertexColors[vertexIndex + 2] = currentColor;
                newVertexColors[vertexIndex + 3] = currentColor;
            }

            // Mesh'i güncelle
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                playText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }
}