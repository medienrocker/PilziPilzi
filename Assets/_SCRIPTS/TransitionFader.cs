using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionFader : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade in
        yield return StartCoroutine(Fade(0f, 1f));

        // Unload the previous scene (if any)
        if (SceneManager.sceneCount > 1)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            yield return SceneManager.UnloadSceneAsync(currentScene);
        }

        // Load the target scene
        string targetSceneName = PlayerPrefs.GetString("TargetSceneName");
        yield return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

        // Set the newly loaded scene as active
        Scene newScene = SceneManager.GetSceneByName(targetSceneName);
        SceneManager.SetActiveScene(newScene);

        // Fade out
        yield return StartCoroutine(Fade(1f, 0f));

        // Unload the transition scene
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}