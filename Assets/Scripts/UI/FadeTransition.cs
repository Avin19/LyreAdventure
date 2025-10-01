using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    public static FadeTransition Instance; // Singleton

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvas; // assign FadePanel's CanvasGroup
    [SerializeField] private float fadeDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn()); // fade in when scene starts
    }

    public IEnumerator FadeIn()
    {
        fadeCanvas.alpha = 1;
        fadeCanvas.blocksRaycasts = true;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 0;
        fadeCanvas.blocksRaycasts = false;
    }

    public IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeCanvas.alpha = 0;
        fadeCanvas.blocksRaycasts = true;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1;
        SceneManager.LoadScene(sceneName);
    }
}
