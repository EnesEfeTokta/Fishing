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
    [SerializeField] private float waveSpeed = 2f; // It controls the wave speed.
    [SerializeField] private float waveAmplitude = 0.5f; // The size of the color wave.
    [SerializeField] private Color startColor = Color.red; // Initial color.
    [SerializeField] private Color endColor = Color.blue; // Finish color.

    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);

        string levelName = PlayerPrefs.GetString("UnmashedLevel");
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
        // Settings to update TMP's character information.
        playText.ForceMeshUpdate();
        TMP_TextInfo textInfo = playText.textInfo;
        Color32[] newVertexColors;

        while (true)
        {
            playText.ForceMeshUpdate(); // Update TMP Information.
            textInfo = playText.textInfo;

            // Make color settings for each character.
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue; // Jump the invisible characters.

                // Take the character's vertex colors.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                newVertexColors = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32;

                // The color changing over time for wave effect.
                float wave = Mathf.Sin(Time.time * waveSpeed + i * waveAmplitude);
                Color32 currentColor = Color32.Lerp(startColor, endColor, (wave + 1) / 2);

                // Set the color for each corner.
                newVertexColors[vertexIndex + 0] = currentColor;
                newVertexColors[vertexIndex + 1] = currentColor;
                newVertexColors[vertexIndex + 2] = currentColor;
                newVertexColors[vertexIndex + 3] = currentColor;
            }

            // Update Mesh.
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                playText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;
        }
    }
}