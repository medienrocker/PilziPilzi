using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private string transitionSceneName = "TransitionScene";

    private Image fadeImage;
    private bool isTransitioning = false;

    private void Awake()
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

    public void LoadSceneWithTransition(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(LoadSceneWithTransitionCoroutine(sceneName));
        }
    }

    private IEnumerator LoadSceneWithTransitionCoroutine(string sceneName)
    {
        isTransitioning = true;

        // Load the transition scene additively
        yield return SceneManager.LoadSceneAsync(transitionSceneName, LoadSceneMode.Additive);

        // Find the fade image in the transition scene
        fadeImage = FindObjectOfType<Image>();

        if (fadeImage != null)
        {
            // Fade in
            yield return StartCoroutine(FadeCoroutine(0f, 1f));

            // Unload the current scene
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            // Load the target scene
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            // Set the newly loaded scene as active
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(newScene);

            // Fade out
            yield return StartCoroutine(FadeCoroutine(1f, 0f));

            // Unload the transition scene
            yield return SceneManager.UnloadSceneAsync(transitionSceneName);
        }
        else
        {
            Debug.LogError("Fade image not found in transition scene");
            SceneManager.LoadScene(sceneName);
        }

        isTransitioning = false;
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration && fadeImage != null)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            if (fadeImage != null)
            {
                fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            }
            yield return null;
        }

        if (fadeImage != null)
        {
            fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
        }
    }
}