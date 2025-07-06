using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    [Header("Referencia a la imagen negra")]
    public Image fadeImage;

    [Header("Config inicial")]
    public bool startBlack = true;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (startBlack)
        {
            SetAlpha(1f);
            StartCoroutine(FadeTo(0f, fadeDuration)); // Desvanecer a transparente
        }
        else
        {
            SetAlpha(0f);
        }
    }

    public void FadeToBlack(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    public void BackToMenu(string sceneName)
    {
        fadeImage = null;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return FadeTo(1f, fadeDuration);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }
    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}

